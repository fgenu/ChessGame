using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalo : Peca
{
	bool primeiraJogada = true;
	public Cavalo(char c, bool cima, Jogador j) : base(c, cima, j)
	{

	}


	public override List<Movimento> ListaMovimentos(Casa[,] tab, int x, int y)
	{
		List<Movimento> movimentos = new List<Movimento>();
		int quantMovimentos;

		//verifica cada lugar que o cavalo pode ir
		//verifica se pode mover para cima 2x e esquerda 1x
		if (x - 2 >= 0 && y - 1 >= 0)
		{
			movimentos.Add(new Movimento(x - 2, y - 1, tab[x - 2, y - 1], tab[x, y]));
		}
		//verifica se pode mover para cima 2x e direita 1x
		if (x - 2 >= 0 && y + 1 < tabuleiro.Tamanho)
		{
			movimentos.Add(new Movimento(x - 2, y + 1, tab[x - 2, y + 1], tab[x, y]));
		}
		//verifica se pode mover para cima 1x e esquerda 2x
		if (x - 1 >= 0 && y - 2 >= 0)
		{
			movimentos.Add(new Movimento(x - 1, y - 2, tab[x - 1, y - 2], tab[x, y]));
		}
		//verifica se pode mover para cima 1x e direita 2x
		if (x - 1 >= 0 && y + 2 < tabuleiro.Tamanho)
		{
			movimentos.Add(new Movimento(x - 1, y + 2, tab[x - 1, y + 2], tab[x, y]));
		}
		//verifica se pode mover para baixo 2x e esquerda 1x
		if (x + 2 >= 0 && y - 1 >= 0)
		{
			movimentos.Add(new Movimento(x + 2, y - 1, tab[x + 2, y - 1], tab[x, y]));
		}
		//verifica se pode mover para baixo 2x e direita 1x
		if (x + 2 >= 0 && y + 1 < tabuleiro.Tamanho)
		{
			movimentos.Add(new Movimento(x + 2, y + 1, tab[x + 2, y + 1], tab[x, y]));
		}
		//verifica se pode mover para baixo 1x e esquerda 2x
		if (x + 1 >= 0 && y - 2 >= 0)
		{
			movimentos.Add(new Movimento(x + 1, y - 2, tab[x + 1, y - 2], tab[x, y]));
		}
		//verifica se pode mover para baixo 1x e direita 2x
		if (x + 1 >= 0 && y + 2 < tabuleiro.Tamanho)
		{
			movimentos.Add(new Movimento(x + 1, y - 2, tab[x + 1, y - 2], tab[x, y]));
		}

		return movimentos;
	}

	public override GenuListaMovimentos(Tabuleiro tabuleiro, Casa origem)
	{
		var movimentos = new List<Movimento>();

		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -2, -1, passos = 1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -2, +1, passos = 1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, -2, passos = 1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, +2, passos = 1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +2, -1, passos = 1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +2, +1, passos = 1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, -2, passos = 1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, +2, passos = 1));

		return movimentos;
	}
}
