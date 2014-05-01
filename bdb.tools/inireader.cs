/*
 * Created by SharpDevelop.
 * User: brett
 * Date: 2005/06/11
 * Time: 08:15 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;
using System.IO;

namespace bdb.tools.fileWrappers{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class IniReader{
		
		StreamReader	m_myReader;
		String[]			m_arrVals;
		ArrayList			m_retVals;

		public IniReader(){
		}

		public IniReader(String szFileName){
			try{
				m_myReader = new StreamReader(szFileName);
			}
			catch(Exception e){
				new bdb.tools.wrappers.genericDialogs().ShowErrorBox(e.ToString());
			}
		}
		
		public bool Open(String szFileName){
			try{
				if(m_myReader == null)
					m_myReader = new StreamReader(szFileName);
			}
			catch(Exception e){
				new bdb.tools.wrappers.genericDialogs().ShowErrorBox(e.ToString());
				return false;
			}
			return true;
		}
		
		public bool Close(){
			try{
				if(m_myReader == null)
					m_myReader.Close();
			}
			catch(Exception e){
				new bdb.tools.wrappers.genericDialogs().ShowErrorBox(e.ToString());
				return false;
			}
			return true;
		}
	}

	//End of class
	public class IniWriter{
		public		string				m_szFileName;
		private		StreamWriter	m_hndFile;
		public		string				m_szOutBuffer;
		public IniWriter(){
		}
	}
}
