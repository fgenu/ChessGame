using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Partida
{

	public Tabuleiro Tabuleiro { get; private set; }
	public Jogador Jogador1 { get; private set; }
	public Jogador Jogador2 { get; private set; }
	public IA jIA;
	public UITabuleiro UItab;
	public int TurnoAtual { get; private set; }   // depois de consertar o problema do nullpointer pode por isso como private denovo.
	public int TurnoDaUltimaCaptura { get; /*private*/ set; }
	public List<String> HistoricoDoTabuleiro { get; private set; }
	public bool fim { get; private set; }


	public Partida()
	{
		Tabuleiro = new Tabuleiro();
		Jogador1 = new Jogador('b', false);
		Jogador2 = new Jogador('p', true);
		Tabuleiro.partida = this;
		jIA = new IA(Jogador2);
		HistoricoDoTabuleiro = new List<String>();
		IniciarPartida(Jogador1, Jogador2);
		this.fim = false;
	}

	public void PassarAVez()
	{

		if (VerificaEmpateObrigatorio())   
		{
            UItab.ativaFim("               Empate!");
			Debug.Log("Empate!");
			this.fim = true;
		}

        
        if (VerificaVitoria())
		{
            if (JogadorDaVez() == Jogador1)
            {
                UItab.ativaFim("               Você Ganhou!");
            }
            else
            {
                UItab.ativaFim("               Você Perdeu!");
            }
            Debug.Log("Vitória!");
			this.fim = true;
			RegistrarEstadoDoTabuleiro();
			return; // coloquei esse return temporariamente aqui por causa do nullpointer
		}

        if (!UItab.promovendoPeao)
        {
            if (JogadorDaVez() == Jogador1)
            {
				TurnoAtual++;
                //Tabuleiro.PrintaTabuleiro();
                Movimento m = jIA.minmax(3, 3, -222222222, 2222222222, true, Tabuleiro, null).movimento;
                if (m != null)
				{
					//Debug.Log(m.origem.PosX + "   " + m.origem.PosY);
					UItab.TryMove(m.origem.uiC, m.destino.uiC);
				}
				//Tabuleiro.PrintaTabuleiro();
            }
            else
            {
				TurnoAtual++;
                //Tabuleiro.PrintaTabuleiro();
                Movimento m = jIA.minmax(3, 3, -222222222, 2222222222, true, Tabuleiro, null).movimento;
				if (m != null)
				{
					//Debug.Log(m.origem.PosX + "   " + m.origem.PosY);
					UItab.TryMove(m.origem.uiC, m.destino.uiC);
				}
                //Tabuleiro.PrintaTabuleiro();
            }
        }

        if (VerificaEmpateOpcional())
		{
			Debug.Log("Um empate pode ser pedido!");
		}

        RegistrarEstadoDoTabuleiro();
	}

    public Jogador JogadorDaVez()
	{
		if (TurnoAtual % 2 == 1) // turno ímpar
		{
			if (Jogador1.Cor == 'b')
				return Jogador1;
			else
				return Jogador2;
		}
		else // turno par
		{
			if (Jogador1.Cor == 'p')
				return Jogador1;
			else
				return Jogador2;
		}
	}

	void IniciarPartida(Jogador j1, Jogador j2)
	{
		j1.inimigo = j2;
		j2.inimigo = j1;
		TurnoAtual = 1;

		Tabuleiro.InserePecasNaPosicaoInicial(this);
		Tabuleiro.PrintaTabuleiro();
		
		HistoricoDoTabuleiro.Clear();
		RegistrarEstadoDoTabuleiro();
	}
	
	public bool VerificaVitoria()
	{
		if (JogadorDaVez().inimigo.EmXeque())
		{
			foreach (Peca peca in JogadorDaVez().inimigo.conjuntoPecas)
			{
				if (peca != null)
				{
					if (peca.ListaMovimentos().Count > 0)
					{
						return false;
					}
				}
			}

			return true;
		}
		
		return false;
	}



	// definem o final do jogo (TODO nos sonhos: verificar mais impossibilidade de xeque (é muito complicado de calcular, acho que podemos ignorar))
	private bool VerificaEmpateObrigatorio()
	{
		var pecasAliadas = JogadorDaVez().conjuntoPecas;
		var pecasInimigas = JogadorDaVez().inimigo.conjuntoPecas;

		// Afogamento
		bool semMovimentos = true;

		foreach (Peca peca in pecasInimigas)
		{
			if (peca.ListaMovimentos().Count > 0)  // AQUI OCORRE NULL POINTER
			{
				semMovimentos = false;
				break;
			}
		}

		if (semMovimentos && !JogadorDaVez().inimigo.EmXeque())
		{
			Debug.Log("Empate! Rei afogado.");
			return true;
		}


		// Impossibilidade de xeque por falta de peças (supõe que cada jogador possui um Rei)
		if (pecasInimigas.Count == 1)
		{
			if (pecasAliadas.Count == 1) // Apenas Reis
			{
				Debug.Log("Empate! Xeque impossível.");
				return true;
			}

			if (pecasAliadas.Count == 2)
			{
				foreach (var peca in pecasAliadas)
				{
					if (peca is Bispo || peca is Cavalo) // Apenas Rei e Bispo ou Rei e Cavalo
					{
						Debug.Log("Empate! Xeque impossível.");
						return true;
					}
				}
			}
		}
		else if (pecasInimigas.Count == 2 && pecasAliadas.Count == 2)
		{
			foreach (Peca peca in pecasAliadas)
			{
				if (peca is Bispo)
				{
					foreach (Peca inimiga in pecasInimigas)
					{
						if (inimiga is Bispo && peca.CasaAtual.cor == inimiga.CasaAtual.cor) // Apenas Rei e Bispo dos dois lados, bispos em casa de mesma cor
						{
							Debug.Log("Empate! Xeque impossível.");
							return true;
						}
					}
				}
			}
		}


		return false;
	}

	private bool VerificaEmpateOpcional()
	{
		// 50 ou mais turnos desde a última captura ou movimento de peão

		if (TurnoAtual >= 50)
		{
			if (TurnoDaUltimaCaptura + 50 <= TurnoAtual)
				{ Debug.Log("Mais de 50 turnos passaram desde a última captura!"); return true;}
			
			int ultimoTurnoComMovimentoDePeao = 0;
			foreach (Peca peca in Jogador1.conjuntoPecas.Union(Jogador2.conjuntoPecas))
			{
				if (peca is Peao && peca.UltimoTurnoMovido > ultimoTurnoComMovimentoDePeao)
					ultimoTurnoComMovimentoDePeao = peca.UltimoTurnoMovido; // é mais recente
			}
			if(ultimoTurnoComMovimentoDePeao + 50 <= TurnoAtual)
			{
				Debug.Log("Mais de 50 turnos passaram desde o último movimento de peão!");
				return true;
			}
		}


		// Repetição do mesmo estado do tabuleiro três vezes. Empate pode ser pedido apenas logo que a repetição acontece.

		string estado = EstadoAtual();
		int contagem = 0;
		foreach (string registro in HistoricoDoTabuleiro)
		{
			if (string.Compare(estado, registro) == 0)
			{
				contagem++;

				if (contagem >= 2) // dependendo da ordem de chamada desta função, a contagem deve ser >= 2 ou 3.
				{
					Debug.Log("Três ou mais repetições do estado do tabuleiro!");
					return true;
				}
			}
		}

		return false;
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

	public String EstadoAtual()
	{
		String str = "A jogar: " + JogadorDaVez().Cor + "\n";

		for (int i = 0; i < Tabuleiro.Tamanho; i++)
		{
			for (int j = 0; j < Tabuleiro.Tamanho; j++)
			{
				str += "Casa (" + i + "," + j + ") ";
				Peca peca = Tabuleiro.GetCasa(i, j).PecaAtual;

				if (peca == null)
				{
					str += "__";
				}
				else
				{
					if (peca is Torre)
					{
						str += "T";
					}
					else if (peca is Cavalo)
					{
						str += "C";
					}
					else if (peca is Bispo)
					{
						str += "B";
					}
					else if (peca is Rei)
					{
						str += "E";
					}
					else if (peca is Rainha)
					{
						str += "A";
					}
					else if (peca is Peao)
					{
						str += "P";
					}
					else
					{
						str += "?";
						Debug.LogWarning("Tipo de peça desconhecido ao registrar no histórico.");
					}

					str += peca.Cor;
					str += peca.ListaMovimentosToString();
				}
				str += "\n";
			}
			str += "\n";
		}

		return str;
	}



	private void RegistrarEstadoDoTabuleiro()
	{
		HistoricoDoTabuleiro.Add(EstadoAtual());
	}


}