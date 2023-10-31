using System;
using QuickFix;

namespace TwgSendPTF
{
	class Executor
	{
		[ STAThread ]
		static void Main( string[] args )
		{	
			/*
			string saveNow = DateTime.Now.ToString("M/d/yyyy hh:mm:ss tt");
			string saveUtc = DateTime.UtcNow.ToString("M/d/yyyy hh:mm:ss tt");
			
			Console.WriteLine("{0} {1}", "Local.:" , saveNow);
			Console.WriteLine("{0} {1}", "UTC...:" , saveUtc);
			*/

			if ( args.Length != 1 )
			{
				DateTime dtLog = DateTime.Now;
				string sNomeArquivo = "";

				sNomeArquivo = string.Format(@"{0:0000}{1:00}{2:00}_Send.txt", 
					dtLog.Year, dtLog.Month, dtLog.Day); // Data e Hora

				clsArquivo.pprNomeArquivoLog = sNomeArquivo;
				clsArquivo.pprPathArquivoLog = @"C:\Inetpub\wwwroot\FIX\TwgSendPTF\TwgSendPTF\temp\";
				clsArquivo.fnGravaLogTXT("Iniciando app...");
				Console.WriteLine( "Iniciando PTF..." );
			}
			try
			{
				SessionSettings settings = new SessionSettings( @"C:\Inetpub\wwwroot\FIX\TwgSendPTF\TwgSendPTF\config\QuickFix.cfg" );
				Application application = new Application();
				FileStoreFactory storeFactory = new FileStoreFactory( settings );
				// ScreenLogFactory logFactory = new ScreenLogFactory(true, true, true);
				FileLogFactory logFactory = new FileLogFactory(settings);
				MessageFactory messageFactory = new DefaultMessageFactory();
				
				SocketInitiator acceptor = new SocketInitiator( application, storeFactory, settings, logFactory, messageFactory );
				acceptor.start();

				clsArquivo.fnGravaLogTXT("Conexao: " + acceptor.ToString());
				Console.WriteLine       ("Conexao: " + acceptor.ToString());

				Console.Read();
				application.Dispose();
				application = null;

				// acceptor.stop();
			}
			catch ( Exception e )
			{
				clsArquivo.fnGravaLogTXT( e.Message.ToString() +  "\r\n" );
				clsArquivo.fnGravaLogTXT( e.TargetSite.ToString() +  "\r\n" );

				Console.WriteLine( e.Message.ToString() +  "\r\n" );
				Console.WriteLine( e.TargetSite.ToString() +  "\r\n" );
			}
		}
	}
}