using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : Peca {
    
    
    public Torre(bool jogadorCima, Jogador j): base(jogadorCima,j)
    {
    }

    // temp:
    /*var tabuleiro = FindObjectsOfType<Tabuleiro>();
    public override List<Movimento> listaMovimentos(Casa[,] tab, int x, int y)
    {
    
    }

    public override List<Movimento> GenuListaMovimentos (Tabuleiro tabuleiro, Casa origem)
    {
        var movimentos = new List<Movimento>();

        movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, 0));
        movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, 0));
        movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, +1));
        movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, -1));

        return movimentos;
    }*/
    
}
