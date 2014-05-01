using System;
using System.IO;
using bdb.tools.encrypt.CryptEngine;
using bdb.tools.wrappers;


namespace bdb.tools.fileWrappers{
	/// <summary>
	/// Summary description for BdbFileWriter.
	/// </summary>
	public class BdbFileWriter{
		
		public	StreamWriter m_myWriter;
		private	XCryptEngine m_myEncrypter;
		//private byte[]			 m_myKey;
		private bool         m_bEncrypted;

#region constructors
		public BdbFileWriter(){
			try{
				new genericDialogs().ShowErrorBox("Use a real constructor dammit ;)");				
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());				
			}
			//m_myKey[0] =  20;
			//m_myKey[1] =  40;
			//m_myKey[2] =  65;
		}
#endregion


#region methods
		public bool Write(){
			return false;
		}	
		
	public bool Test(){
			string teststring = "I am a test string";
			//byte[] m_myArr;
			m_myEncrypter = new XCryptEngine(XCryptEngine.AlgorithmType.BlowFish);
			try{
				m_myWriter = new StreamWriter("bgame.conf");
				for (int w=0; w<10; w++){
					m_myWriter.WriteLine(m_myEncrypter.Encrypt(teststring,"bdbgame"));
				}
				
				
			}
			catch(Exception e){
				new genericDialogs().ShowErrorBox(e.ToString());
				return false;
			}
			return true;
		}
#endregion
	}

}
