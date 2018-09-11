using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabuleiro : MonoBehaviour {
    static int tamTabuleiro = 8;
    Casa[,] tabuleiro = new Casa[tamTabuleiro, tamTabuleiro];

    //função serve para pintar a casa de branco ou preto
    void pintaCasas() {
        //pinta linhas de sequencia branco, preto, branco etc
        for (int i = 0; i < tamTabuleiro; i += 2) {
            for (int j = 0; j < tamTabuleiro; j++) {
                if (j % 2 == 0)
                {
                    tabuleiro[i, j].cor = 'b';
                }
                else
                {
                    tabuleiro[i, j].cor = 'p';
                }
            }
        }
        //pinta as linhas de sequencia preto, branco,preto etc
        for (int i = 1; i < tamTabuleiro; i += 2)
        {
            for (int j = 0; j < tamTabuleiro; j++)
            {
                if (j % 2 != 0)
                {
                    tabuleiro[i, j].cor = 'b';
                }
                else
                {
                    tabuleiro[i, j].cor = 'p';
                }
            }
        }
    }
    void inserePecas(Jogador j1, Jogador j2)
    {
        //coloca as peças dos jogadores no tabuleiro
        for(int i = 0; i < tamTabuleiro; i++)
        {
            //coloca as pecas especiais do primeiro jogador
            tabuleiro[tamTabuleiro - 1, i].pecaAtual = j1.conjuntoPecas[i];
            j1.conjuntoPecas[i].posX = tamTabuleiro - 1;
            j1.conjuntoPecas[i].posY = i;

            //coloca os peoes do primeiro jogador
            tabuleiro[tamTabuleiro - 2, i].pecaAtual = j1.conjuntoPecas[i+8];
            j1.conjuntoPecas[i+8].posX = tamTabuleiro - 2;
            j1.conjuntoPecas[i+8].posY = i;

            //coloca as pecas especiais do segundo jogador
            tabuleiro[0, i].pecaAtual = j2.conjuntoPecas[i];
            j2.conjuntoPecas[i].posX = 0;
            j2.conjuntoPecas[i].posY = i;

            //coloca os peoes do segundo jogador
            tabuleiro[1, i].pecaAtual = j2.conjuntoPecas[i + 8];
            j2.conjuntoPecas[i+8].posX = 1;
            j2.conjuntoPecas[i+8].posY = i;
        }
        
    }
    void printaTabuleiro()
    {
        string linha = "";
        for (int i = 0; i < tamTabuleiro; i++)
        {
            for(int j = 0; j < tamTabuleiro; j++)
            {
                if (tabuleiro[i, j].pecaAtual is Torre)
                {
                    linha += " T";
                }
                else if (tabuleiro[i, j].pecaAtual is Cavalo)
                {
                    linha += " C";
                }
                else if (tabuleiro[i, j].pecaAtual is Bispo)
                {
                    linha += " B";
                }
                else if (tabuleiro[i, j].pecaAtual is Rei)
                {
                    linha += " E";
                }
                else if (tabuleiro[i, j].pecaAtual is Rainha)
                {
                    linha += " A";
                }
                else if (tabuleiro[i, j].pecaAtual is Peao)
                {
                    linha += " P";
                }
                else
                {
                    linha += " "+tabuleiro[i, j].cor;
                }
            }
            linha += "\n";
        }
        Debug.Log(linha);
    }
    void inicializaCasas()
    {
        for (int i = 0; i < tamTabuleiro; i++)
        {
            for (int j = 0; j < tamTabuleiro; j++)
            {
                tabuleiro[i, j] = new Casa(i,j);
            }
        }
    }
	// Use this for initialization
	void Start () {
        Jogador j1 = new Jogador('b',true);
        Jogador j2 = new Jogador('p',false);
        inicializaCasas();
        pintaCasas();
        inserePecas(j1, j2);
        printaTabuleiro();
        //verifica peao
        j2.conjuntoPecas[8].realizaMovimento(j2.conjuntoPecas[8].listaMovimentos(tabuleiro, j2.conjuntoPecas[8].posX, j2.conjuntoPecas[8].posY)[0]);
        
        //verifica rei
        j2.conjuntoPecas[4].posX = 4;
        j2.conjuntoPecas[4].posY = 4;
        tabuleiro[4, 4].pecaAtual = tabuleiro[0, 4].pecaAtual;
        tabuleiro[0, 4].pecaAtual = null;
        printaTabuleiro();
        j2.conjuntoPecas[4].realizaMovimento(j2.conjuntoPecas[4].listaMovimentos(tabuleiro, j2.conjuntoPecas[4].posX, j2.conjuntoPecas[4].posY)[2]);
        printaTabuleiro();
        j2.conjuntoPecas[4].realizaMovimento(j2.conjuntoPecas[4].listaMovimentos(tabuleiro, j2.conjuntoPecas[4].posX, j2.conjuntoPecas[4].posY)[3]);
        printaTabuleiro();

        //verifica cavalo
        j2.conjuntoPecas[1].posX = 5;
        j2.conjuntoPecas[1].posY = 5;
        tabuleiro[5, 5].pecaAtual = tabuleiro[0, 1].pecaAtual;
        tabuleiro[0, 1].pecaAtual = null;
        printaTabuleiro();
        j2.conjuntoPecas[1].realizaMovimento(j2.conjuntoPecas[1].listaMovimentos(tabuleiro, j2.conjuntoPecas[1].posX, j2.conjuntoPecas[1].posY)[2]);
        printaTabuleiro();
        j2.conjuntoPecas[1].realizaMovimento(j2.conjuntoPecas[1].listaMovimentos(tabuleiro, j2.conjuntoPecas[1].posX, j2.conjuntoPecas[1].posY)[3]);
        printaTabuleiro();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
