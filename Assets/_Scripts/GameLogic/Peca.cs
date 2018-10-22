﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca
{
	public Casa CasaAtual { get; private set; }

	public int PosX { get { return CasaAtual.PosX; } }
	public int PosY { get { return CasaAtual.PosY; } }
	public char Cor { get; private set; }
	public Jogador jDono;
    public UIPiece uiP;
	public bool primeiraJogada;

	public Peca(Jogador j)
	{
		CasaAtual = null;
		Cor = j.Cor;
		primeiraJogada = true;
		jDono = j;
	}

    //só para o peao para verificar captura com rei
    

	// TODO: retirar "tabuleiro" do parâmetro, já que a casa tem uma referência a ele
    public virtual List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem,bool verificaXeque = true, bool verificaCaptura = false)
	{
		return null;
	}

	// realiza a movimentação baseado em um único movimento
	public void RealizaMovimento(Movimento m)
	{
		//verifica se tem captura de peça
		if (m.destino.PecaAtual != null)
		{
			Jogador jCapturado = m.destino.PecaAtual.jDono;
			int posPeca = 0;

			//verifico todas as peças até achar a que eu quero
			foreach (Peca p in jCapturado.conjuntoPecas)
			{
				if (p == m.destino.PecaAtual)
				{
					break;
				}
				posPeca++;
			}
			jCapturado.conjuntoPecas.RemoveAt(posPeca);

		}
		//realiza o movimento
		m.destino.ColocarPeca(m.origem.PopPeca());

		primeiraJogada = false;

		//verifica se é peao e se chegou ao fim do tabuleiro, se sim, muda o tipo de peça
		if ((this is Peao) && (this as Peao).PodePromover())// (m.destino.PosX == tamTabuleiro - 1))
		{
            CasaAtual.Tabuleiro.partida.UItab.ativaPromocao(m);
			//PromoverPeao(m);
		}
	}

	public Peca PromoverPeao(Movimento m,int tipoNovaPeca)
	{
		int indicePeao = 0;
		Peca peaoAtual = m.destino.PecaAtual;

		//tipoNovaPeca = FUNÇÂOQUEUSAINTERFACEPARADEFINIR (TODO)

		// acha a posicao do peao no array de peças do jogador
		foreach (Peca p in peaoAtual.jDono.conjuntoPecas)
		{
			if (p == peaoAtual)
			{
				break;
			}
			indicePeao++;
		}

		Peca novaPeca;
		//se 1, então vira rainha
		//se 2, então vira torre
		//se 3, então vira cavalo
		//senao, então vira Bispo       
		if (tipoNovaPeca == 1)
		{
			novaPeca = new Rainha(peaoAtual.jDono);
		}
		else if (tipoNovaPeca == 2)
		{
			novaPeca = new Torre(peaoAtual.jDono);
		}
		else if (tipoNovaPeca == 3)
		{
			novaPeca = new Cavalo(peaoAtual.jDono);
		}
		else
		{
			novaPeca = new Bispo(peaoAtual.jDono);
		}

		//define a posição e salva a peça na casa e no jogador
		novaPeca.jDono.conjuntoPecas[indicePeao] = novaPeca;
		m.destino.ColocarPeca(novaPeca);
        novaPeca.CasaAtual = m.destino;

        return novaPeca;
	}

	// Quando uma casa colocar esta peça como atual,
	// esta precisa colocar tal casa como atual.
	public void ValidarNovaCasa(Casa casa)
	{
		if (casa.PecaAtual == this)
			CasaAtual = casa;
	}

	public void TirarDaCasaAtual()
	{
		if (CasaAtual == null)
			return;

		if (CasaAtual.PecaAtual == this)
			CasaAtual.PopPeca();

		CasaAtual = null;
	}

	/* 
	public Casa GetCasaAtual(Tabuleiro tabuleiro)
	{
		if (tabuleiro == null)
		{
			Debug.LogError("Tabuleiro passado é nulo.");
			return null;
		}

		Casa casaAtual = tabuleiro.GetCasa(this.PosX, this.PosY);

		if (casaAtual == null) return null;

		if (casaAtual.PecaAtual != this)
		{
			Debug.LogError("A peça examinada e a peça na casa representada pelo seu X e Y neste tabuleiro são diferentes!");
			return null;
		}
		
		return casaAtual;
	}
	*/

	// Genú: Por enquanto, a função abaixo está bem basicona. Será que,
	// além das regras do xeque, que já foram programadas, 
	// há alguma outra verificação?
	public bool PodeCapturar(Peca alvo)
	{
		if (this.Cor == alvo.Cor)
			return false;
		if (this.Cor != alvo.Cor)
			return true;

		return false;
	}

	public bool PodePercorrer(Movimento movimento, Tabuleiro tabuleiro)
	{
		if (CasaAtual != movimento.origem)
			return false;

		List<Movimento> possibilidades = ListaMovimentos(tabuleiro, CasaAtual);

		foreach (var possibilidade in possibilidades)
			if (possibilidade.destino == movimento.destino)
				return true;

		return false;
	}
}