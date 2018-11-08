using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peao : Peca
{
	public Peao(Jogador j) : base(j)
	{

	}

	/*
	public override List<Movimento> ListaMovimentos(Casa[,] tab, int x, int y)
	{
		List<Movimento> movimentos = new List<Movimento>();
        //int quantMovimentos;

        //antes verifica por cada casa se tem casa aliada
        //caso ele seja o jogador de jogadorCima, tem que se movimentar para baixo
        if (!jDono.jogadorCima)
		{
			//verifica se o peao pode comer casa e cria o movimento para esquerdo inferior
			if (x + 1 < tamTabuleiro && y - 1 >= 0)
			{
				if (tab[x + 1, y - 1].PecaAtual != null)
                {
                    if(tab[x + 1, y - 1].PecaAtual.jDono != tab[x,y].PecaAtual.jDono) 
						movimentos.Add(new Movimento(tab[x + 1, y-1], tab[x, y]));
                }
			}
			//verifica se o peao pode comer casa e cria o movimento para direito inferior
			if (x + 1 < tamTabuleiro && y + 1 < tamTabuleiro)
			{
				if (tab[x + 1, y + 1].PecaAtual != null)
                {
                    if (tab[x + 1, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) 
						movimentos.Add(new Movimento(tab[x + 1, y+1], tab[x, y]));
                }
			}
			//verifica se é a primeira jogada da peça e permite mover mais
			if (primeiraJogada)
            {
                if (x + 2 < tamTabuleiro)
                {
                    if (tab[x + 2, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) 
						movimentos.Add(new Movimento( tab[x + 2, y], tab[x, y]));
                }

            }
			//movimento normal,um abaixo
			if (x + 1 < tamTabuleiro)
			{

                if (tab[x + 1, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) 
					movimentos.Add(new Movimento(tab[x + 1, y], tab[x, y]));

			}
		}
		//senão ele considera a peça subindo no tabuleiro
		else
		{

			//verifica se o peao pode comer casa e cria o movimento para esquerdo inferior
			if (x - 1 >= 0 && y - 1 >= 0)
			{
				if (tab[x - 1, y - 1].PecaAtual != null)
                {
                    if (tab[x - 1, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) 
						movimentos.Add(new Movimento(tab[x - 1, y-1], tab[x, y]));
                }
			}
			//verifica se o peao pode comer casa e cria o movimento para direito inferior
			if (x - 1 >= 0 && y + 1 < tamTabuleiro)
			{
				if (tab[x - 1, y + 1].PecaAtual != null)
                {
                    if (tab[x - 1, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) 
						movimentos.Add(new Movimento(tab[x - 1, y+1], tab[x, y]));
                }
			}
			//verifica se é a primeira jogada da peça e permite mover mais
			if (primeiraJogada)
			{
                if (x - 2 >= 0)
                {
                    if (tab[x - 2, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) 
						movimentos.Add(new Movimento(tab[x - 2, y], tab[x, y]));
                }
			}
			//movimento normal,um abaixo
			if (x - 1 >= 0)
			{

                if (tab[x - 1, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) 
					movimentos.Add(new Movimento(tab[x - 1, y], tab[x, y]));

			}
		}

		return movimentos;
	}
	*/

	public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem,bool verificaXeque=true,bool verificaCaptura=false)
	{
		List<Movimento> movimentos = new List<Movimento>();

		int mod;

		// caso ele seja do jogador de cima, tem que se movimentar para baixo
		if (jDono.jogadorCima)
			mod = -1;
		// senão ele considera a peça subindo no tabuleiro
		else
			mod = +1;
        //funcao so para verificar o xeque
        if (verificaCaptura)
        {

            movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 1, 1 * mod, passos: 1, tipo: Movimento.Tipo.SemCaptura, verificaXeque: verificaXeque));
            movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, 1 * mod, passos: 1, tipo: Movimento.Tipo.SemCaptura, verificaXeque: verificaXeque));
        }
        else
        {
            // Movimentos sem captura
            if (primeiraJogada)
                movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, 1 * mod, passos: 2, tipo: Movimento.Tipo.SemCaptura, verificaXeque: verificaXeque));
            else
                movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, 1 * mod, passos: 1, tipo: Movimento.Tipo.SemCaptura, verificaXeque: verificaXeque));

        }


        // Movimentos com captura
        movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 1, 1 * mod, passos: 1, tipo: Movimento.Tipo.SomenteCaptura, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, 1 * mod, passos: 1, tipo: Movimento.Tipo.SomenteCaptura, verificaXeque: verificaXeque));

		// TODO: en passant. Ideia: poder verificar o tabuleiro do turno anterior, ou usar um "peão fantasma" como peça.


		return movimentos;
	}

	public bool PodePromover()
	{
		if (PosY == 0)
			if (jDono.jogadorCima)
				return true;

		if (PosY == CasaAtual.Tabuleiro.Tamanho - 1)
			if (!jDono.jogadorCima)
				return true;

		return false;
	}
}
