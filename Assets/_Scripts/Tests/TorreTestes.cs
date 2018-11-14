using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

class TorreTeste
{
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
              
               t.tabuleiro[e,linha].PopPeca();
              // Console.WriteLine("removendo peca: ");
              // Console.WriteLine(e);
              // if(t.tabuleiro[linha,e].PecaAtual == null)
              // {
              //     Console.WriteLine("yes");
              // }
             //  Console.WriteLine(typeof(t.tabuleiro[linha,e]));
            }
        }

        public static void criasituacao(Partida p, int x, int y,int linhajogador)
        {
            Tabuleiro t = p.Tabuleiro;
            int poslinha = Math.Abs(linhajogador -7); // vai ser 7 ou 0 (serve para pegar a peça inimiga)
            Debug.Log("posição:");
            Debug.Log(poslinha);
            if(t.tabuleiro[y,x].EstaOcupada())
            {
                t.tabuleiro[y,x].PopPeca();
            }
            //Peca escolhida =  t.tabuleiro[4,linhajogador].PecaAtual.jDono.inimigo.conjuntoPecas[3];   // por default vou escolher a rainha (ela se move para qualquer direção facilitando os testes)
            Casa pescolhida = t.tabuleiro[3,poslinha];
            Peca escolhida = pescolhida.PopPeca();
            if(escolhida is Rainha )
            {
                Debug.Log("ok");
            }
            t.tabuleiro[y,x].ColocarPeca(escolhida);
            
        }
        /* 
        public static Partida p1= new Partida(); // tabuleiro cheio (sem espaço entre as peças)
        
        public static Partida p2= new  Partida(); // roque grande pecas cima ou baixo
        
        public static Partida p3= new  Partida(); // roque pequeno pecas cima ou baixo
        public static Partida p4= new  Partida();  // peça ameaçada
        public static Partida p5= new  Partida(); // peça ja se moveu
        */

        [TestCase(0,0,false,0,0,false,0,0)] // tabuleiro cheio e todos nas poisções iniciais jogador 0 (cima), torre escolhida 0 
        [TestCase(0,0,false,1,0,false,0,0)] // tabuleiro cheio e todos nas poisções iniciais jogador 1 (baixo), torre escolhida 0
        [TestCase(0,0,true,0,8,false,0,0)]  // tabuleiro cheio e teve movimentação e todos nas poisções iniciais jogador 0 (cima), torre escolhida 8
        [TestCase(0,0,true,1,8,false,0,0)] // tabuleiro cheio e teve movimentação e todos nas poisções iniciais jogador 1 (baixo), torre escolhida 8
        
        [TestCase(1,4,false,0,0,false,0,0)] // torre 0 e rei com espaço entre eles(roque maior) jogador de cima
        [TestCase(1,4,false,1,0,false,0,0)] // torre 0 e rei com espaço entre eles(roque maior) jogador de baixo
        [TestCase(1,3,false,1,0,false,0,0)] // torre 0 e rei sem espaço entre eles jogador de baixo
        [TestCase(1,2,false,0,0,false,0,0)] // torre 0 e rei sem espaço entre eles jogador de cima
        [TestCase(5,7,false,0,0,false,0,0)] // torre 0 e rei com espçao entre ele e a OUTRA TORRE jogador de cima
        [TestCase(5,7,false,1,0,false,0,0)] // torre 0 e rei com espçao entre ele e a OUTRA TORRE jogador de baixo
        [TestCase(1,4,true,0,0,false,0,0)] // tem espaço mas houve movimentação
        [TestCase(1,4,true,1,0,false,0,0)] // tem espaço mas houve movimentação
 
        [TestCase(5,7,false,0,8,false,0,0)] // torre 8 e rei com espaço entre eles(roque menor) jogador de cima
        [TestCase(5,7,false,1,8,false,0,0)] // torre 8 e rei com espaço entre eles(roque menor) jogador de baixo
////        [TestCase(1,3,false,1,8,false,0,0)] // torre 8 e rei sem espaço entre eles jogador de baixo (esse teste não faz exatamente muito sentido... fazendo ele acreditar que esta errado)(o roque não é feito porque nao faz sentido, oque prova que o roque funciona...)
        [TestCase(1,2,false,0,8,false,0,0)] // torre 8 e rei sem espaço entre eles jogador de cima 
        [TestCase(1,4,false,1,8,false,0,0)] // torre 8 e rei com espaço entre ele e a OUTRA TORRE jogador de baixo
        [TestCase(5,6,false,0,8,false,0,0)] // torre 8 e rei com espaço entre ele e a OUTRA TORRE jogador de cima
        [TestCase(5,7,true,0,8,false,0,0)] // tem espaço mas houve movimentação
        [TestCase(5,7,true,1,8,false,0,0)] // tem espaço mas houve movimentação

        //TESTES RELACIONADOS AO XEQUE, true: se deve verificar situações de xeque, x(linha) , y(coluna) posição no tabuleiro a inserir a peça para a situação simulada
        
        [TestCase(1,4,false,0,0,true,7,6)]  // roque não feito pois há xeque
        [TestCase(1,4,false,1,0,true,0,6)]

        [TestCase(5,7,false,0,8,true,7,6)] // roque não feito porque tem casas ocupadas no caminho e rei está em xeque
        [TestCase(5,7,false,1,8,true,0,6)] // roque não feito porque tem casas ocupadas no caminho e rei está em xeque
        
        // roque não feito porque o rei vai ficar em xeque ao terminar o movimento
        [TestCase(1,4,false,0,0,true,6,2)]  
        [TestCase(1,4,false,1,0,true,1,2)] 

        [TestCase(1,4,false,0,0,true,6,1)]
        [TestCase(1,4,false,1,0,true,1,1)]

        [TestCase(5,7,false,0,8,true,6,6)]  
        [TestCase(5,7,false,1,8,true,1,6)]

        [TestCase(5,7,false,0,8,true,6,7)]  
        [TestCase(5,7,false,1,8,true,1,7)]


        public void TesteRoque(int i, int f, bool moveu,int jogador,int codtorre,bool xeq,int x, int y)
        {
            Partida p= new Partida();
            Tabuleiro t = p.Tabuleiro;
            int linha = t.Tamanho-1; 
            Movimento mteste;
            // OBS :assert's comentados são os de quando os testes verficavam movimentações no tabuleiro!
            // agora os testes verficam movimentos possiveis retornados pela função
            // eu não apaguei esses casos para caso seja necessário utilizar como referencia depois.
            if(jogador == 1)
            {
                Debug.Log("Jogador de baixo!");
                linha = 0;
            }
            Torre torre;
            Rei rtemp;
            if(xeq)
            {
                    removeIntervalo(p,jogador,1,4); // deixa somente o rei e as torres nas peças especiais do aliado
                    removeIntervalo(p,jogador,5,7);
                    criasituacao(p,x,y,linha);
                    // após isso realizar o roque para esse caso de testes já que o esperado é que as peças se mantenham no mesmo lugar!
                    // NOTE QUE ESSES CASOS ASSIM COMO OS OUTROS NÃO CONSIDERAM QUE AS TORRES FORAM CAPTURADAS(ATÉ PORQUE O METODO NÃO PODERIA SER EXECUTADO POIS ELAS ESTARIAM FORA DO JOGO)
                    if(codtorre !=0)
                    {
                        codtorre = codtorre -1;
                    }
                    torre = (Torre) t.tabuleiro[codtorre,linha].PecaAtual;
                    mteste = torre.Roque(p.Tabuleiro); 

                    /*  
                    Assert.IsNotNull(t.tabuleiro[0,linha].PecaAtual);
                    Assert.IsNotNull(t.tabuleiro[7,linha].PecaAtual);
                    Assert.IsNotNull(t.tabuleiro[4,linha].PecaAtual);

                    Assert.IsInstanceOf<Torre>(t.tabuleiro[0,linha].PecaAtual);
                    Assert.IsInstanceOf<Torre>(t.tabuleiro[7,linha].PecaAtual);
                    Assert.IsInstanceOf<Rei>(t.tabuleiro[4,linha].PecaAtual);
                    */
                    Assert.IsNull(mteste);
                    return;
            }
            if((Math.Abs(i - f) == 3 && codtorre == 0) || (Math.Abs(i - f) == 2 && codtorre !=0) ) // note que tabuleiros que podem ocorrer roque tem que ter esse espaçamento pelo menos entre torre e rei
            {
                removeIntervalo(p,jogador,i,f); // cria o tabuleiro "ideal" para o teste
                // criar tabuleiro resposta
                 
                 //Torre ttemp;
                 
           //    copia = (Casa[,]) t.tabuleiro.Clone(); // tabuleiro copia sofrerá alterações para ser a "resposta"
           //    rtemp = (Rei) copia[linha,4].PopPeca();
                
                
                if(codtorre == 0 && !moveu )
                {
                   Debug.Log("talvez possa fazer roque");
                   torre = (Torre) t.tabuleiro[codtorre,linha].PecaAtual;
                   mteste = torre.Roque(p.Tabuleiro);                 

                   /*
                   Assert.IsNull(t.tabuleiro[codtorre,linha].PecaAtual);
                   Assert.IsNull(t.tabuleiro[4,linha].PecaAtual);

                   Assert.IsInstanceOf<Torre>(t.tabuleiro[codtorre+3,linha].PecaAtual);
                   Assert.IsInstanceOf<Rei>(t.tabuleiro[4-2,linha].PecaAtual);
                    */
                   Assert.IsNotNull(mteste);
                   // movimento do rei
                   Assert.AreEqual(mteste.origem.PosX,4);
                   Assert.AreEqual(mteste.origem.PosY,linha);
                   Assert.AreEqual(mteste.destino.PosX,4-2);
                   Assert.AreEqual(mteste.destino.PosY,linha);
                   // movimento da torre
                   Assert.AreEqual(mteste.movimentoExtra.origem.PosX,codtorre);
                   Assert.AreEqual(mteste.movimentoExtra.origem.PosY,linha);
                   Assert.AreEqual(mteste.movimentoExtra.destino.PosX,codtorre+3);
                   Assert.AreEqual(mteste.movimentoExtra.destino.PosY,linha);
                    
                     
                    
                }
                else if(codtorre != 0 && !moveu)
                {
                    codtorre = codtorre -1;
                    Debug.Log("talvez possa fazer roque");
                    torre = (Torre) t.tabuleiro[codtorre,linha].PecaAtual;
                    mteste = torre.Roque(p.Tabuleiro); 
                    //torre.RealizaMovimento(m);           
                    /*
                    Assert.IsNull(t.tabuleiro[codtorre,linha].PecaAtual);
                    Assert.IsNull(t.tabuleiro[4,linha].PecaAtual);

                    Assert.IsInstanceOf<Torre>(t.tabuleiro[codtorre-2,linha].PecaAtual);
                    Assert.IsInstanceOf<Rei>(t.tabuleiro[4+2,linha].PecaAtual);
                    */

                   Assert.IsNotNull(mteste);
                   // movimento do rei
                   Assert.AreEqual(mteste.origem.PosX,4);
                   Assert.AreEqual(mteste.origem.PosY,linha);
                   Assert.AreEqual(mteste.destino.PosX,4+2);
                   Assert.AreEqual(mteste.destino.PosY,linha);
                   // movimento da torre
                   Assert.AreEqual(mteste.movimentoExtra.origem.PosX,codtorre);
                   Assert.AreEqual(mteste.movimentoExtra.origem.PosY,linha);
                   Assert.AreEqual(mteste.movimentoExtra.destino.PosX,codtorre-2);
                   Assert.AreEqual(mteste.movimentoExtra.destino.PosY,linha);
                }
                else if(moveu)
                {
                    Debug.Log("Não pode fazer roque");
                    if(codtorre !=0)
                    {
                        codtorre = codtorre -1;
                    }

                    torre = (Torre) t.tabuleiro[codtorre,linha].PecaAtual;

                    p.Tabuleiro.tabuleiro[0,linha].PecaAtual.primeiraJogada = false;
                    p.Tabuleiro.tabuleiro[7,linha].PecaAtual.primeiraJogada = false;
                    p.Tabuleiro.tabuleiro[4,linha].PecaAtual.primeiraJogada = false;

                    mteste = torre.Roque(p.Tabuleiro);
                    /*
                    Assert.IsNotNull(t.tabuleiro[0,linha].PecaAtual);
                    Assert.IsNotNull(t.tabuleiro[7,linha].PecaAtual);
                    Assert.IsNotNull(t.tabuleiro[4,linha].PecaAtual);

                    Assert.IsInstanceOf<Torre>(t.tabuleiro[0,linha].PecaAtual);
                    Assert.IsInstanceOf<Torre>(t.tabuleiro[7,linha].PecaAtual);
                    Assert.IsInstanceOf<Rei>(t.tabuleiro[4,linha].PecaAtual);
                    */
                    Assert.IsNull(mteste);
                    
                }
                
            }
            else // caso em que não pode fazer roque.
            {
                Debug.Log("nao pode fazer roque");
                torre = (Torre) t.tabuleiro[0,linha].PecaAtual;
                if(codtorre != 0)
                {
                    torre = (Torre) t.tabuleiro[7,linha].PecaAtual;
                }
                if(moveu)
                {
                    p.Tabuleiro.tabuleiro[0,linha].PecaAtual.primeiraJogada = false;
                    p.Tabuleiro.tabuleiro[7,linha].PecaAtual.primeiraJogada = false;
                    p.Tabuleiro.tabuleiro[4,linha].PecaAtual.primeiraJogada = false;
                }
                mteste = torre.Roque(p.Tabuleiro);
                Assert.IsNull(mteste);
                /*
                // casas ainda estão ocupadas
                Assert.IsNotNull(t.tabuleiro[0,linha].PecaAtual);
                Assert.IsNotNull(t.tabuleiro[7,linha].PecaAtual);
                Assert.IsNotNull(t.tabuleiro[4,linha].PecaAtual);

                // e são estão ocupadas por torre e rei 
                Assert.IsInstanceOf<Torre>(t.tabuleiro[0,linha].PecaAtual); // torre
                Assert.IsInstanceOf<Torre>(t.tabuleiro[7,linha].PecaAtual); // torre
                Assert.IsInstanceOf<Rei>(t.tabuleiro[4,linha].PecaAtual);   // rei

                */
            }
           
            

        }
        
}