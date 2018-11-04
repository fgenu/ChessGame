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
		this.tipo = tipo;
	}

	// Propaga um movimento na direção dada. 
	public static List<Movimento> SeguindoDirecao(Casa origem, int x, int y, int passos = int.MaxValue, Tipo tipo = Tipo.Normal, bool bloqueavel = true, bool verificaXeque=true)
	{
		var possibilidades = new List<Movimento>();
        //Debug.Log(origem.PosX + x);
		Tabuleiro tabuleiro = origem.Tabuleiro;
		Casa seguinte = tabuleiro.GetCasa(origem.PosX + x, origem.PosY + y);

		while (seguinte != null && passos > 0)
		{
			Movimento novo = new Movimento(origem: origem, destino: seguinte, tipo: tipo);
			
			if (seguinte.EstaOcupada())
			{
                if (tipo != Tipo.SemCaptura)
                    if (origem.PecaAtual.PodeCapturar(seguinte.PecaAtual))
                    {
                        if (verificaXeque)
                        {
                            if (novo.CausaAutoXeque() == false)
                            {
                                possibilidades.Add(novo);
                            }
                        }
                        else
                        {
                            possibilidades.Add(novo);
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
                        if (novo.CausaAutoXeque() == false)
                        {
                            possibilidades.Add(novo);
                        }
                    }
                    else
                    {
                        possibilidades.Add(novo);
                    }
                }
			}

			seguinte = tabuleiro.GetCasa(seguinte.PosX + x, seguinte.PosY + y);
			passos--;
		}

		return possibilidades;
	}

	// verifica todos os movimentos das peças inimigas para verificar se pode mover sem ter xeque
	// tive que mudar para public porque essa função precisa ser utilizada em outro local alem da propria classe.
	public bool CausaAutoXeque ()
	{
		if (origem == null || destino == null)
			return false;
		
		// Faz um ensaio do tabuleiro como se o movimento acontecesse
		Peca movida;
		if (origem.PecaAtual != null)
			movida = origem.PopPeca();
		else
			return false;
		
		Peca capturada = null;
		if (destino.PecaAtual != null)
			capturada = destino.PopPeca();
		
		destino.ColocarPeca(movida);

		// Verifica se seria xeque
		bool resultado = false;
		if (movida.jDono.EmXeque())		
			resultado = true;

		// Devolvendo as peças para seus lugares
		destino.PopPeca();
		origem.ColocarPeca(movida);
		if (capturada != null)
			destino.ColocarPeca(capturada);

		return resultado;
	}

}