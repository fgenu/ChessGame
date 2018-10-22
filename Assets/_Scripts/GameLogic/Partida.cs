using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partida
{

	public Tabuleiro Tabuleiro { get; private set; }
	public Jogador Jogador1 { get; private set; }
	public Jogador Jogador2 { get; private set; }
    public UITabuleiro UItab;
    public int Turno { get; private set; }

	public Partida()
	{
		Tabuleiro = new Tabuleiro();
		Jogador1 = new Jogador('b', false);
		Jogador2 = new Jogador('p', true);
        Tabuleiro.partida = this;
		IniciarPartida(Jogador1, Jogador2);
	}

	// TODO: avaliar necessidade desta função
    public int retornaTurno()
    {
        return Turno;
    }

	public void PassarAVez()
    {
        VerificaVitoria();
		if (Turno == 1)
        {
            Turno = 2;
        }
        else
        {
            Turno = 1;
        }
        Debug.Log(Turno);

    }

	private Jogador JogadorDaVez()
	{
		if (Turno == 1) return Jogador1;
		else return Jogador2;
	}

	private void VerificaVitoria()
	{
		// Há movimentos possíveis?
		foreach (Peca peca in JogadorDaVez().inimigo.conjuntoPecas)
		{
			if (peca.ListaMovimentos(Tabuleiro, peca.CasaAtual).Count > 0)
			{
				// Sim
				return;
			}
		}

		// O rei está em xeque?
		if (JogadorDaVez().inimigo.EmXeque())
		{
			Debug.Log("Xeque-mate!");
		}

		Debug.Log("Fim");
	}

	void IniciarPartida(Jogador j1, Jogador j2)
	{
		j1.inimigo = j2;
		j2.inimigo = j1;
        if (j1.Cor == 'b')
        {
            Turno = 1;
        }
        else
        {
            Turno = 2;
        }
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
