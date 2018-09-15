using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador
{
	public Peca[] conjuntoPecas = new Peca[16];
    public bool jogadorCima;
	public char Cor { get; private set; }
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
		conjuntoPecas[0] = new Torre(jogadorCima, this); 
		conjuntoPecas[1] = new Cavalo(jogadorCima, this);
		conjuntoPecas[2] = new Bispo(jogadorCima, this);
		conjuntoPecas[3] = new Rainha(jogadorCima, this);
		conjuntoPecas[4] = new Rei(jogadorCima, this);
		conjuntoPecas[5] = new Bispo(jogadorCima, this);
		conjuntoPecas[6] = new Cavalo(jogadorCima, this);
		conjuntoPecas[7] = new Torre(jogadorCima, this);

		//inicializa os peoes
		for (int i = 8; i < 16; i++)
		{
			conjuntoPecas[i] = new Peao(jogadorCima, this);
		}
	}
}
