using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServiceExternoUnitTests
{
    class Program
    {
        

        static void Main()
        {
            WSNotificacaoUnitTests.TesteComunicacao zzz = new WSNotificacaoUnitTests.TesteComunicacao();

            zzz.TestaHelloWorld();

            CarteiraServiceTest curTest = new CarteiraServiceTest();
            curTest.PosicaoCarteiraTest();
        }

    }
}
