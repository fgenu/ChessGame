using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : Peca
{
	public Rei(Jogador j) : base(j)
	{

	}

	//função que verifica todos os movimentos das peças inimigas para verificar se o rei pode mover para a casa sem ter xeque
	// TODO: generalizar essa verificação para funcionar com movimentos de outra peça 
	// (por exemplo, uma peça não pode sair de um lugar que protegia diretamente o seu rei)
	public bool podeMoverXeque(Tabuleiro tabuleiro, Casa destino)
	{
		if (destino == null)
			return false;

		List<Movimento> movimentos;
		foreach (Peca p in jDono.inimigo.conjuntoPecas)
		{
			if (!(p is Rei))
			{
				movimentos = p.ListaMovimentos(tabuleiro, p.CasaAtual);
				if (movimentos != null)
				{
					foreach (Movimento mov in movimentos)
					{
						if (mov.destino == destino)
						{
							return false;
						}
					}
				}
			}
		}
		return true;
	}

	public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem)
	{
		List<Movimento> movimentos = new List<Movimento>();

		// temp, refinar depois:
		Casa[,] tab = tabuleiro.tabuleiro;
		int x = origem.PosX;
		int y = origem.PosY;
		//int quantMovimentos;

		//rei pode ir tanto para frente ou atras então movimento unico
		//verifica se pode mover para jogadorCima do tabuleiro

		//funciona por verificando linha, depois coluna, mas para cada casa 
		//verifica se pode mover por xeque, depois verifica se tem casa aliada na posição
		if (x - 1 >= 0)
		{
			if (podeMoverXeque(tabuleiro, tabuleiro.GetCasa(x - 1, y))
	)
			{
				if (tab[x - 1, y].PecaAtual == null || tab[x - 1, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
					movimentos.Add(new Movimento(tab[x - 1, y], tab[x, y]));
			}

			if (y - 1 >= 0)
			{
				if (podeMoverXeque(tabuleiro, tabuleiro.GetCasa(x - 1, y - 1)))
					if (tab[x - 1, y - 1].PecaAtual == null || tab[x - 1, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
					{
						movimentos.Add(new Movimento(tab[x - 1, y - 1], tab[x, y]));
					}
			}
			if (y + 1 < CasaAtual.Tabuleiro.Tamanho)
			{
				if (podeMoverXeque(tabuleiro, tabuleiro.GetCasa(x - 1, y + 1)))
					if (tab[x - 1, y + 1].PecaAtual == null || tab[x - 1, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
					{
						movimentos.Add(new Movimento(tab[x - 1, y + 1], tab[x, y]));
					}
			}
		}
		//verifica embaixo do tabuleiro
		if (x + 1 < CasaAtual.Tabuleiro.Tamanho)
		{
			if (podeMoverXeque(tabuleiro, tabuleiro.GetCasa(x + 1, y))
	) if (tab[x + 1, y].PecaAtual == null || tab[x + 1, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
				{
					movimentos.Add(new Movimento(tab[x + 1, y], tab[x, y]));
				}

			if (y - 1 >= 0)
			{
				if (podeMoverXeque(tabuleiro, tabuleiro.GetCasa(x + 1, y - 1)))
				{
					if (tab[x + 1, y - 1].PecaAtual == null || tab[x + 1, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
						movimentos.Add(new Movimento(tab[x + 1, y - 1], tab[x, y]));
				}
			}
			if (y + 1 < CasaAtual.Tabuleiro.Tamanho)
			{
				if (podeMoverXeque(tabuleiro, tabuleiro.GetCasa(x + 1, y + 1)))
					if (tab[x + 1, y + 1].PecaAtual == null || tab[x + 1, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
					{
						movimentos.Add(new Movimento(tab[x + 1, y + 1], tab[x, y]));
					}
			}

		}
		//verifica os lados
		if (y - 1 >= 0)
		{
			if (podeMoverXeque(tabuleiro, tabuleiro.GetCasa(x, y - 1)))
				if (tab[x, y - 1].PecaAtual == null || tab[x, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
				{
					movimentos.Add(new Movimento(tab[x, y - 1], tab[x, y]));
				}
		}
		if (y + 1 < CasaAtual.Tabuleiro.Tamanho)
		{
			if (podeMoverXeque(tabuleiro, tabuleiro.GetCasa(x, y + 1)))
				if (tab[x, y + 1].PecaAtual == null || tab[x, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
				{
					movimentos.Add(new Movimento(tab[x, y + 1], tab[x, y]));
				}
		}

		return movimentos;
	}

}
