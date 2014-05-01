using System;

namespace bdb.core.engine
{
	/// <summary>
	/// Summary description for bdbEngineMain.
	/// </summary>
	public class bdbEngineMain{
		Axiom.Core.Root m_myRoot;
		public bdbEngineMain(){
			try
			{
				m_myRoot = new Axiom.Core.Root("config.xml","debug");

				//Frame listeners
				m_myRoot.FrameStarted += new Axiom.Core.FrameEvent(OnFrameStarted);
				m_myRoot.FrameEnded += new Axiom.Core.FrameEvent(OnFrameEnded);
				Axiom.Graphics.RenderSystem renderSystem = Axiom.Core.Root.Instance.RenderSystems["DirectX9"];
				m_myRoot.RenderSystem = renderSystem;
				Axiom.Configuration.EngineConfig.DisplayModeRow mode = renderSystem.ConfigOptions.DisplayMode[0];
				mode.FullScreen = true;
				mode.Selected = true;
				m_myRoot.Initialize(true);
			}catch(Exception e){
				new bdb.tools.wrappers.genericDialogs().ShowErrorBox(e.ToString());
				System.Console.WriteLine(e.ToString());
			}
			
		}


		protected virtual void OnFrameStarted(Object source, Axiom.Core.FrameEventArgs e){
		}
		
		protected virtual void OnFrameEnded(Object source, Axiom.Core.FrameEventArgs e){
		}
	}

}
