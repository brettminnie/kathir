/************************************************************/
/* Namespace: bdb.tools.wrappers.sql												*/
/* Class sql_wrapper																				*/
/* date: 07-09-2005                                         */
/* Coded by: Brett Minnie																		*/
/*																													*/
/*----------------------------------------------------------*/
/* Generic sql wrapper to simplify the use of a sql server	*/
/* for loading/saving data to a database using ansi sql to	*/
/* make it cross db compatible															*/
/*																													*/
/*----------------------------------------------------------*/
/* Revision 1.0 07-09-2005	- Initial version								*/
/*																													*/
/*----------------------------------------------------------*/
/*																													*/
/*@Todo																											*/
/*----------------------------------------------------------*/

using System;
using System.Data;
using System.Data.Odbc;
using System.IO;

namespace bdb.tools.wrappers
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class sql_wrapper{
		
#region ClassMembers
		private OdbcConnection		m_myDbConn;
		private String						m_szConnectionString;
		private  static String[]	m_szParams =
			{"Driver=", "Server=", "Trusted_Connection=","Database=","UID=","PWD="};
		private  OdbcCommand			m_myCommand;
		private  OdbcDataReader		m_myReader;
		private String						m_szQuery;
		private	StreamReader			m_myInfile;
#endregion

#region Accessors
		public  String ConnectionString{
			get{
				return m_szConnectionString; 
			}
			set{
				m_szConnectionString = value;
			}
		}

		public	OdbcCommand Command{
			get{
				return m_myCommand;
			}
		}

		public	String QueryString{
			get{
				return m_szQuery;
			}
			set{
				m_szQuery = value;
			}
		}
#endregion

#region Methods

		//Base constructor
		public sql_wrapper(){
			try{
				
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
		}
		
		public OdbcDataReader PrepareAndExecute(String szSQL){
			m_myCommand = new OdbcCommand(szSQL);
			m_myCommand.Connection = m_myDbConn;
			m_myReader = m_myCommand.ExecuteReader();
			return m_myReader;
		}

		public sql_wrapper(String szConfigFile){
			try{
				if(LoadConf(szConfigFile)){}
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
		}

		private bool LoadConf(String szFileName){
			try{
				m_myInfile = new StreamReader(szFileName);
				String	szTemp;
				String	szInLine;
				int iCount = 0;

				while(m_myInfile.Peek() >= 0){
					szInLine = m_myInfile.ReadLine();
					szTemp = szInLine.Substring(0,szInLine.IndexOf("#"));
					if(szTemp.Length > 0){
						m_szConnectionString = String.Concat(m_szConnectionString, m_szParams[iCount], szTemp, ";");
						iCount+=1;
					}
				}
				m_myInfile.Close();
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
				return false;
			}
			return true;
		}

		private void ReaderClose(){
			try	{
				if (m_myReader != null){
					if (m_myReader.IsClosed == false){
						m_myReader.Close();
					}
				}
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
		}


#region SQLMethods

		//execute a sql statement
		public OdbcDataReader ExecStatement(String szSQL){
			try{
				ReaderClose();
				m_myReader = PrepareAndExecute(szSQL);
			}				
			catch (OdbcException e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
			return m_myReader;
			
		}
		
		//Execute a prepared sql select statement
		public OdbcDataReader ExecStatement(){ 
			try{
				ReaderClose();
				m_myReader = PrepareAndExecute(m_szQuery);
			}
			catch (OdbcException e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
			return m_myReader;
			
		}

		/*Insert A Single Recordset into a table*/
		public bool Insert(String szTable, String[] szData){
				ReaderClose();
				String szSQL = String.Concat("SELECT TOP 1 * FROM ",szTable);
				int iRet = 0;
				try{
					m_myReader = PrepareAndExecute(szSQL);				
					String[] szFields = new String[m_myReader.FieldCount - 1];
					szSQL = "";
					szSQL = "INSERT INTO " + szTable + " ( " ;
					szFields[0] = m_myReader.GetName(1);//Primary Key
					
					szSQL +=  szFields[0];

					for (int i = 1; i < szFields.Length; i++){
						szSQL += ", ";
						szFields[i] = m_myReader.GetName(i + 1);
						szSQL += szFields[i];
					}
					
					ReaderClose();
					szSQL += ") VALUES ('";
        
					for(int q=0; q< szData.Length; q++){
						szData[q] = szData[q].Replace("'","`");
					}
        
					szSQL +=szData[0];

					for(int i = 1; i < szData.Length; i++){
						szSQL += "', '" +szData[i];
					}
					szSQL += "')";
								
					m_myCommand = new OdbcCommand(szSQL);
					m_myCommand.Connection = m_myDbConn;
					iRet = m_myCommand.ExecuteNonQuery();
				}
				catch(Exception e){
					new genericDialogs().ShowErrorBox(e.ToString());
				}
				return(iRet >= 1)?true:false;
		}
		/*execute a non query */
		public bool NonQuery(String szSQL){
      int iRet = 0;
			ReaderClose();
			try{
				m_myCommand = new OdbcCommand(szSQL, m_myDbConn);
				iRet = m_myCommand.ExecuteNonQuery();
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
				return false;
			}
			return (iRet > 0)?true:false;
		}
		/*Close our db connection*/
		public void CloseConnection(){
			ReaderClose();
			try{
				m_myDbConn.Close();
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
		}
#endregion
#endregion
		}

}
