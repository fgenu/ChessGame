using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class MovimentoTestes {

   public void criasituacao()
    {

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


    //CASOS EM QUE VAI TER CAPTURA(DEPOIS)
    public void TesteListagem(int codpeca,bool sit,bool cap,bool xeq,int x1, int y1,int x2, int y2 ,int oraculotamanho)
    {
          Tabuleiro t = new Tabuleiro();
          Jogador j1 = new Jogador('b',true);
          Jogador j2 = new Jogador('p',false);
          if(xeq)
          {
                j1.inimigo = j2;
                j2.inimigo = j1;
          }
          
          int Tamanho = t.Tamanho; 
          
          // faz inicialização de peças similar ao que seria feito em partida! (garantir isolamento)
          for (int i = 0; i < Tamanho; i++)
          {
            t.tabuleiro[i, Tamanho - 1].ColocarPeca(j1.conjuntoPecas[i]);
            //coloca os peoes do jogador de cima
            if(!sit)
            {
                t.tabuleiro[i, Tamanho - 2].ColocarPeca(j1.conjuntoPecas[i + 8]);
            }
            

        //    Debug.Log(i+8);
        //    Debug.Log(j1.conjuntoPecas[i + 8].GetType());

            //coloca as pecas especiais jogador de baixo
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
             /*
             if(p != null){
                Debug.Log("aquiii");
                Debug.Log(p.GetType());
             }
             */
            
             
          }
          else
          {
             //throw ArgumentOutOfRangeException;
             Debug.Log("completamente invalido");
             Assert.AreEqual(false,true); // aborta de proposito caso o throw nao funcione kkkk

          }
          
          List<Movimento> saida;
          if(!sit && !xeq) // só deve verificar os casos de bloqueio e movimentos possiveis
          {
             saida = p.ListaMovimentos(verificaXeque: xeq , verificaCaptura: cap); // usando parametro default
            Debug.Log(p.GetType());
            //Assert.AreNotEqual(0,saida.Count);
            Assert.AreEqual(oraculotamanho,saida.Count); // peoes e cavalos na situação inicio só possuem 2 movimentos possíveis!
             
             
             
          }
          else
          {
                Debug.Log(p.GetType());
                criasituacao();
                saida = p.ListaMovimentos(verificaXeque: xeq , verificaCaptura: cap);
                  // fazer os asserts aqui (ainda estou pensando no que vou comparar alem do tamanho dos movimentos ...)
                Assert.AreEqual(oraculotamanho,saida.Count);

          }
          
          
        

            
    }

    //planejo "acoplar" a função de realiza movimento nesse teste ("pseudo teste de integração")
    // caminhos da função : i) tem peça no destino(vai realizar captura) -> nao (vá para ii)
    // ii) continua execução... e verifica se tem promoção do peao(se tem vá para iii) senao va para iv
    //iii) ativar a promoção do peao (estou pensando em como vou isolar essa parte da função )(pensando em stubs)
    //iv) fim 
    // parametros possíveis: 
    public void TesteRealiza()
    {
          
            
    }
}
