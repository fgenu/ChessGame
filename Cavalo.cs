using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalo : Peca
{
    int tamTabuleiro = 8;
    bool primeiraJogada = true;
    public Cavalo(char c,bool cima, Jogador j) : base(c, cima, j)
    {

    }


    public override List<Movimento> listaMovimentos(Casa[,] tab, int x, int y)
    {
        List<Movimento> movimentos = new List<Movimento>();
        int quantMovimentos;

        //verifica cada lugar que o cavalo pode ir
        //verifica se pode mover para cima 2x e esquerda 1x
        if (x - 2 >= 0 && y-1>=0)
        {
            movimentos.Add(new Movimento(x - 2, y-1, tab[x - 2, y-1], tab[x, y]));
        }
        //verifica se pode mover para cima 2x e direita 1x
        if (x - 2 >= 0 && y + 1 <tamTabuleiro)
        {
            movimentos.Add(new Movimento(x - 2, y + 1, tab[x - 2, y + 1], tab[x, y]));
        }
        //verifica se pode mover para cima 1x e esquerda 2x
        if (x - 1 >= 0 && y - 2 >= 0)
        {
            movimentos.Add(new Movimento(x - 1, y - 2, tab[x - 1, y - 2], tab[x, y]));
        }
        //verifica se pode mover para cima 1x e direita 2x
        if (x - 1 >= 0 && y + 2 < tamTabuleiro)
        {
            movimentos.Add(new Movimento(x - 1, y + 2, tab[x - 1, y + 2], tab[x, y]));
        }
        //verifica se pode mover para baixo 2x e esquerda 1x
        if (x + 2 >= 0 && y - 1 >= 0)
        {
            movimentos.Add(new Movimento(x + 2, y - 1, tab[x + 2, y - 1], tab[x, y]));
        }
        //verifica se pode mover para baixo 2x e direita 1x
        if (x + 2 >= 0 && y + 1<tamTabuleiro)
        {
            movimentos.Add(new Movimento(x + 2, y + 1, tab[x + 2, y + 1], tab[x, y]));
        }
        //verifica se pode mover para baixo 1x e esquerda 2x
        if (x + 1 >= 0 && y - 2 >= 0)
        {
            movimentos.Add(new Movimento(x + 1, y - 2, tab[x + 1, y - 2], tab[x, y]));
        }
        //verifica se pode mover para baixo 1x e direita 2x
        if (x + 1 >= 0 && y + 2 < tamTabuleiro)
        {
            movimentos.Add(new Movimento(x + 1, y - 2, tab[x + 1, y - 2], tab[x, y]));
        }

        return movimentos;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
