using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : Peca
{
	int tamTabuleiro = 8;
	bool primeiraJogada = true;
	public Rei(char c, bool cima, Jogador j) : base(c, cima, j)
	{

	}

	public override List<Movimento> ListaMovimentos(Casa[,] tab, int x, int y)
	{
		List<Movimento> movimentos = new List<Movimento>();
		int quantMovimentos;

		//rei pode ir tanto para frente ou atras então movimento unico
		//verifica se pode mover para cima do tabuleiro
		if (x - 1 >= 0)
		{
			movimentos.Add(new Movimento(x - 1, y, tab[x - 1, y], tab[x, y]));
			if (y - 1 >= 0)
			{
				movimentos.Add(new Movimento(x - 1, y - 1, tab[x - 1, y - 1], tab[x, y]));
			}
			if (y + 1 < tamTabuleiro)
			{
				movimentos.Add(new Movimento(x - 1, y + 1, tab[x - 1, y + 1], tab[x, y]));
			}
		}
		//verifica embaixo do tabuleiro
		if (x + 1 < tamTabuleiro)
		{
			movimentos.Add(new Movimento(x + 1, y, tab[x + 1, y], tab[x, y]));
			if (y - 1 >= 0)
			{
				movimentos.Add(new Movimento(x + 1, y - 1, tab[x + 1, y - 1], tab[x, y]));
			}
			if (y + 1 < tamTabuleiro)
			{
				movimentos.Add(new Movimento(x + 1, y + 1, tab[x + 1, y + 1], tab[x, y]));
			}

		}
		//verifica os lados
		if (y - 1 >= 0)
		{
			movimentos.Add(new Movimento(x, y - 1, tab[x, y - 1], tab[x, y]));
		}
		if (y + 1 < tamTabuleiro)
		{
			movimentos.Add(new Movimento(x, y + 1, tab[x, y + 1], tab[x, y]));
		}

		return movimentos;
	}
}
