using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

public class Casa
{
    public Peca PecaAtual;
	public char cor = 'i';
	public int PosX { get; private set; }
	public int PosY { get; private set; }
	public Tabuleiro Tabuleiro { get; private set; }

	public Casa(int x, int y, Tabuleiro tabuleiro)
	{
		cor = 'i';
		PecaAtual = null;
		PosX = x;
		PosY = y;
		Tabuleiro = tabuleiro;
		if(x < 0 || x > 7)
		{
			throw new ArgumentOutOfRangeException("x", "Posição X invalida");

		}
		if( y < 0 || y > 7)
		{
			throw new ArgumentOutOfRangeException("y","Posição Y invalida");
		}
	}

	public void ColocarPeca(Peca peca)
	{
		if (PecaAtual != null)
			PopPeca(); // TODO: destruir a peça removida OU NÃO, dependendo de como armazenarmos o histórico

		PecaAtual = peca;

		peca.ValidarNovaCasa(this);
	}

	public Peca PopPeca()
	{
		Peca removida = PecaAtual;

		PecaAtual = null;

		removida.TirarDaCasaAtual();

		return removida;
	}

	public bool EstaOcupada()
	{
		if (PecaAtual != null)
			return true;
		else
			return false;
	}

}
