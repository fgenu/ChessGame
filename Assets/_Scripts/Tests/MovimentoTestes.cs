using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class MovimentoTestes {

   public void criasituacao(int x1, int y1, int x2, int y2, bool cap, bool xeq,bool peao,Tabuleiro t)
    {
        if(peao) // simplesmente remove uma peça do outro lado (para deixar ele ser promovido)
        {

        }


        if(cap)
        {
            Debug.Log("Caso de captura");
            // caso somente de captura (ou seja vai movimentar a peça de 1 para 2)
            Casa pe = t.tabuleiro[y1,x1];
            Peca e = pe.PopPeca();
            if(t.tabuleiro[y2,x2].EstaOcupada()) 
            {
                t.tabuleiro[y2,x2].PopPeca();
            }
            t.tabuleiro[y2,x2].ColocarPeca(e);


        }
        if(xeq && !cap)
        {
            Debug.Log("Caso de Xeque");
            // vai movimentar a peça x1,y1 para frente do rei  e a rainha inimiga para a posição de ataque x2 y2 
            

            Casa p1 = t.tabuleiro[y1,x1];
            Peca e1 = p1.PopPeca();
            
            
            Casa p2 = t.tabuleiro[3,7];
            Peca e2 = p2.PopPeca();

            if(t.tabuleiro[y2,x2].EstaOcupada()) 
            {
                t.tabuleiro[y2,x2].PopPeca();
            }
            t.tabuleiro[y2,x2].ColocarPeca(e1);
            int col;
            if(e1 is Bispo)
            {
                col = 5;
            }
            else
            {
                col = 4;
            }
            if(t.tabuleiro[col,6].EstaOcupada()) 
            {
                t.tabuleiro[col,6].PopPeca();
            }
            t.tabuleiro[col,6].ColocarPeca(e2);

        }
        
    }
    
    //planejo tentar isolar essa função do resto : mais especificamente isolar da função realiza movimento,

    // já que já temos um teste de inicialização de peças no jogador, e inicialização do tabuleiro
    // temos certeza (ou não) que essa funcionalidade não tem bugs!(essas não serão isoladas)(são estaveis)
    // no caso a função lista movimento usa também a função de xeque (da classe movimento e jogador, estou pensando numa forma de isolar elas...)
    //(apesar que essas são bem estaveis a essa altura do campeonato), acho vou isolar usando os parametros "default" de lista movimento


    // parametros: (codigo da peça) peca escolhida para testar a movimentação, se deve criar situação ou será o tabuleiro nas posições iniciais 
    // posições x e y das peças que serão manipuladas em cada situação, booleano de bloqueio(situação de peça bloqueada), booleano de captura(situação de captura)
    // e por ultimo um booleano de xeque que é para dizer que queremos uma situação de xeque (já que o rei perde movimentos nessa situação)
    
    // o oraculo: tamanho da lista de movimentos em dada situação, ainda estou pensando no resto... talvez numero de casas que ele andou , ou até a "direção do movimento" (ex bispo só é pela diagonal)
    // se for caso de captura se existe um movimento na lista que seja de captura, e caso de bloqueado um movimento na lista que seja bloqueado por outra peça
    
    //CASOS EM QUE O TABULEIRO ESTA DEFAULT
    //-subcasos em que vai ocorrer "bloqueio total" (sem movimentos possiveis)
    [TestCase(0,false,false,false,0,0,0,0,0)] // passando torre que NÃO PODE SE MOVER
    [TestCase(2,false,false,false,0,0,0,0,0)] // passando bispo que NÃO PODE SE MOVER
    [TestCase(3,false,false,false,0,0,0,0,0)] // passando rainha que NÃO PODE SE MOVER
    [TestCase(4,false,false,false,0,0,0,0,0)] // passando rei que NÃO PODE SE MOVER
    [TestCase(5,false,false,false,0,0,0,0,0)] // passando bispo que NÃO PODE SE MOVER
    [TestCase(7,false,false,false,0,0,0,0,0)] // passando torre que NÃO PODE SE MOVER

    //-subcasos em que vão existir 2 movimentos possíveis 
    [TestCase(1,false,false,false,0,0,0,0,2)] // passando cavalo que PODE SE MOVER
    [TestCase(6,false,false,false,0,0,0,0,2)] // passando cavalo que PODE SE MOVER
    [TestCase(8,false,false,false,0,0,0,0,2)] // passando peao que PODE SE MOVER

    //CASOS EM QUE O TABULEIRO NÃO ESTÁ DEFAULT (vou limpar todos os peões nesse caso para ajudar a movimentar as peças especiais)
    [TestCase(0,true,false,false,0,0,0,0,7)] // passando torre que  PODE SE MOVER
    [TestCase(2,true,false,false,0,0,0,0,7)] // passando bispo que  PODE SE MOVER
    [TestCase(3,true,false,false,0,0,0,0,14)] // passando rainha que  PODE SE MOVER
    [TestCase(4,true,false,false,0,0,0,0,3)] // passando rei que  PODE SE MOVER
    [TestCase(5,true,false,false,0,0,0,0,7)] // passando bispo que  PODE SE MOVER
    [TestCase(7,true,false,false,0,0,0,0,7)] // passando torre que  PODE SE MOVER


    //CASOS EM QUE VAI TER CAPTURA(basicamente se posiciona uma peça  qualquer inimiga alinhada com a escolhida )
    [TestCase(0,true,true,false,0,3,6,0,1)] // passando torre 
    [TestCase(1,true,true,false,0,3,5,0,3)] // passando cavalo 
    [TestCase(2,true,true,false,0,3,6,1,6)] // passando bispo 
    [TestCase(3,true,true,false,0,3,6,3,8)] // passando rainha 
    [TestCase(4,true,true,false,0,3,6,4,3)] // passando rei 
    [TestCase(8,false,true,true,0,3,5,1,1)] // passando peao
     
    //CASOS DE que o rei fica em xeque, (ou seja a peça tem um movimento a menos ou nenhum disponivel) 
    
    [TestCase(3,true,false,true,0,3,5,4,1)]
    [TestCase(3,true,false,true,0,2,4,7,2)]
    
    public void TesteListagem(int codpeca,bool sit,bool cap,bool xeq,int x1, int y1,int x2, int y2 ,int oraculotamanho)
    {
          Tabuleiro t = new Tabuleiro();
          Jogador j1 = new Jogador('b',true);
          Jogador j2 = new Jogador('p',false);
          if(xeq) // serve para garantir um certo isolamento da classe partida
          {
                j1.inimigo = j2;
                j2.inimigo = j1;

          }
          
          int Tamanho = t.Tamanho; 
        
          // faz inicialização de peças similar ao que seria feito em partida! (garantir isolamento)
          for (int i = 0; i < Tamanho; i++)
          {
            t.tabuleiro[i, Tamanho - 1].ColocarPeca(j1.conjuntoPecas[i]);
            //coloca os peoes do jogador de cima (mas somente se quisermos simular uma situação nao default)
            if(!sit || (cap & xeq))
            {
                t.tabuleiro[i, Tamanho - 2].ColocarPeca(j1.conjuntoPecas[i + 8]);
            }
    
            //coloca as pecas especiais jogador de baixo (mas somente se quisermos simular uma situação nao default)
           
            t.tabuleiro[i, 0].ColocarPeca(j2.conjuntoPecas[i]);
            //coloca os peoes do jogador de baixo
            if(!sit || (cap & xeq))
            {
               t.tabuleiro[i, 1].ColocarPeca(j2.conjuntoPecas[i + 8]);
            }
            



          }
          Peca p = null;
          if(codpeca < 15 )
          {
             p = j1.conjuntoPecas[codpeca];
             
           
             
            
             
          }
          else if(p == null)
          {
             throw new System.ArgumentException("Parametros de teste errados!");
             Debug.Log("completamente invalido");
             Assert.AreEqual(false,true); // aborta de proposito caso o throw nao funcione kkkk

          }
          
          List<Movimento> saida;
          if(!sit & !cap & !xeq) // só deve verificar os casos de bloqueio e movimentos possiveis
          {
            saida = p.ListaMovimentos(verificaXeque: xeq , verificaCaptura: cap); // usando parametro default
            Debug.Log(p.GetType());
            Debug.Log("Caso Default");
            //Assert.AreNotEqual(0,saida.Count);
            Assert.AreEqual(oraculotamanho,saida.Count); // peoes e cavalos na situação inicio só possuem 2 movimentos possíveis!
             
             
             
          }
          else // deve verificar os casos de captura e com xeque!
          {
                Debug.Log(p.GetType());
                criasituacao(x1,y1,x2,y2,cap,xeq,false,t);
              

                saida = p.ListaMovimentos(verificaXeque: xeq , verificaCaptura: cap);
                Assert.AreEqual(oraculotamanho,saida.Count);
                if(xeq && !cap)
                {
                    Debug.Log("A peça tem:"+saida.Count);
                    Debug.Log("possibildiades de movimentos");
                }
                bool resposta = false;
                if(cap)
                {
                        foreach(Movimento m in saida)
                        {
                            if(m.pecaCapturada != null)
                            {
                                resposta = true;
                                Debug.Log("Teve captura!");
                                break;
                                
                            }
                        }
                Assert.AreEqual(cap,resposta);
                }
                

          }
          
          
        

            
    }

    //planejo "acoplar" a função de realiza movimento nesse teste ("pseudo teste de integração")
    // caminhos da função : i) tem peça no destino(vai realizar captura) -> nao (vá para ii)
    // ii) continua execução... e verifica se tem promoção do peao(se tem vá para iii) senao va para iv
    //iii) ativar a promoção do peao (estou pensando em como vou isolar essa parte da função )(pensando em stubs)
    //iv) fim 
    // parametros possíveis:
    [TestCase(0,true,true,false,false,7,0,4,0)] // torre  
    [TestCase(0,true,false,false,true,7,0,4,0)] // torre com captura
    [TestCase(1,true,true,false,false,7,1,5,0)] // cavalo  
    [TestCase(1,true,false,false,true,7,1,5,0)] // cavalo com captura
    [TestCase(2,true,true,false,false,7,2,5,0)] // bispo  
    [TestCase(2,true,false,false,true,7,2,5,0)] // bispo com captura
    [TestCase(3,true,true,false,false,7,3,3,0)] // rainha  
    [TestCase(3,true,false,false,true,7,3,3,0)] // rainha com captura
    [TestCase(3,true,true,false,false,7,4,4,0)] // rei  
    [TestCase(3,true,false,false,true,7,4,4,0)] // rei com captura

    

    public void TesteRealiza(int codpeca,bool sit, bool comum, bool peao,bool cap, int i1, int i2, int f1, int f2)
    {
          Tabuleiro t = new Tabuleiro();
          Jogador j1 = new Jogador('b',true);
          Jogador j2 = new Jogador('p',false);
          j1.inimigo = j2;
          j2.inimigo = j1;
          int Tamanho = t.Tamanho; 
        
          // faz inicialização de peças similar ao que seria feito em partida! (garantir isolamento)
          for (int i = 0; i < Tamanho; i++)
          {
            t.tabuleiro[i, Tamanho - 1].ColocarPeca(j1.conjuntoPecas[i]);
            //coloca os peoes do jogador de cima (mas somente se quisermos simular uma situação nao default)
            if(!sit && !peao)
            {
                t.tabuleiro[i, Tamanho - 2].ColocarPeca(j1.conjuntoPecas[i + 8]);
            }
    
            //coloca as pecas especiais jogador de baixo (mas somente se quisermos simular uma situação nao default)
           
            t.tabuleiro[i, 0].ColocarPeca(j2.conjuntoPecas[i]);
            //coloca os peoes do jogador de baixo
            if(!sit)
            {
               t.tabuleiro[i, 1].ColocarPeca(j2.conjuntoPecas[i + 8]);
            }
            



          }
          Peca p = null;
          if(codpeca < 15 )
          {
             p = j1.conjuntoPecas[codpeca];
                 
          }
          else if(p == null)
          {
             throw new System.ArgumentException("Parametros de teste errados!");
             Debug.Log("completamente invalido");
             Assert.AreEqual(false,true); // aborta de proposito caso o throw nao funcione kkkk

          }


          Movimento m = new Movimento(t.tabuleiro[f2,f1],t.tabuleiro[i2,i1]);
          if(comum)
          {
               
                p.RealizaMovimento(m,true);
                Assert.IsNull(t.tabuleiro[i2,i1].PecaAtual);
                Assert.IsNotNull(t.tabuleiro[f2,f1].PecaAtual);
                 Debug.Log("Movimento comum!");
          
          }
          if(cap)
          {
              
              Casa ce = t.tabuleiro[3,0];
              Peca pe = ce.PopPeca(); // peça capturada pela nossa
              if(t.tabuleiro[f2,f1].EstaOcupada()) 
              {
                t.tabuleiro[f2,f1].PopPeca();
              }
              t.tabuleiro[f2,f1].ColocarPeca(pe);
              
              p.RealizaMovimento(m,true);
              Debug.Log("Movimento com captura!");
             
          }
          
            
    }
}
