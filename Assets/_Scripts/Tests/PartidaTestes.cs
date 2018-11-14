using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class PartidaTestes {

// 3 7

                                                        // 0 ou 7           4
    public static void removeintervalo(Tabuleiro t, int x, int y, int linhajogador, int colunajogador,int modo)
    {
        
        
        int iniciox, inicioy;
        int fimx,fimy,dif;
        if(x > linhajogador)
        {
            iniciox = linhajogador;
            fimx = x;
        } 
        else
        {
            iniciox = x;
            fimx = linhajogador;
        }

        if(y > colunajogador)
        {
            inicioy = colunajogador;
            fimy = y;
        } 
        else
        {
            inicioy = y;
            fimy = colunajogador;
        }
        if(modo == 1)
        {
          //  Debug.Log("???????????????????????");
            dif = Math.Abs(linhajogador-x);
            if(dif != Math.Abs(y-colunajogador) )
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA FUDEU");
            }
            iniciox = iniciox +1;
            inicioy = inicioy +1;
            // isso é para evitar remover as peças que queremos usar na simulação
            while(iniciox < fimx  && inicioy < fimy)
            {
              //  Debug.Log("removendo...");
              //  Debug.Log(inicioy);
              //  Debug.Log(iniciox);
                if(t.tabuleiro[inicioy,iniciox].EstaOcupada())
                {
                    t.tabuleiro[inicioy,iniciox].PopPeca();
                }
                iniciox = iniciox +1;
                inicioy = inicioy +1;
            }   
        }
        else
        {
             // deve-se remover o intervalo entre essa peça e o rei horizontalmente e verticalmente
            for(int i=iniciox+1; i < fimx ;i++)
            {
                 if(t.tabuleiro[colunajogador,i].EstaOcupada())
                 {
                    t.tabuleiro[colunajogador,i].PopPeca();
                 }
            }

            for(int i=inicioy+1; i < fimy ;i++)
            {
                if(t.tabuleiro[i,linhajogador].EstaOcupada())
                 {
                    t.tabuleiro[i,linhajogador].PopPeca();
                 }       
            }
        
        }

        
       
    }

    public static void pegaEocupa(Tabuleiro t, int x, int y , int poslinha , int codpeca)
    {
        if(x == 0 && y == 0)
        {
            Debug.Log("entendido não considerarei...");
            return;
        } 
        Casa pe = t.tabuleiro[codpeca,poslinha];
        Peca e = pe.PopPeca();
        if(t.tabuleiro[y,x].EstaOcupada()) 
        {
                t.tabuleiro[y,x].PopPeca();
        }
        if(codpeca == 3 && e is Rainha )
        {
                    Debug.Log("ok queen");
        }
        if((codpeca == 0 || codpeca == 7) && e is Torre )
        {
                    Debug.Log("ok torre");
        }
        if((codpeca == 2 || codpeca == 5) && e is Bispo )
        {
                    Debug.Log("ok bispo");
        }
        t.tabuleiro[y,x].ColocarPeca(e);
    }

    public static void criasituacao(Partida p, int x1, int y1, int x2, int y2, int x3, int y3, int linhajogador)
    {
           
            int colunajogador = 4; // posição do rei na sua linha
            Tabuleiro t = p.Tabuleiro;
            int poslinha = Math.Abs(linhajogador -7); // vai ser 7 ou 0 (serve para pegar a peça inimiga)
            
            removeintervalo(t,x1,y1,linhajogador,colunajogador,0);
            removeintervalo(t,x2,y2,linhajogador,colunajogador,1);
            removeintervalo(t,x3,y3,linhajogador,colunajogador,0);

            pegaEocupa(t,x1,y1,poslinha,3);
            pegaEocupa(t,x2,y2,poslinha,2);
            pegaEocupa(t,x3,y3,poslinha,0);

            
            
            
            
           
            
    }
    // note que passar false e algum atributo garante uma assertion incorreta.
    // note que passar true e não passar pelo menos 2 posições para xeque mate torna a assertion incorreta.
    [TestCase(false,0,0,0,0,0,0,0)] // esse caso teste é o caso "passe direto" logo vai falhar   
    [TestCase(true,0,0,0,3,7,0,6)] 
    public void TesteVitoria(bool acaba, int linhajogador, int x1, int y1, int x2, int y2, int x3, int y3)
    {
       Partida p = new Partida();
       if(acaba)
       {
          criasituacao(p, x1, y1, x2, y2, x3, y3, linhajogador); // função usada para cercar o rei com até 3 diferentes peças (rainha, bispo , torre e simular)
       }
       
       Tabuleiro t = p.Tabuleiro;
       
       // testando se o problema é a simulação...
  //   Debug.Log(t.tabuleiro[7,3].PecaAtual is Bispo);  
  //    Debug.Log(t.tabuleiro[6,0].PecaAtual is Torre);

  //    Debug.Log(t.tabuleiro[4,1].EstaOcupada());  
  //  Debug.Log(t.tabuleiro[4,0].PecaAtual.jDono.Cor);

  //      Debug.Log(t.tabuleiro[5,1].EstaOcupada());
  //      Debug.Log(t.tabuleiro[6,2].EstaOcupada());

  //    Debug.Log(t.tabuleiro[4,0].PecaAtual.jDono.Cor);
  //    Debug.Log(t.tabuleiro[7,3].PecaAtual.jDono.Cor);
  //    Debug.Log(t.tabuleiro[6,0].PecaAtual.jDono.Cor);
       p.Turno = 2; // usado para testes temporariamente (para uma situação de "inicio de jogo" (acabei de dar new partida)) seria obrigado passar o turno 2 vezes para ele verificar 
       p.PassarAVez();
       



     //testando se o problema é a função de vitoria mesmo... (SIM O PROBLEMA É NA FUNÇÃO DE VITORIA)?

       if(acaba)
       {
         Assert.IsTrue(p.fim);
       }
       else
       {
         Debug.Log("yare yare...");
         Assert.IsFalse(p.fim);
       }
       
    


    }





/*

    [Test]
    public void PartidaTestesSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator PartidaTestesWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }

*/

}
