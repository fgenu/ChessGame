using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogada{
    public Double valor;
    public Movimento movimento;
    public Jogada(Double val, Movimento mov) {
        valor = val;
        movimento = mov;
    }
}

public class IA
{
    private double[,] peaoBranco = new double[8,8] {
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
        {5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0},
        {1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0},
        {0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5},
        {0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0},
        {0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5},
        {0.5,  1.0, 1.0,  -2.0, -2.0,  1.0,  1.0,  0.5},
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0}};
    
    private double[,] peaoPreto = peaoBranco.Reverse();

    private double[,] cavalo = new double[8,8] {
        {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0},
        {-4.0, -2.0,  0.0,  0.0,  0.0,  0.0, -2.0, -4.0},
        {-3.0,  0.0,  1.0,  1.5,  1.5,  1.0,  0.0, -3.0},
        {-3.0,  0.5,  1.5,  2.0,  2.0,  1.5,  0.5, -3.0},
        {-3.0,  0.0,  1.5,  2.0,  2.0,  1.5,  0.0, -3.0},
        {-3.0,  0.5,  1.0,  1.5,  1.5,  1.0,  0.5, -3.0},
        {-4.0, -2.0,  0.0,  0.5,  0.5,  0.0, -2.0, -4.0},
        {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0}};

    private double[,] bispoBranco = new double[8,8] {
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0},
        { -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
        { -1.0,  0.0,  0.5,  1.0,  1.0,  0.5,  0.0, -1.0},
        { -1.0,  0.5,  0.5,  1.0,  1.0,  0.5,  0.5, -1.0},
        { -1.0,  0.0,  1.0,  1.0,  1.0,  1.0,  0.0, -1.0},
        { -1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0, -1.0},
        { -1.0,  0.5,  0.0,  0.0,  0.0,  0.0,  0.5, -1.0},
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0}};

    private double[,] bispoPreto = bispoBranco.Reverse();

    private double[,] torreBranco = new double[8,8] {
        {  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
        {  0.5,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        {  0.0,   0.0, 0.0,  0.5,  0.5,  0.0,  0.0,  0.0}};

    private double[,] torrePreto = torreBranco.Reverse();

    private double[,] rainha = new double[8,8] {
        { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0},
        { -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
        { -1.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -1.0},
        { -0.5,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -0.5},
        {  0.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -0.5},
        { -1.0,  0.5,  0.5,  0.5,  0.5,  0.5,  0.0, -1.0},
        { -1.0,  0.0,  0.5,  0.0,  0.0,  0.0,  0.0, -1.0},
        { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0}};
    
    private double[,] reiBranco = new double[8,8] {
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0},
        { -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0},
        {  2.0,  2.0,  0.0,  0.0,  0.0,  0.0,  2.0,  2.0 },
        {  2.0,  3.0,  1.0,  0.0,  0.0,  1.0,  3.0,  2.0 }};

    private double[,] reiPreto = reiBranco.Reverse();

    public char Cor { get; private set; }

    public IA(Jogador j){
        Cor = j.cor;
    }

    public double getPieceValue(Peca peca, Casa destino){
        if(destino.PecaAtual == null){
            if(peca.GetType() == "Peao"){
                if(peca.cor == "b"){
                    return peaoBranco[destino.PosX, destino.PosY];
                }
                else{
                    return peaoPreto[destino.PosX, destino.PosY];
                }
            }
            else if(peca.GetType() == "Rainha"){
                return rainha[destino.PosX, destino.PosY];
            }
            else if(peca.GetType() == "Torre"){
                if(peca.cor == "b"){
                    return torreBranco[destino.PosX, destino.PosY];
                }
                else{
                    return torrePreto[destino.PosX, destino.PosY];
                }
            }
            else if(peca.GetType() == "Bispo"){
                if(peca.cor == "b"){
                    return bispoBranco[destino.PosX, destino.PosY];
                }
                else{
                    return bispoPreto[destino.PosX, destino.PosY];
                }
            }
            else if(peca.GetType() == "Rei"){
                if(peca.cor == "b"){
                    return reiBranco[destino.PosX, destino.PosY];
                }
                else{
                    return reiPreto[destino.PosX, destino.PosY];
                }
            }
            if(peca.GetType() == "Cavalo"){
                return cavalo[destino.PosX, destino.PosY];
            }
        }
        else {
            if(destino.PecaAtual.GetType() == "Peao"){
               if(peca.cor == "b"){
                    return 10 + peaoBranco[destino.PosX, destino.PosY];
                }
                else{
                    return 10 + peaoPreto[destino.PosX, destino.PosY];
                } 
            }
            else if(destino.PecaAtual.GetType() == "Torre"){
               if(peca.cor == "b"){
                    return 50 + torreBranco[destino.PosX, destino.PosY];
                }
                else{
                    return 50 + torrePreto[destino.PosX, destino.PosY];
                } 
            }
            else if(destino.PecaAtual.GetType() == "Bispo"){
               if(peca.cor == "b"){
                    return 30 + bispoBranco[destino.PosX, destino.PosY];
                }
                else{
                    return 30 + bispoPreto[destino.PosX, destino.PosY];
                } 
            }
            else if(destino.PecaAtual.GetType() == "Rei"){
               if(peca.cor == "b"){
                    return 900 + reiBranco[destino.PosX, destino.PosY];
                }
                else{
                    return 900 + reiPreto[destino.PosX, destino.PosY];
                } 
            }
            else if(destino.PecaAtual.GetType() == "Cavalo"){
                return 30 + cavalo[destino.PosX, destino.PosY];
            }
            else if(destino.PecaAtual.GetType() == "Rainha"){
                return 90 + rainha[destino.PosX, destino.PosY];
            }
        }
    }

    public Jogada melhorJogada(Tabuleiro tab){
        Casa atual;
        double melhor = Double.MinValue;
        int melhorX;
        int melhorY;
        double jogadaAtual;
        Movimento melhorMov=null;
        for(int i=0; i<8; i++){
            for(int j=0; j<8; j++){
                atual = tab.GetCasa(i,j);
                if(atual.PecaAtual.Cor == this.Cor){
                    List<Movimento> possibilidades = ListaMovimentos(tab, atual);
                    foreach (var possibilidade in possibilidades){
                        if (atual.PecaAtual.PodePercorrer(possibilidade, tab)){
                           jogadaAtual = getPieceValue(atual.PecaAtual, possibilidade.destino);
                           if (jogadaAtual > melhor){
                               melhor = jogadaAtual;
                               melhorMov = possibilidade;
                               melhorX = i;
                               melhorY = j;
                           }
                        }
                    }
                }
            }
        }
        return new Jogada(melhor, melhorMov);
        //return tab.GetCasa(i,j);
    }

    public Jogada minmax(int profundidade, bool max, Tabuleiro tab)
    {

        if (profundidade == 1)
        {
            return melhorJogada(tab);
        }
        if (max)
        {
            Jogada pontuacaoAt = new Jogada(-100000000, null);
            Jogada pontuacaoTemp;
            List<Movimento> movimentosPossiveis = listaMovimentosTotal(j2);
            foreach (Movimento m in movimentosPossiveis)
            {

                Tabuleiro tabTemp = novoTabuleiro(tab, m);
                pontuacaoTemp = minmax(profundidade - 1, !max, tabTemp);
                if (pontuacaoTemp.valor > pontuacaoAt.valor)
                {
                    pontuacaoAt = pontuacaoTemp;
                }
            }
            return pontuacaoAt;
        }
        else
        {
            Jogada pontuacaoAt = new Jogada(100000000, null);
            Jogada pontuacaoTemp;
            List<Movimento> movimentosPossiveis = listaMovimentosTotal(j2);
            foreach (Movimento m in movimentosPossiveis)
            {

                Tabuleiro tabTemp = novoTabuleiro(tab, m);
                pontuacaoTemp = minmax(profundidade - 1, !max, tabTemp);
                if (pontuacaoTemp.valor < pontuacaoAt.valor)
                {
                    pontuacaoAt = pontuacaoTemp;
                }
            }
            return pontuacaoAt;
        }
    }

    //lista todos os movimentos possíveis de um jogador
    private List<Movimento> listaMovimentosTotal(Jogador j,Tabuleiro tab) {
        List<Movimento> listaMovs = new List<Movimento>();
        foreach (Peca p in j.conjuntoPecas){
            listaMovs.AddRange(p.ListaMovimentos(tab,p.CasaAtual));
        }

    }
}
