using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainha : Peca
{
	public Rainha(Jogador j) : base(j)
	{

	}

	public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem)
	{
		var movimentos = new List<Movimento>();

		// Como torre:
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, 0));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, 0));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, +1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, -1));

		// Como bispo:
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, +1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, -1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, +1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, -1));

		return movimentos;
	}
}
