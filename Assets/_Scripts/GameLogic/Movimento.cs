using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento
{
	public int posX, posY;  // Genú: Não precisa se armazenarmos a Casa destino. Aliás, é ambíguo, antes de escrever este comentário achei que fosse da origem... 
							// Não vale a pena uma variável de nome curto se demorar pra entender o que ela significa
	public int valor; // Genú: O que significa?

	public Casa origem, destino;

	/*public Movimento(int x, int y, Casa destino, Casa origem) // Genú: não faz sentido passar x e y, já tem referência à casa final em 'destino'...
	{
		posX = x;
		posY = y;
		valor = 0;
		this.destino = destino;
		this.origem = origem;
	}*/

	// Genú: Podemos manter só esse construtor?
	public Movimento( Casa destino, Casa origem)
	{
		posX = destino.posX;
		posY = destino.posY;
		valor = 0;
		this.destino = destino;
		this.origem = origem;
	}

	// Propaga um movimento na direção dada. 
	/*public List<Movimento> SeguindoDirecao(Tabuleiro tabuleiro, Casa origem, int x, int y, int passos = MAX_INT, boolean bloqueavel = true)
	{
		var possibilidades = new List<Movimento>();

		Casa seguinte = tabuleiro.GetCasa(origem.posX + x, origem.posY + y);

		while (seguinte != null && passos > 0)
		{
			if (seguinte.EstaOcupada)
			{
				if (origem.pecaAtual.PodeCapturar(seguinte.pecaAtual))
				{
					possibilidades.Add(new Movimento(origem, seguinte));
				}

				// Se for "bloqueável", o movimento não permite atravessar outras peças. (O cavalo "pula", não "atravessa", depois explico melhor)
				if (bloqueavel) return possibilidades;
			}
			else
			{
				possibilidades.Add(new Movimento(origem, seguinte));
			}

			seguinte = tabuleiro.GetCasa(seguinte.posX + x, seguinte.posY + y);
			passos--;
		}

		return possibilidades;
	}*/
}
