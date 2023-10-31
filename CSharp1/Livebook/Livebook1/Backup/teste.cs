using System;
using System.Collections.Generic;
using System.Text;



namespace SGN.Teste1
{
    public class Teste
    {
        public void Testando(object algo)
        {
            // Para simular a execu��o, vamos criar algo
            // que demore um pouco para executar. Neste for
            // eu fa�o a thread parar 1 segundo por itera��o
            for (int i = 0; i < 10; i++)
            {
                // Isso aqui � para irmos acompanhando a execu��o
                // na janela Output do VS...
                Console.WriteLine(algo.ToString() + " - Passo " + i.ToString());
                System.Threading.Thread.Sleep(1000);
            }
        }

    }
}
