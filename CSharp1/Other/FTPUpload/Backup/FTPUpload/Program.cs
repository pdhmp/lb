using System;
using System.Collections.Generic;
using System.Text;

namespace FTPUpload
{
    class Program
    {
        static void Main(string[] args)
        {          
            if (args.Length == 1)
            {
                Console.WriteLine("Carregando arquivo " + args[0] + ". Aguarde finalização do processo");

                bool Cotas = false;
                if (args[0] == "Cotas")
                {
                    Cotas = true;
                }                

                Class1.Upload(args[0],Cotas);
                Console.WriteLine("Arquivo " + args[0] + " enviado. Verifique atualização do site");
            }            
            else
            {
                Console.WriteLine("Nenhum arquivo informado. Informe nome do arquivo");
                Console.WriteLine("O arquivo informado deve estar na pasta C:\\NESTDATA\\");
                Console.WriteLine("Sintaxe: FTPUpload nome_do_arquivo\r\n");
                Console.WriteLine("Pressione ENTER para continuar...");
                Console.Read();
            }
        }
    }
}
