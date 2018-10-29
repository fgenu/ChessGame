using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

public class TorreTeste {

     public static void removeIntervalo(Partida p,int jogador,int i, int f)
        {
            
            Tabuleiro t = p.Tabuleiro;
            Jogador j = p.JogadorDeCima();
            int linha = t.Tamanho-1; 
            if(jogador == 1)
            {
                 j = p.JogadorDeBaixo();
                 linha = 0;
            }
            for(int e=i; e < f ;e++)
		    {
			  
               t.tabuleiro[linha,e].PopPeca();
              // Console.WriteLine("removendo peca: ");
              // Console.WriteLine(e);
              // if(t.tabuleiro[linha,e].PecaAtual == null)
              // {
              //     Console.WriteLine("yes");
              // }
             //  Console.WriteLine(typeof(t.tabuleiro[linha,e]));
		    }
        }
        /* 
        public static Partida p1= new Partida(); // tabuleiro cheio (sem espaço entre as peças)
        
        public static Partida p2= new  Partida(); // roque grande pecas cima ou baixo
        
        public static Partida p3= new  Partida(); // roque pequeno pecas cima ou baixo
        public static Partida p4= new  Partida();  // peça ameaçada
        public static Partida p5= new  Partida(); // peça ja se moveu
        */
        [TestCase(0,0,false,0,0)] // tabuleiro cheio e todos nas poisções iniciais jogador 0 (cima), torre escolhida 0 
        [TestCase(0,0,false,1,0)] // tabuleiro cheio e todos nas poisções iniciais jogador 1 (baixo), torre escolhida 0
        [TestCase(0,0,true,1,0)]  // tabuleiro cheio e teve movimentação e todos nas poisções iniciais jogador 0 (cima), torre escolhida 8
        [TestCase(0,0,true,1,8)] // tabuleiro cheio e teve movimentação e todos nas poisções iniciais jogador 1 (baixo), torre escolhida 8
        
        [TestCase(1,4,false,0,0)] // torre 0 e rei com espaço entre eles(roque maior) jogador de cima
        [TestCase(1,4,false,1,0)] // torre 0 e rei com espaço entre eles(roque maior) jogador de baixo
        [TestCase(1,3,false,1,0)] // torre 0 e rei sem espaço entre eles jogador de cima
        [TestCase(1,2,false,0,0)] // torre 0 e rei sem espaço entre eles jogador de baixo
        [TestCase(1,4,true,0,0)] // tem espaço mas houve movimentação
        [TestCase(1,4,true,1,0)] // tem espaço mas houve movimentação

        [TestCase(6,7,false,0,8)] // torre 8 e rei com espaço entre eles(roque menor) jogador de cima
        [TestCase(6,7,false,1,8)] // torre 8 e rei com espaço entre eles(roque menor) jogador de baixo
        [TestCase(1,4,false,1,8)] // torre 8 e rei sem espaço entre eles jogador de cima
        [TestCase(1,4,false,0,8)] // torre 8 e rei sem espaço entre eles jogador de baixo
        [TestCase(6,7,true,0,8)] // tem espaço mas houve movimentação
        [TestCase(6,7,true,1,8)] // tem espaço mas houve movimentação


        public void TesteRoque(int i, int f, bool moveu,int jogador,int codtorre)
        {
            Partida p= new Partida();
            Tabuleiro t = p.Tabuleiro;
            int linha = t.Tamanho-1; 
            if(jogador == 1)
            {
                linha = 0;
            }
            Torre torre;
            Rei rtemp;
            if(i - f == Math.Abs(3) || i - f == Math.Abs(2) )
            {
                removeIntervalo(p,jogador,i,f); // cria o tabuleiro "ideal" para o teste
                // criar tabuleiro resposta
                 
                 //Torre ttemp;
                 
           //    copia = (Casa[,]) t.tabuleiro.Clone(); // tabuleiro copia sofrerá alterações para ser a "resposta"
           //    rtemp = (Rei) copia[linha,4].PopPeca();
                
                
                if(codtorre == 0 && !moveu )
                {
                    
                   torre = (Torre) t.tabuleiro[linha,codtorre].PecaAtual;
                   torre.Roque(p.Tabuleiro);                 

                    Assert.IsNull(t.tabuleiro[linha,codtorre].PecaAtual);
                    Assert.IsNull(t.tabuleiro[linha,4].PecaAtual);

                    Assert.IsInstanceOf<Torre>(t.tabuleiro[linha,codtorre+3].PecaAtual);
                    Assert.IsInstanceOf<Rei>(t.tabuleiro[linha,4-2].PecaAtual);

             
                    
                     
                    
                }
                else if(codtorre != 0 && !moveu)
                {
                    torre = (Torre) t.tabuleiro[linha,codtorre-1].PecaAtual;
                    torre.Roque(p.Tabuleiro);            

                    Assert.IsNull(t.tabuleiro[linha,codtorre].PecaAtual);
                    Assert.IsNull(t.tabuleiro[linha,4].PecaAtual);

                    Assert.IsInstanceOf<Torre>(t.tabuleiro[linha,codtorre-2].PecaAtual);
                    Assert.IsInstanceOf<Rei>(t.tabuleiro[linha,4+2].PecaAtual);
                }
                else if(moveu)
                {
                    torre = (Torre) t.tabuleiro[linha,0].PecaAtual;

                    p.Tabuleiro.tabuleiro[linha,0].PecaAtual.primeiraJogada = false;
                    p.Tabuleiro.tabuleiro[linha,7].PecaAtual.primeiraJogada = false;
                    p.Tabuleiro.tabuleiro[linha,4].PecaAtual.primeiraJogada = false;

                    torre.Roque(p.Tabuleiro);

                    Assert.IsNotNull(t.tabuleiro[linha,0].PecaAtual);
                    Assert.IsNotNull(t.tabuleiro[linha,7].PecaAtual);
                    Assert.IsNotNull(t.tabuleiro[linha,4].PecaAtual);

                    Assert.IsInstanceOf<Torre>(t.tabuleiro[linha,0].PecaAtual);
                    Assert.IsInstanceOf<Torre>(t.tabuleiro[linha,7].PecaAtual);
                    Assert.IsInstanceOf<Rei>(t.tabuleiro[linha,4].PecaAtual);

                    
                }
                
            }
            else
            {
                torre = (Torre) t.tabuleiro[linha,0].PecaAtual;
                if(codtorre != 0)
                {
                    torre = (Torre) t.tabuleiro[linha,7].PecaAtual;
                }
                torre.Roque(p.Tabuleiro);
                Assert.IsNotNull(t.tabuleiro[linha,0].PecaAtual);
                Assert.IsNotNull(t.tabuleiro[linha,7].PecaAtual);
                Assert.IsNotNull(t.tabuleiro[linha,4].PecaAtual);

                
                    Assert.IsInstanceOf<Torre>(t.tabuleiro[linha,0].PecaAtual);
                    Assert.IsInstanceOf<Torre>(t.tabuleiro[linha,7].PecaAtual);
                    Assert.IsInstanceOf<Rei>(t.tabuleiro[linha,4].PecaAtual);

            }
           
            

        }
}
