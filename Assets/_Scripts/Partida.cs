using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partida : MonoBehaviour
{

	Tabuleiro tabuleiro;
	public Jogador Jogador1 { get; private set; }
	public Jogador Jogador2 { get; private set; }

	void Awake()
	{
		Jogador1 = new Jogador('b', false);
		Jogador2 = new Jogador('p', true);
	}

	public Jogador JogadorDeCima()
	{
		if (Jogador1.jogadorCima && !Jogador2.jogadorCima)
			return Jogador1;
		else if (Jogador2.jogadorCima && !Jogador1.jogadorCima)
			return Jogador2;
		else
		{
			Debug.LogError("Exatamente um jogador deve ficar na parte de cima do tabuleiro.");
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
