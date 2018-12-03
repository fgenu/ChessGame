using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TabuleiroTestes {

 
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
        
   
}
