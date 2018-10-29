using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : Peca
{
	public Rei(Jogador j) : base(j)
	{

	}

	public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem, bool verificaXeque = true, bool verificaCaptura = false)
	{
		var movimentos = new List<Movimento>();

		// Como torre:
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, 0, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, 0, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, -1, passos: 1, verificaXeque: verificaXeque));

		// Como bispo:
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, -1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, -1, passos: 1, verificaXeque: verificaXeque));

		return movimentos;
	}

    // public List<Movimento> FelipeListaMovimentos(Tabuleiro tabuleiro, Casa origem, bool verificaXeque = true, bool verificaCaptura = false)
	// {
	// 	List<Movimento> movimentos = new List<Movimento>();

	// 	// temp, refinar depois:
	// 	Casa[,] tab = tabuleiro.tabuleiro;
	// 	int x = origem.PosX;
	// 	int y = origem.PosY;
	// 	//int quantMovimentos;

	// 	//rei pode ir tanto para frente ou atras então movimento unico
	// 	//verifica se pode mover para jogadorCima do tabuleiro

	// 	//funciona por verificando linha, depois coluna, mas para cada casa 
	// 	//verifica se pode mover por xeque, depois verifica se tem casa aliada na posição
	// 	if (x - 1 >= 0)
	// 	{
	// 		if (verifica Xeque && podeMoverXeque(tabuleiro,CasaAtual, tabuleiro.GetCasa(x - 1, y))
	// )
	// 		{
	// 			if (tab[x - 1, y].PecaAtual == null || tab[x - 1, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
	// 				movimentos.Add(new Movimento(tab[x - 1, y], tab[x, y]));
	// 		}

	// 		if (y - 1 >= 0)
	// 		{
	// 			if (verificaXeque && podeMoverXeque(tabuleiro, CasaAtual, tabuleiro.GetCasa(x - 1, y - 1)))
	// 				if (tab[x - 1, y - 1].PecaAtual == null || tab[x - 1, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
	// 				{
	// 					movimentos.Add(new Movimento(tab[x - 1, y - 1], tab[x, y]));
	// 				}
	// 		}
	// 		if (y + 1 < CasaAtual.Tabuleiro.Tamanho)
	// 		{
	// 			if (verificaXeque && podeMoverXeque(tabuleiro, CasaAtual, tabuleiro.GetCasa(x - 1, y + 1)))
	// 				if (tab[x - 1, y + 1].PecaAtual == null || tab[x - 1, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
	// 				{
	// 					movimentos.Add(new Movimento(tab[x - 1, y + 1], tab[x, y]));
	// 				}
	// 		}
	// 	}
	// 	//verifica embaixo do tabuleiro
	// 	if (x + 1 < CasaAtual.Tabuleiro.Tamanho)
	// 	{
	// 		if (verificaXeque && podeMoverXeque(tabuleiro, CasaAtual, tabuleiro.GetCasa(x + 1, y))
	// ) if (tab[x + 1, y].PecaAtual == null || tab[x + 1, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
	// 			{
	// 				movimentos.Add(new Movimento(tab[x + 1, y], tab[x, y]));
	// 			}

	// 		if (y - 1 >= 0)
	// 		{
	// 			if (verificaXeque && podeMoverXeque(tabuleiro, CasaAtual, tabuleiro.GetCasa(x + 1, y - 1)))
	// 			{
	// 				if (tab[x + 1, y - 1].PecaAtual == null || tab[x + 1, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
	// 					movimentos.Add(new Movimento(tab[x + 1, y - 1], tab[x, y]));
	// 			}
	// 		}
	// 		if (y + 1 < CasaAtual.Tabuleiro.Tamanho)
	// 		{
	// 			if (verificaXeque && podeMoverXeque(tabuleiro, CasaAtual, tabuleiro.GetCasa(x + 1, y + 1)))
	// 				if (tab[x + 1, y + 1].PecaAtual == null || tab[x + 1, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
	// 				{
	// 					movimentos.Add(new Movimento(tab[x + 1, y + 1], tab[x, y]));
	// 				}
	// 		}

	// 	}
	// 	//verifica os lados
	// 	if (y - 1 >= 0)
	// 	{
	// 		if (verificaXeque && podeMoverXeque(tabuleiro, CasaAtual, tabuleiro.GetCasa(x, y - 1)))
	// 			if (tab[x, y - 1].PecaAtual == null || tab[x, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
	// 			{
	// 				movimentos.Add(new Movimento(tab[x, y - 1], tab[x, y]));
	// 			}
	// 	}
	// 	if (y + 1 < CasaAtual.Tabuleiro.Tamanho)
	// 	{
	// 		if (verificaXeque && podeMoverXeque(tabuleiro, CasaAtual, tabuleiro.GetCasa(x, y + 1)))
	// 			if (tab[x, y + 1].PecaAtual == null || tab[x, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
	// 			{
	// 				movimentos.Add(new Movimento(tab[x, y + 1], tab[x, y]));
	// 			}
	// 	}

	// 	return movimentos;
	// }


	public override void Roque(Tabuleiro tabuleiro, Torre escolhida)
	{
		
		escolhida.Roque(tabuleiro); // obs: eu fiz isso para evitar redundancia nas funções, a diferença é que esse aqui considera uma torre especifica que chama a função
	}								// ou seja o jogador escolhe qual das torres ele pode usar para o roque

}
