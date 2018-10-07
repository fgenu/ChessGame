using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalo : Peca
{
	public Cavalo(Jogador j) : base(j)
	{

	}

	/*
    public override List<Movimento> ListaMovimentos(Casa[,] tab, int x, int y)
    {
        List<Movimento> movimentos = new List<Movimento>();
        //int quantMovimentos;

        //verifica cada lugar que o cavalo pode ir
        //antes verifica por cada casa se tem casa aliada
        //verifica se pode mover para jogadorCima 2x e esquerda 1x
        if (x - 2 >= 0 && y - 1 >= 0)
        {
            if (tab[x - 2, y - 1].PecaAtual == null || tab[x -2, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) movimentos.Add(new Movimento(tab[x - 2, y - 1], tab[x, y]));
        }
        //verifica se pode mover para jogadorCima 2x e direita 1x
        if (x - 2 >= 0 && y + 1 < tamTabuleiro)
        {
            if (tab[x - 2, y + 1].PecaAtual == null || tab[x - 2, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) movimentos.Add(new Movimento(tab[x - 2, y + 1], tab[x, y]));
        }
        //verifica se pode mover para jogadorCima 1x e esquerda 2x
        if (x - 1 >= 0 && y - 2 >= 0)
        {
            if (tab[x - 1, y - 2].PecaAtual == null || tab[x - 1, y - 2].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) movimentos.Add(new Movimento(tab[x - 1, y - 2], tab[x, y]));
        }
        //verifica se pode mover para jogadorCima 1x e direita 2x
        if (x - 1 >= 0 && y + 2 < tamTabuleiro)
        {
            if (tab[x - 1, y + 2].PecaAtual == null || tab[x - 1, y + 2].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) movimentos.Add(new Movimento(tab[x - 1, y + 2], tab[x, y]));
        }
        //verifica se pode mover para baixo 2x e esquerda 1x
        if (x + 2 < tamTabuleiro && y - 1 >= 0)
        {
            if (tab[x + 2, y - 1].PecaAtual == null || tab[x + 2, y - 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) movimentos.Add(new Movimento(tab[x + 2, y - 1], tab[x, y]));
        }
        //verifica se pode mover para baixo 2x e direita 1x
        if (x + 2 < tamTabuleiro && y + 1 < tamTabuleiro)
        {
            if (tab[x + 2, y + 1].PecaAtual == null || tab[x + 2, y + 1].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) movimentos.Add(new Movimento(tab[x + 2, y + 1], tab[x, y]));
        }
        //verifica se pode mover para baixo 1x e esquerda 2x
        if (x + 1 < tamTabuleiro && y - 2 >= 0)
        {
            if (tab[x + 1, y - 2].PecaAtual == null || tab[x + 1, y - 2].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) movimentos.Add(new Movimento(tab[x + 1, y - 2], tab[x, y]));
        }
        //verifica se pode mover para baixo 1x e direita 2x
        if (x + 1 < tamTabuleiro && y + 2 < tamTabuleiro)
        {
            if (tab[x + 1, y + 2].PecaAtual == null || tab[x + 1, y +2].PecaAtual.jDono != tab[x, y].PecaAtual.jDono) movimentos.Add(new Movimento(tab[x + 1, y - 2], tab[x, y]));
        }

        return movimentos;
    }
    */

	public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem,bool verificaXeque=true)
	{
		var movimentos = new List<Movimento>();

		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -2, -1, passos: 1, verificaXeque:verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -2, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, -2, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, +2, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +2, -1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +2, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, -2, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, +2, passos: 1, verificaXeque: verificaXeque));

		return movimentos;
	}
}
