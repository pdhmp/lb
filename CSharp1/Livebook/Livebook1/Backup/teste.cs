using System;
using System.Collections.Generic;
using System.Text;



namespace SGN.Teste1
{
    public class Teste
    {
        public void Testando(object algo)
        {
            // Para simular a execução, vamos criar algo
            // que demore um pouco para executar. Neste for
            // eu faço a thread parar 1 segundo por iteração
            for (int i = 0; i < 10; i++)
            {
                // Isso aqui é para irmos acompanhando a execução
                // na janela Output do VS...
                Console.WriteLine(algo.ToString() + " - Passo " + i.ToString());
                System.Threading.Thread.Sleep(1000);
            }
        }

    }
}
