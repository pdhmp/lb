using System;
using NestQuant.Common;

namespace FIXInitiator
{
    class Program
    {
        [STAThread] //Indicates that the COM threading model for an application is single-threaded apartment(STA).
        static void Main(string[] args)
        {
            //Caminho base da aplicação
            string strAppPath = @"N:\TI\Projects\CSharp\Quant\FIX\FIXInitiator\FIXInitiator\config\QuickFix.cfg";

            //Verifica parametros de entrada do aplicativo
            if (args.Length != 1)
            {
                //Informações para abertura de arquivo de Log
                //TODO: Implementar classe clsArquivo caso o arquivo de log seja relevantemente referenciado
            }

            try
            {
                FIXConn fix = new FIXConn(strAppPath);

                System.Threading.Thread.Sleep(5000);

                //fix.sendOrder(1, 1, -1);

                Console.Read();

                fix.Dispose();
                fix = null;

                Console.WriteLine("Aplicação finalizada");
                System.Threading.Thread.Sleep(5000);
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
