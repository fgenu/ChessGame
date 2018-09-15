﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bispo : Peca
{
	public Bispo(char c, bool cima, Jogador j) : base(c, cima, j)
	{

	}

	public override List<Movimento> GenuListaMovimentos(Tabuleiro tabuleiro, Casa origem)
	{
		var movimentos = new List<Movimento>();

		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, +1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, -1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, +1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, -1));

		return movimentos;
	}
}
