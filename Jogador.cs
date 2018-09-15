using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador
{
	public Peca[] conjuntoPecas = new Peca[16];
	char Cor { get; private set; }
	public Jogador(char cor, bool cima)
	{
		Cor = cor;
		InicializaPecas(cima);
	}
	//inicializa as pecas do jogador
	void InicializaPecas(bool cima)
	{
		//inicializa as peças especiais da linha mais a borda
		conjuntoPecas[0] = new Torre(Cor, cima, this); // Genú: (TODO) É desnecessário passar Cor e this, porque o this já tem cor.
		conjuntoPecas[1] = new Cavalo(Cor, cima, this);
		conjuntoPecas[2] = new Bispo(Cor, cima, this);
		conjuntoPecas[3] = new Rainha(Cor, cima, this);
		conjuntoPecas[4] = new Rei(Cor, cima, this);
		conjuntoPecas[5] = new Bispo(Cor, cima, this);
		conjuntoPecas[6] = new Cavalo(Cor, cima, this);
		conjuntoPecas[7] = new Torre(Cor, cima, this);

		//inicializa os peoes
		for (int i = 8; i < 16; i++)
		{
			conjuntoPecas[i] = new Peao(Cor, cima, this);
		}
	}
}
