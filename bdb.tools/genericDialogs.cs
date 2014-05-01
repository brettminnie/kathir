/*
 * Created by SharpDevelop.
 * User: brett
 * Date: 2005/06/11
 * Time: 07:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Windows.Forms;

namespace bdb.tools.wrappers
{
	/// <summary>
	/// A wrapper to simplify message dialogs
	/// </summary>
	public class genericDialogs{
		public genericDialogs(){
		}
		
		public void ShowErrorBox(String szMsg){
			MessageBox.Show(szMsg.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
		}
		
		public void ShowInfoBox(String szMsg){
			MessageBox.Show(szMsg.ToString(),"Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}
		
		public void ShowWarningBox(String szMsg){
			MessageBox.Show(szMsg.ToString(),"Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
		}
	}
}
