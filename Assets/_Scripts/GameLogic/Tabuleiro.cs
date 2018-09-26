using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabuleiro
{
	public int Tamanho = 8;

	// TODO: Fazer isto voltar a ser privado ao finalizar os ListaMovimentos
	public /*private*/ Casa[,] tabuleiro = new Casa[8, 8];

	public Tabuleiro()
	{
		InicializaCasas();
		PintaCasas();
		PrintaTabuleiro();
	}

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
			Debug.Log("Coordenadas " + coordenadas + " inválidas.");
			return null;
		}

		char letra = coordenadas[0];
		int numero = int.Parse(coordenadas[1].ToString());

		return GetCasa((int)(letra - 'A'), numero - 1);
	}

	// pinta as casas de branco ou preto
	void PintaCasas() // TODO: avaliar necessidade de cores nas casas
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

	public void InserePecasNaPosicaoInicial(Partida partida)
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

	public void PrintaTabuleiro()
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
				tabuleiro[i, j] = new Casa(i, j, this);
			}
		}
	}

}
