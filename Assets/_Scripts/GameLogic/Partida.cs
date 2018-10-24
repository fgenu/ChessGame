using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Partida
{

	public Tabuleiro Tabuleiro { get; private set; }
	public Jogador Jogador1 { get; private set; }
	public Jogador Jogador2 { get; private set; }
    public UITabuleiro UItab;
    public int Turno { get; private set; }
    public int turnospassados=0; // acredito que isso vai ser importante mais tarde para implementar empate.
    // depois deve criar uma função que incrementa essa variavel ou incrementar ela na função que troca os turnos (TODO)


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

	// definem o final do jogo 
	public bool VerificaVitoria()
	{
		// para ocorrer a vitoria é preciso que o rei adversario esteja em uma posição em que seja impossivel escapar 
		// ou seja xeque mate:
		// ocorre quando: 
		// o rei não pode se movimentar para nenhuma casa sem que sofra um ataque(não há movimentos validos)
		// a peça(s) que esta(m) atacando não podem ser capturadas pelo rei
		//nenhuma peça pode proteger o rei (entrar na frente do atacante)
		int numameacas;
		int numprote;
		// O rei está em xeque?	
		if (JogadorDaVez().inimigo.EmXeque())
		{
			foreach (Peca peca in JogadorDaVez().inimigo.conjuntoPecas) // loop para achar o rei no conjunto de pecas inimigas
			{
				
				
				if(peca is Rei)
				{
					// agora deve-se verificar se o rei inimigo possui algum movimento valido (ou seja nenhum movimento o deixa em xeque ainda)
					if (peca.ListaMovimentos(Tabuleiro, peca.CasaAtual).Count > 0 )
					{
			
						return false;
					}
					//agora deve-se verificar se as peças que ameaçam o rei estão a mais de uma casa de distancia dele(nao podem ser capturadas pelo rei)
					foreach (Peca pa in JogadorDaVez().conjuntoPecas)
					{
						if (pa.CasaAtual != null)  
						{		
							movimentos = pa.ListaMovimentos(pa.CasaAtual.Tabuleiro, pa.CasaAtual, false); 
							if (movimentos.Count > 0)
							{
								foreach (Movimento mov in movimentos)  
								{
									if (mov.destino.PecaAtual is Rei && mov.tipo != Movimento.Tipo.SemCaptura) // se alguma das possibilidades de movimento envolve captura em direção ao rei
									{							
										
										if( (peca.PosY == pa.PosY && Math.Abs(peca.PosX - pa.PosX) == 1 )|| (peca.PosX == pa.PosX && Math.Abs(peca.PosY - pa.PosY) == 1 ) || ( (Math.Abs(peca.PosX - pa.PosX) == 1) && ( Math.Abs(peca.PosY - pa.PosY) == 1 ) ) ) 
										{
											return false;
										}
										numameacas++;
									}
								}
							}
						}
					}
					// agora deve-se verificar se existe alguma peça para proteger o rei(pode ser movida para ao lado do rei)
					// (acho que deve ter alguma forma de deixar esse código melhor sem ter que chamar outro foreach aqui, mas depois eu faço isso) 
					//TODO*
					foreach (Peca prot in JogadorDaVez().inimigo.conjuntoPecas)
					{	
						if(!(prot is Rei))
						{
							if (prot.CasaAtual != null)  
							{
								movimentos = prot.ListaMovimentos(prot.CasaAtual.Tabuleiro, prot.CasaAtual, false);
								foreach (Movimento mov in movimentos)
								{
									if( (peca.PosY == mov.destino.PosY && Math.Abs(peca.PosX - mov.destino.PosX) == 1 )|| (peca.PosX == mov.destino.PosX && Math.Abs(peca.PosY - mov.destino.PosY) == 1 ) || ( (Math.Abs(peca.PosX - mov.destino.PosX) == 1) && ( Math.Abs(peca.PosY - mov.destino.PosY) == 1 ) )  )
									{
										numprote++;
									}
								}
							}
						}
					}
					if(numprote == numameacas)
					{
						return false;
					}




					break; // evitar percorrer o resto do conjunto de pecas atoa

				}
				
				
			}

		

		
		}

		return true;

		
	}

	// definem o final do jogo (TODO) (deve verificar afogamento de rei, impossibilidade de xeque, regra dos 50 movimentos)
	public bool VerificaEmpate()
	{
		return false;
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
