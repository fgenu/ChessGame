using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

public class Partida
{

	public Tabuleiro Tabuleiro { get; private set; }
	public Jogador Jogador1 { get; private set; }
	public Jogador Jogador2 { get; private set; }

	public Partida()
	{
		Tabuleiro = new Tabuleiro();
	}

	public void definirJogador(int cor){
		if(cor == 1){
			Jogador1 = new Jogador('b', false);
			Jogador2 = new Jogador('p', true);
		}
		else{
			Jogador1 = new Jogador('p', false);
			Jogador2 = new Jogador('b', true);
		}
		IniciarPartida(Jogador1, Jogador2);
	}

	void IniciarPartida(Jogador j1, Jogador j2)
	{
		j1.inimigo = j2;
		j2.inimigo = j1;

		Tabuleiro.InserePecasNaPosicaoInicial(this);
		Tabuleiro.PrintaTabuleiro();
	}

	public Jogador JogadorDeCima()
	{
		if (Jogador1.jogadorCima && !Jogador2.jogadorCima)
			return Jogador1;
		else if (Jogador2.jogadorCima && !Jogador1.jogadorCima)
			return Jogador2;
		else
		{
		//	Debug.LogError("Exatamente um jogador deve ficar na parte de cima do tabuleiro.");
			return null;
		}
	}

	public Jogador JogadorDeBaixo()
	{
		Jogador oOutro = JogadorDeCima();
		if (oOutro == Jogador1)
			return Jogador2;
		else if (oOutro == Jogador2)
			return Jogador1;
		else
			return null;
	}


}
