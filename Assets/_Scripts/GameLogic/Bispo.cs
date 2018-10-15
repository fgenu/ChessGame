using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bispo : Peca
{
	public Bispo(Jogador j) : base(j)
	{

	}

	    public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem, bool verificaXeque = true, bool verificaCaptura = false)
	{
		var movimentos = new List<Movimento>();

		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, +1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, -1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, +1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, -1, verificaXeque: verificaXeque));

		return movimentos;
	}
}
