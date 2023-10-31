using System;
using System.Threading;
//using System.Collections.Generic;
//using System.Text;
using QuickFix;

namespace FIXAcceptor
{
    class Program
    {
        [STAThread] //Indicates that the COM threading model for an application is single-threaded apartment(STA).
        static void Main(string[] args)
        {
            //Caminho base da aplicação
            string strAppPath = @"N:\TI\Projects\CSharp\Quant\FIX\FIXAcceptor\FIXAcceptor\";
            
            //Verifica parametros de entrada do aplicativo
            if (args.Length != 1)
            {
                //Informações para abertura de arquivo de Log
                //TODO: Implementar classe clsArquivo caso o arquivo de log seja relevantemente referenciado
            }

            try
            {
                //Inicializa parametros de configuração FIX com base em arquivo .cfg
                SessionSettings settings = new SessionSettings(strAppPath + @"config\QuickFix.cfg");
                
                //Inicializa aplicação
                MyApplication application = new MyApplication();

                //Inicializa FileStoreFactory, FileLogFactory e MessageFactory
                //TODO: verificar na documentação do QuickFix as classes FileStoreFactory, FileLogFactory e MessageFactory
                FileStoreFactory storeFactory = new FileStoreFactory(settings);
                FileLogFactory logFactory = new FileLogFactory(settings);
                MessageFactory messageFactory = new DefaultMessageFactory();

                //Inicializa SocketAcceptor
                SocketAcceptor acceptor = new SocketAcceptor(application, storeFactory, settings, logFactory, messageFactory);

                //"Startando" acceptor
                acceptor.start();

                //Mensagem de console
                Console.WriteLine("Conexão: " + acceptor.ToString());

                while (true)
                {
                    Thread.Sleep(10000);
                }

                Console.ReadLine();
                acceptor.stop();
                application.Dispose();
                application = null;

                Console.WriteLine("Aplicação finalizada");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString() + "\r\n");
                Console.WriteLine(e.TargetSite.ToString() + "\r\n");
                Console.Read();
            }
        }
    }
}
