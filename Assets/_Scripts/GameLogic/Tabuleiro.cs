using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabuleiro : MonoBehaviour
{
	public int Tamanho = 8;
	
	// TODO: Fazer isto voltar a ser privado ao finalizar os ListaMovimentos
	public /*private*/ Casa[,] tabuleiro = new Casa[8,8];

	public Casa GetCasa(int x, int y)
	{
		if (x < 0
			|| y < 0
			|| x >= Tamanho
			|| y >= Tamanho)
			return null;
		else
		{
			return tabuleiro[x, y];
		}
	}
	public Casa GetCasa(string coordenadas) // usa o padrão do xadrez, como A1 ou D7
	{
		if (coordenadas.Length != 2)
		{
			print("Coordenadas " + coordenadas + " inválidas.");
			return null;
		}
		
		char letra = coordenadas[0];
		int numero = int.Parse(coordenadas[1].ToString());

		return GetCasa((int)(letra - 'A'), numero - 1);
	}

	//função serve para pintar a casa de branco ou preto
	void PintaCasas()
	{
		//pinta linhas de sequencia branco, preto, branco etc
		for (int i = 0; i < Tamanho; i += 2)
		{
			for (int j = 0; j < Tamanho; j++)
			{
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
		for (int i = 1; i < Tamanho; i += 2)
		{
			for (int j = 0; j < Tamanho; j++)
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
	void InserePecas(Partida partida)
	{
		//coloca as peças dos jogadores no tabuleiro
		for (int i = 0; i < Tamanho; i++)
		{
			//coloca as pecas especiais do jogador de cima
			tabuleiro[i, Tamanho - 1].ColocarPeca(partida.JogadorDeCima().conjuntoPecas[i]);

			//coloca os peoes do jogador de cima
			tabuleiro[i, Tamanho - 2].ColocarPeca(partida.JogadorDeCima().conjuntoPecas[i + 8]);
            
			//coloca as pecas especiais jogador de baixo
			tabuleiro[i, 0].ColocarPeca(partida.JogadorDeBaixo().conjuntoPecas[i]);

			//coloca os peoes do jogador de baixo
			tabuleiro[i, 1].ColocarPeca(partida.JogadorDeBaixo().conjuntoPecas[i + 8]);
		}

	}

	void PrintaTabuleiro()
	{
		string linha = "";
		for (int i = 0; i < Tamanho; i++)
		{
			for (int j = 0; j < Tamanho; j++)
			{
				if (tabuleiro[i, j].PecaAtual is Torre)
				{
					linha += " T";
				}
				else if (tabuleiro[i, j].PecaAtual is Cavalo)
				{
					linha += " C";
				}
				else if (tabuleiro[i, j].PecaAtual is Bispo)
				{
					linha += " B";
				}
				else if (tabuleiro[i, j].PecaAtual is Rei)
				{
					linha += " E";
				}
				else if (tabuleiro[i, j].PecaAtual is Rainha)
				{
					linha += " A";
				}
				else if (tabuleiro[i, j].PecaAtual is Peao)
				{
					linha += " P";
				}
				else
				{
					linha += " " + tabuleiro[i, j].cor;
				}
			}
			linha += "\n";
		}
		Debug.Log(linha);
	}
	void InicializaCasas()
	{
		for (int i = 0; i < Tamanho; i++)
		{
			for (int j = 0; j < Tamanho; j++)
			{
				tabuleiro[i, j] = new Casa(i, j);
			}
		}
	}

	void Awake()
	{
        InicializaCasas();
		PintaCasas();
		PrintaTabuleiro();
	}
	void Start()
	{
		Partida partida = FindObjectOfType<Partida>();
		InserePecas(partida);
		PrintaTabuleiro();
		/*
		Jogador j1 = new Jogador('b', true);
		Jogador j2 = new Jogador('p', false);
        j2.conjuntoPecas[4].defineInimigo(j1);
        j1.conjuntoPecas[4].defineInimigo(j2);
		InserePecas(j1, j2);
		PrintaTabuleiro();
		*/

        //verifica peao
        // j1.conjuntoPecas[8].SetPosition(4 ,3);
        // tabuleiro[4, 3].pecaAtual = tabuleiro[6, 1].pecaAtual;
        // tabuleiro[6,1].pecaAtual = null;
        /*j2.conjuntoPecas[8].RealizaMovimento(j2.conjuntoPecas[8].ListaMovimentos(tabuleiro, j2.conjuntoPecas[8].posX, j2.conjuntoPecas[8].posY)[0]);
        PrintaTabuleiro();
        */

        //verifica rei
        /*j2.conjuntoPecas[4].SetPosition(5, 5);
		tabuleiro[5, 5].pecaAtual = tabuleiro[0, 4].pecaAtual;
		tabuleiro[0, 4].pecaAtual = null;
		PrintaTabuleiro();
		j2.conjuntoPecas[4].RealizaMovimento(j2.conjuntoPecas[4].ListaMovimentos(tabuleiro, j2.conjuntoPecas[4].posX, j2.conjuntoPecas[4].posY)[2]);
		PrintaTabuleiro();
		j2.conjuntoPecas[4].RealizaMovimento(j2.conjuntoPecas[4].ListaMovimentos(tabuleiro, j2.conjuntoPecas[4].posX, j2.conjuntoPecas[4].posY)[3]);
		PrintaTabuleiro();*/

		//verifica cavalo
		// j2.conjuntoPecas[1].SetPosition(5, 5);
		// tabuleiro[5, 5].pecaAtual = tabuleiro[0, 1].pecaAtual;
		// tabuleiro[0, 1].pecaAtual = null;
		// PrintaTabuleiro();
		// j2.conjuntoPecas[1].RealizaMovimento(j2.conjuntoPecas[1].ListaMovimentos(tabuleiro, j2.conjuntoPecas[1].posX, j2.conjuntoPecas[1].posY)[2]);
		// PrintaTabuleiro();
		// j2.conjuntoPecas[1].RealizaMovimento(j2.conjuntoPecas[1].ListaMovimentos(tabuleiro, j2.conjuntoPecas[1].posX, j2.conjuntoPecas[1].posY)[3]);
		// PrintaTabuleiro();

    }
}
