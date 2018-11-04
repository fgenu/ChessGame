using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class JogadorTestes {

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
