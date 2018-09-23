using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peao : Peca
{
	bool primeiraJogada = true;
	public Peao(bool jogadorCima, Jogador j) : base(jogadorCima, j)
	{

	}
	public override List<Movimento> ListaMovimentos(Casa[,] tab, int x, int y)
	{
		List<Movimento> movimentos = new List<Movimento>();
        //int quantMovimentos;

        //antes verifica por cada casa se tem casa aliada
        //caso ele seja o jogador de jogadorCima, tem que se movimentar para baixo
        if (!jogadorCima)
		{
			//verifica se o peao pode comer casa e cria o movimento para esquerdo inferior
			if (x + 1 < tamTabuleiro && y - 1 >= 0)
			{
				if (tab[x + 1, y - 1].pecaAtual != null)
                {
                    if(tab[x + 1, y - 1].pecaAtual.jDono != tab[x,y].pecaAtual.jDono) movimentos.Add(new Movimento(tab[x + 1, y-1], tab[x, y]));
                }
			}
			//verifica se o peao pode comer casa e cria o movimento para direito inferior
			if (x + 1 < tamTabuleiro && y + 1 < tamTabuleiro)
			{
				if (tab[x + 1, y + 1].pecaAtual != null)
                {
                    if (tab[x + 1, y + 1].pecaAtual.jDono != tab[x, y].pecaAtual.jDono) movimentos.Add(new Movimento(tab[x + 1, y+1], tab[x, y]));
                }
			}
			//verifica se é a primeira jogada da peça e permite mover mais
			if (primeiraJogada)
            {
                if (x + 2 < tamTabuleiro)
                {
                    if (tab[x + 2, y].pecaAtual.jDono != tab[x, y].pecaAtual.jDono) movimentos.Add(new Movimento( tab[x + 2, y], tab[x, y]));
					primeiraJogada=false;
                }

            }
			//movimento normal,um abaixo
			if (x + 1 < tamTabuleiro)
			{

                if (tab[x + 1, y].pecaAtual.jDono != tab[x, y].pecaAtual.jDono) movimentos.Add(new Movimento(tab[x + 1, y], tab[x, y]));

			}
		}
		//senão ele considera a peça subindo no tabuleiro
		else
		{

			//verifica se o peao pode comer casa e cria o movimento para esquerdo inferior
			if (x - 1 >= 0 && y - 1 >= 0)
			{
				if (tab[x - 1, y - 1].pecaAtual != null)
                {
                    if (tab[x - 1, y - 1].pecaAtual.jDono != tab[x, y].pecaAtual.jDono) movimentos.Add(new Movimento(tab[x - 1, y-1], tab[x, y]));
                }
			}
			//verifica se o peao pode comer casa e cria o movimento para direito inferior
			if (x - 1 >= 0 && y + 1 < tamTabuleiro)
			{
				if (tab[x - 1, y + 1].pecaAtual != null)
                {
                    if (tab[x - 1, y + 1].pecaAtual.jDono != tab[x, y].pecaAtual.jDono) movimentos.Add(new Movimento(tab[x - 1, y+1], tab[x, y]));
                }
			}
			//verifica se é a primeira jogada da peça e permite mover mais
			if (primeiraJogada)
			{
                if (x - 2 >= 0)
                {
                    if (tab[x - 2, y].pecaAtual.jDono != tab[x, y].pecaAtual.jDono) movimentos.Add(new Movimento(tab[x - 2, y], tab[x, y]));
                }
			}
			//movimento normal,um abaixo
			if (x - 1 >= 0)
			{

                if (tab[x - 1, y].pecaAtual.jDono != tab[x, y].pecaAtual.jDono) movimentos.Add(new Movimento(tab[x - 1, y], tab[x, y]));

			}
		}

		return movimentos;
	}
}
