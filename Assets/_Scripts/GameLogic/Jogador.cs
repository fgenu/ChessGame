using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador
{
	public List<Peca> conjuntoPecas = new List<Peca>();
	public bool jogadorCima;
	public char Cor { get; private set; }
	public Jogador inimigo;
	public Jogador(char cor, bool cima)
	{
		Cor = cor;
		jogadorCima = cima;
		InicializaPecas();
	}
	//inicializa as pecas do jogador
	void InicializaPecas()
	{
		//inicializa as peças especiais da linha mais a borda
		conjuntoPecas.Add(new Torre(this));
		conjuntoPecas.Add(new Cavalo(this));
		conjuntoPecas.Add(new Bispo(this));
		conjuntoPecas.Add(new Rainha(this));
		conjuntoPecas.Add(new Rei(this));
		conjuntoPecas.Add(new Bispo(this));
		conjuntoPecas.Add(new Cavalo(this));
		conjuntoPecas.Add(new Torre(this));

		//inicializa os peoes
		for (int i = 8; i < 16; i++)
		{
			conjuntoPecas.Add(new Peao(this));
		}
	}

	// função que define se um jogador está em xeque
	// e também aparentemente também verifica se algum movimento inimigo envolve captura em direção ao rei (posso estar errado =x)
													 
	public bool EmXeque()
	{
		List<Movimento> movimentos;             // verifica todos os movimentos do seu inimigo
		foreach (Peca i in inimigo.conjuntoPecas)
		{
			if (i.CasaAtual != null)   // se o inimigo tiver alguma peça em alguma casa (se ele tem alguma peça que não foi capturada)
			{		
				movimentos = i.ListaMovimentos(i.CasaAtual.Tabuleiro, i.CasaAtual, false); 
			
				if (movimentos.Count > 0)
				{
					foreach (Movimento mov in movimentos)  
					{
						if (mov.destino.PecaAtual is Rei && mov.tipo != Movimento.Tipo.SemCaptura) // se alguma das possibilidades de movimento envolve captura em direção ao rei
						{							
							return true;
						}
					}
				}
			}
		}

		return false;
	}

}
