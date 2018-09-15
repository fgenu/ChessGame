using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabuleiro : MonoBehaviour
{
	int Tamanho { get; private set; } = 8;
	private Casa[,] tabuleiro = new Casa[Tamanho, Tamanho];

	public Casa GetCasa(int x, int y)
	{
		if (x < 0
			|| y < 0
			|| x >= Tamanho
			|| y >= Tamanho)
			return null;
		else
			return tabuleiro[x, y];
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
	void InserePecas(Jogador j1, Jogador j2)
	{
		//coloca as peças dos jogadores no tabuleiro
		for (int i = 0; i < Tamanho; i++)
		{
			//coloca as pecas especiais do primeiro jogador
			tabuleiro[Tamanho - 1, i].pecaAtual = j1.conjuntoPecas[i];
			j1.conjuntoPecas[i].posX = Tamanho - 1;
			j1.conjuntoPecas[i].posY = i;

			//coloca os peoes do primeiro jogador
			tabuleiro[Tamanho - 2, i].pecaAtual = j1.conjuntoPecas[i + 8];
			j1.conjuntoPecas[i + 8].posX = Tamanho - 2;
			j1.conjuntoPecas[i + 8].posY = i;

			//coloca as pecas especiais do segundo jogador
			tabuleiro[0, i].pecaAtual = j2.conjuntoPecas[i];
			j2.conjuntoPecas[i].posX = 0;
			j2.conjuntoPecas[i].posY = i;

			//coloca os peoes do segundo jogador
			tabuleiro[1, i].pecaAtual = j2.conjuntoPecas[i + 8];
			j2.conjuntoPecas[i + 8].posX = 1;
			j2.conjuntoPecas[i + 8].posY = i;
		}

	}

	void PrintaTabuleiro()
	{
		string linha = "";
		for (int i = 0; i < Tamanho; i++)
		{
			for (int j = 0; j < Tamanho; j++)
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
	// Use this for initialization
	void Start()
	{
		Jogador j1 = new Jogador('b', true);
		Jogador j2 = new Jogador('p', false);
		InicializaCasas();
		PintaCasas();
		InserePecas(j1, j2);
		PrintaTabuleiro();
		//verifica peao
		j2.conjuntoPecas[8].RealizaMovimento(j2.conjuntoPecas[8].ListaMovimentos(tabuleiro, j2.conjuntoPecas[8].posX, j2.conjuntoPecas[8].posY)[0]);

		//verifica rei
		j2.conjuntoPecas[4].SetPosition(4, 4);
		tabuleiro[4, 4].pecaAtual = tabuleiro[0, 4].pecaAtual;
		tabuleiro[0, 4].pecaAtual = null;
		PrintaTabuleiro();
		j2.conjuntoPecas[4].RealizaMovimento(j2.conjuntoPecas[4].ListaMovimentos(tabuleiro, j2.conjuntoPecas[4].posX, j2.conjuntoPecas[4].posY)[2]);
		PrintaTabuleiro();
		j2.conjuntoPecas[4].RealizaMovimento(j2.conjuntoPecas[4].ListaMovimentos(tabuleiro, j2.conjuntoPecas[4].posX, j2.conjuntoPecas[4].posY)[3]);
		PrintaTabuleiro();

		//verifica cavalo
		j2.conjuntoPecas[1].SetPosition(5, 5);
		tabuleiro[5, 5].pecaAtual = tabuleiro[0, 1].pecaAtual;
		tabuleiro[0, 1].pecaAtual = null;
		PrintaTabuleiro();
		j2.conjuntoPecas[1].RealizaMovimento(j2.conjuntoPecas[1].ListaMovimentos(tabuleiro, j2.conjuntoPecas[1].posX, j2.conjuntoPecas[1].posY)[2]);
		PrintaTabuleiro();
		j2.conjuntoPecas[1].RealizaMovimento(j2.conjuntoPecas[1].ListaMovimentos(tabuleiro, j2.conjuntoPecas[1].posX, j2.conjuntoPecas[1].posY)[3]);
		PrintaTabuleiro();

	}
}
