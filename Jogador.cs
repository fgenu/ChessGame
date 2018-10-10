using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

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
}
