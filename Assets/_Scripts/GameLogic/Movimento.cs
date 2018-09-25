using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TiposDeMovimento;

namespace TiposDeMovimento
{
	public enum Tipo {Normal, SomenteCaptura, SemCaptura};
}


public class Movimento
{
	public int valor; // Genú: O que significa?

	public Casa origem, destino;

	//public enum Tipo {Normal, SomenteCaptura, SemCaptura};

	public Movimento (Casa destino, Casa origem)
	{
		valor = 0;
		this.destino = destino;
		this.origem = origem;
	}

	// Propaga um movimento na direção dada. // TODO: (pro Genú) implementar verificações de tipo (ex: movimento do peão)
	public static List<Movimento> SeguindoDirecao(Tabuleiro tabuleiro, Casa origem, int x, int y, int passos = int.MaxValue, Tipo tipo = Tipo.Normal, bool bloqueavel = true)
	{
		var possibilidades = new List<Movimento>();

		Casa seguinte = tabuleiro.GetCasa(origem.PosX + x, origem.PosY + y);

		while (seguinte != null && passos > 0)
		{
			if (seguinte.EstaOcupada())
			{
				if (tipo != Tipo.SemCaptura)
					if (origem.PecaAtual.PodeCapturar(seguinte.PecaAtual))
						possibilidades.Add(new Movimento(origem:origem, destino:seguinte));

				// Se for "bloqueável", o movimento não permite atravessar outras peças. (O cavalo "pula", não "atravessa", depois explico melhor)
				if (bloqueavel) return possibilidades;
			}
			else
			{
				if (tipo != Tipo.SomenteCaptura)
					possibilidades.Add(new Movimento(origem:origem, destino:seguinte));
			}

			seguinte = tabuleiro.GetCasa(seguinte.PosX + x, seguinte.PosY + y);
			passos--;
		}

		return possibilidades;
	}

}
