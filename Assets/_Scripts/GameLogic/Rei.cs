using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : Peca
{
    public Jogador inimigo;
	public Rei(Jogador j) : base(j)
	{

	}
    public override void defineInimigo(Jogador i)
    {
        inimigo = i;
    }
    //função que verifica todos os movimentos das peças inimigas para verificar se o rei pode mover para a casa sem ter xeque
    public bool podeMoverXeque(Casa[,] tab,int x, int y)
    {
        List<Movimento> movimentos;
        foreach (Peca p in inimigo.conjuntoPecas)
        {
            if(!(p is Rei))
            {
                movimentos = p.ListaMovimentos(tab, p.posX, p.posY);
                if (movimentos != null)
                {
                    foreach (Movimento mov in movimentos)
                    {
                        if (mov.destino == tab[x, y])
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

	public override List<Movimento> ListaMovimentos(Casa[,] tab, int x, int y)
	{
		List<Movimento> movimentos = new List<Movimento>();
        //int quantMovimentos;

        //rei pode ir tanto para frente ou atras então movimento unico
        //verifica se pode mover para jogadorCima do tabuleiro

        //funciona por verificando linha, depois coluna, mas para cada casa 
        //verifica se pode mover por xeque, depois verifica se tem casa aliada na posição
		if (x - 1 >= 0)
		{
            if(podeMoverXeque(tab,x-1,y))
            {
                if (tab[x - 1, y ].PecaAtual == null || tab[x - 1, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) 
                    movimentos.Add(new Movimento(tab[x - 1, y], tab[x, y]));
            }

            if (y - 1 >= 0)
			{
                if (podeMoverXeque(tab, x - 1, y-1)) 
                    if (tab[x - 1, y - 1].PecaAtual == null || tab[x - 1, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
                    {
                        movimentos.Add(new Movimento( tab[x - 1, y - 1], tab[x, y]));
                    }
            }
			if (y + 1 < tamTabuleiro)
			{
                if (podeMoverXeque(tab, x - 1, y+1)) 
                    if (tab[x - 1, y + 1].PecaAtual == null || tab[x - 1, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
                    {
                        movimentos.Add(new Movimento(tab[x - 1, y + 1], tab[x, y]));
                    }
            }
		}
		//verifica embaixo do tabuleiro
		if (x + 1 < tamTabuleiro)
		{
            if (podeMoverXeque(tab, x + 1, y))
                if (tab[x + 1, y].PecaAtual == null || tab[x + 1, y].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
                {
                    movimentos.Add(new Movimento(tab[x + 1, y], tab[x, y]));
                }

            if (y - 1 >= 0)
			{
                if (podeMoverXeque(tab, x + 1, y-1))
                {
                    if (tab[x + 1, y - 1].PecaAtual == null || tab[x + 1, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) 
                        movimentos.Add(new Movimento(tab[x + 1, y - 1], tab[x, y]));
                }
            }
			if (y + 1 < tamTabuleiro)
			{
                if (podeMoverXeque(tab, x + 1, y+1)) 
                    if (tab[x + 1, y + 1].PecaAtual == null || tab[x + 1, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
                    {
                        movimentos.Add(new Movimento(tab[x + 1, y + 1], tab[x, y]));
                    }
            }

		}
		//verifica os lados
		if (y - 1 >= 0)
		{
            if (podeMoverXeque(tab, x, y-1))
            {
                if (tab[x, y - 1].PecaAtual == null || tab[x, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) movimentos.Add(new Movimento( tab[x, y -1], tab[x, y]));
            }
        }
		if (y + 1 < tamTabuleiro)
		{
            if (podeMoverXeque(tab, x , y+1)) if (tab[x, y + 1].PecaAtual == null || tab[x, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono)
                {
                    movimentos.Add(new Movimento(tab[x, y + 1], tab[x, y]));
                }
        }

		return movimentos;
	}

    public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem)
    {
        return ListaMovimentos(tabuleiro.tabuleiro, origem.PosX, origem.PosY);
    }

}
