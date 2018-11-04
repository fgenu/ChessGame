using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : Peca
{
	public Rei(Jogador j) : base(j)
	{

	}

	public override List<Movimento> ListaMovimentos(bool verificaXeque = true, bool verificaCaptura = false)
	{
		var movimentos = new List<Movimento>();

		// Como torre:
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, +1, 0, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, -1, 0, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 0, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 0, -1, passos: 1, verificaXeque: verificaXeque));

		// Como bispo:
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, +1, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, +1, -1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, -1, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, -1, -1, passos: 1, verificaXeque: verificaXeque));

		return movimentos;
	}

	public override void Roque(Tabuleiro tabuleiro, Torre escolhida)
	{
		
		escolhida.Roque(tabuleiro); // obs: eu fiz isso para evitar redundancia nas funções, a diferença é que esse aqui considera uma torre especifica que chama a função
	}								// ou seja o jogador escolhe qual das torres ele pode usar para o roque

}
