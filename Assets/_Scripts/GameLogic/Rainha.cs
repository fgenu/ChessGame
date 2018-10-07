using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainha : Peca
{
	public Rainha(Jogador j) : base(j)
	{

	}

	public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem,bool verificaXeque=true)
	{
		var movimentos = new List<Movimento>();

		// Como torre:
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, 0, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, 0, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, +1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, -1, verificaXeque: verificaXeque));

		// Como bispo:
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, +1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, -1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, +1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, -1, verificaXeque: verificaXeque));

		return movimentos;
	}
}
