using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TabuleiroTestes {

  //  [Test]
  //  public void TabuleiroTestesSimplePasses() {
        // Use the Assert class to test conditions.
 //   }
//
     [Test]
        public void InicializaTabuleiroTeste()
        {
            
            var tabuleiro = new Tabuleiro();
            var total = 64; // total de casas
            var c = 0;
            //tabuleiro.InicializaCasas(); // remover depois
            for (int i = 0; i < 8; i++)
		    {
			    for (int j = 0; j < 8; j++)
			    {
				   Casa casa = tabuleiro.tabuleiro[i,j];
                   if(tabuleiro.tabuleiro[i,j] is Casa && casa.PosX == i && casa.PosY == j)
                   {
                       c = c +1;
                   }
			    }
		    }
            Assert.AreEqual(total,c);
            
        }
        
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
   // [UnityTest]
   // public IEnumerator TabuleiroTestesWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        //yield return null;
 //   }
}
