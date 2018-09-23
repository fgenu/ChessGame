using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casa
{
	public Peca pecaAtual = null;
	public char cor;
	public int posX, posY;
	public Casa(int x, int y)
	{
		cor = 'i';
		pecaAtual = null;
		posX = x;
		posY = y;
	}

	public bool EstaOcupada()
	{
		if (pecaAtual != null)
			return true;
		else
			return false;
	}

}
