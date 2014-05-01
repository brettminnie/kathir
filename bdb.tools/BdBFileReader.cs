using System;
using System.IO;
using bdb.tools.encrypt.Blowfish_NET;
using bdb.tools.wrappers;


namespace bdb.tools.fileWrappers{
	/// <summary>
	/// Summary description for FileReader.
	/// </summary>
	public class BdbFileReader{
		public		string				m_szFileName;
		private		StreamReader	m_hndFile;
		private		string				m_szInBuffer;
		private		bool					m_bEncrypted;
		private   byte					m_myKey;
		
#region privateAccessors
	//StringBuffer functions
	public string InBuffer(){
		return m_szInBuffer;
	}	
	
	public void InBuffer(string szInBuffer){
		m_szInBuffer = szInBuffer;
	}
	
	//Encrypted
	public bool Encrypted(){
		return m_bEncrypted;
	}

	public void Encrypted(bool bEncrypted){
		m_bEncrypted = bEncrypted;
	}

	public byte Key(){
		return m_myKey;
	}
#endregion

#region class constructors		
		public BdbFileReader(){
			new genericDialogs().ShowErrorBox("Please use a real constructor");
		}
		
		public BdbFileReader(string szFileName, bool bIsEncrypted){
			try{
				m_hndFile = new StreamReader(szFileName);
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
			m_bEncrypted = bIsEncrypted;
			m_myKey =   0;
			m_myKey ^=  20;
			m_myKey ^=  40;
			m_myKey ^=  65;
		}
		

#endregion

#region filemethods
		public void Close(){
			try{
				m_hndFile.Close();
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
		}
		
		public bool ReadLine(){
			try{
				m_szInBuffer = m_hndFile.ReadLine();
				return true;
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
			return false;
		}

		public bool ReadFile(){
			try{
				m_szInBuffer = m_hndFile.ReadToEnd();
				return true;
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
			}
		return false;
	}

#endregion
	}
}
