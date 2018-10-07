using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento
{
	public Casa origem, destino;
	public enum Tipo { Normal, SomenteCaptura, SemCaptura };
	public Tipo tipo { get; private set; }
	public int valor; // Genú: O que significa?



	public Movimento(Casa destino, Casa origem, Tipo tipo = Tipo.Normal)
	{
		valor = 0;
		this.destino = destino;
		this.origem = origem;
	}

	// Propaga um movimento na direção dada.
	public static List<Movimento> SeguindoDirecao(Tabuleiro tabuleiro, Casa origem, int x, int y, int passos = int.MaxValue, Tipo tipo = Tipo.Normal, bool bloqueavel = true, bool verificaXeque=true)
	{
		var possibilidades = new List<Movimento>();

		Casa seguinte = tabuleiro.GetCasa(origem.PosX + x, origem.PosY + y);

		while (seguinte != null && passos > 0)
		{
			if (seguinte.EstaOcupada())
			{
                if (tipo != Tipo.SemCaptura)
                    if (origem.PecaAtual.PodeCapturar(seguinte.PecaAtual))
                    {
                        if (verificaXeque)
                        {
                            if (origem.PecaAtual.podeMoverXeque(tabuleiro, origem, seguinte))
                            {
                                possibilidades.Add(new Movimento(origem: origem, destino: seguinte, tipo: tipo));
                            }
                        }
                        else
                        {
                            possibilidades.Add(new Movimento(origem: origem, destino: seguinte, tipo: tipo));
                        }
                    }

				// Se for "bloqueável", o movimento não permite atravessar outras peças. (O cavalo "pula", não "atravessa", depois explico melhor)
				if (bloqueavel) return possibilidades;
			}
			else
			{
				if (tipo != Tipo.SomenteCaptura)
                {

                    if (verificaXeque)
                    {
                        if (origem.PecaAtual.podeMoverXeque(tabuleiro, origem, seguinte))
                        {
                            possibilidades.Add(new Movimento(origem: origem, destino: seguinte, tipo: tipo));
                        }
                    }
                    else
                    {
                        possibilidades.Add(new Movimento(origem: origem, destino: seguinte, tipo: tipo));
                    }
                }
			}

			seguinte = tabuleiro.GetCasa(seguinte.PosX + x, seguinte.PosY + y);
			passos--;
		}

		return possibilidades;
	}

}
