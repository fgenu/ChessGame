using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CasaTestes {

    [TestCase(true,0,0)]
    [TestCase(false,0,0)]
    //[TestCase(false,-1,-1)]  // teste nao valido
    //[TestCase(false,-1,-1), ExpectedException(typeof(ArgumentOutOfRangeException))] // nao existe na versao atual do NUnit
    public void TesteOcupado(bool p,int x, int y)
    {
            Casa c = null;
            Jogador j1 = null;
            Peca pec = null;
            Tabuleiro t = new Tabuleiro();
            try
            {
                c = new Casa(x,y,t);
                if(p)
                {
                    j1 = new Jogador('b', true);
                    pec = new Peca(j1);
                    //c.PecaAtual = pec;
                    c.ColocarPeca(pec);
                    Assert.AreEqual(p,c.EstaOcupada()); 
                }
                else
                {
                    Assert.AreEqual(p,c.EstaOcupada());
                }
            }catch(ArgumentOutOfRangeException e)
            {
               //Log.Error("Garantia de tratamento",e);
              //  Console.WriteLine("Garantia de Tratamento");
                //TestContext.Write("Garantia de Tratamento");
               // Debug.WriteLine("Garantia de Tratamento");
                if (e.Source != null)  
                    Console.WriteLine("Garantia de catch: {0}", e.Source);  
                throw;  
              //  Assert.Throws<ArgumentOutOfRangeException>(delegate()
             //   {
             //       Console.WriteLine("Garantia de catch: {0}", e.Source);
             //       throw ;
            //    });
            }
            
           
            
        }
}
