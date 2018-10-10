using System;
using System.Diagnostics;
using NUnit.Framework;

namespace tunitarios
{
    class Program
    {
        public static void Main(string[] args)
        {
            //var result = AddInts(3,4);
           // Console.WriteLine("Result: "+result);
           // Console.Read();

        }

        public static int AddInts(int x, int y)
        {
            return x+y;

        }

        public static bool logica(bool x, bool y)
        {
            return x && y; 
        }

    }
   
    [TestFixture]  // é uma classe de testes! e o NUNIT vai procurar por metodos publicos nessa classe que tenham atributo test ou TestCase
    class ProgramTests
    {
        // estudo...
        /* 
        [TestCase(2,4,6)]
        [TestCase(1,0,1)]
        [TestCase(10,-2,8)]
        public void should_return_sum(int x, int y, int z)
        {
            var result = Program.AddInts(x,y);
            Assert.AreEqual(z,result);
        }
        [TestCase(true,false,false)]
        [TestCase(true,true,true)]
        public void teria(bool e, bool z, bool r )
        {
            var result = Program.logica(e,z);
            Assert.AreEqual(r,result);
        }
        */
       

    }
    [TestFixture] // copiar essas classes testes depois para arquivos separados para INTEGRAR aos TESTS EDITOR DO UNITY
    class TabuleiroTestes
    {
        [Test]
        public void InicializaTabuleiroTeste()
        {
            
            var tabuleiro = new Tabuleiro();
            var total = 64; // total de casas
            var c = 0;
            //tabuleiro.InicializaCasas();
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
    [TestFixture] 
    class JogadorTeste // (100% só falta passar para o unity)
    {
        [TestCase('b',true)]
        [TestCase('p',false)] // verificar se todas as peças estao presentes
        public void InicializaPecasTeste(char cor, bool cima)
        {
           /* if(cima)
            {
                Console.WriteLine("testando com peça branca");
            }
            else
            {
                Console.WriteLine("testando com peça preta"); 
            }
            */
            var tabuleiro = new Jogador(cor,cima);
            var peao = 0;
            var bispo = 0;
            var cavalo = 0;
            var torre = 0;
            var rei = 0;
            var rainha = 0;
            foreach(Peca p in tabuleiro.conjuntoPecas)
            {
                    if(p is Peao)
                    {
                        peao = peao+1;
                    }
                    if(p is Bispo)
                    {
                        bispo = bispo+1;
                    }
                    if(p is Cavalo)
                    {
                        cavalo = cavalo+1;
                    }
                    if(p is Torre)
                    {
                        torre = torre+1;
                    }
                    if(p is Rei)
                    {
                        rei = rei+1;
                    }
                    if(p is Rainha)
                    {
                        rainha = rainha+1;
                    }

            }
            Assert.AreEqual(16,peao+bispo+cavalo+torre+rei+rainha);
        } 
    }

    [TestFixture]
    class CasaTeste
    {
        [TestCase(true,0,0)]
        [TestCase(false,0,0)]
      //  [TestCase(false,-1,-1)]
       // [TestCase(false,-1,-1), ExpectedException(typeof(ArgumentOutOfRangeException))] // nao existe na versao atual do NUnit
        
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
                    c.PecaAtual = pec;
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
     
    [TestFixture]
    class PecaTeste
    {
        public void TesteMover() // ainda pensando em um teste
        {

        }
        
    }

    [TestFixture] // nao tem muito que testar por enquanto...Só o construtor nao ta comentado
    class MovimentoTeste
    {

    }


}
