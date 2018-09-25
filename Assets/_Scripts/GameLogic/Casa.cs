using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casa
{
	public Peca PecaAtual { get; private set; }
	public char cor = 'i';
	public int PosX {get; private set;} 
	public int PosY {get; private set;}

	public Casa(int x, int y)
	{
		cor = 'i';
		PecaAtual = null;
		PosX = x;
		PosY = y;
	}

	public void ColocarPeca(Peca peca)
	{
		if(PecaAtual != null)
			PopPeca(); // TODO: destruir a peça removida OU NÃO, dependendo de como armazenarmos o histórico
		
		PecaAtual = peca;
		peca.SetPosition(PosX, PosY);
	}

	public Peca PopPeca()
	{
		PecaAtual.SetPosition(-1, -1);

		Peca removida = PecaAtual;
		PecaAtual = null;

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
