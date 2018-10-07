using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : Peca
{

	public Torre(Jogador j) : base(j)
	{
	}

	public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem,bool verificaXeque=true)
	{
		var movimentos = new List<Movimento>();

		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, 0, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, 0, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, +1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, -1, verificaXeque: verificaXeque));

		return movimentos;
	}

}
