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
    public double[,] peaoBranco = new double[8,8] {
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
        {5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0},
        {1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0},
        {0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5},
        {0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0},
        {0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5},
        {0.5,  1.0, 1.0,  -2.0, -2.0,  1.0,  1.0,  0.5},
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0}};

    public double[,] peaoPreto = new double[8, 8] {
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
        {0.5,  1.0, 1.0,  -2.0, -2.0,  1.0,  1.0,  0.5},
        {0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5},
        {0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0},
        {0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5},
        {1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0},
        {5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0},
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
    };

    public double[,] cavalo = new double[8,8] {
        {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0},
        {-4.0, -2.0,  0.0,  0.0,  0.0,  0.0, -2.0, -4.0},
        {-3.0,  0.0,  1.0,  1.5,  1.5,  1.0,  0.0, -3.0},
        {-3.0,  0.5,  1.5,  2.0,  2.0,  1.5,  0.5, -3.0},
        {-3.0,  0.0,  1.5,  2.0,  2.0,  1.5,  0.0, -3.0},
        {-3.0,  0.5,  1.0,  1.5,  1.5,  1.0,  0.5, -3.0},
        {-4.0, -2.0,  0.0,  0.5,  0.5,  0.0, -2.0, -4.0},
        {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0}};

    public double[,] bispoBranco = new double[8,8] {
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0},
        { -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
        { -1.0,  0.0,  0.5,  1.0,  1.0,  0.5,  0.0, -1.0},
        { -1.0,  0.5,  0.5,  1.0,  1.0,  0.5,  0.5, -1.0},
        { -1.0,  0.0,  1.0,  1.0,  1.0,  1.0,  0.0, -1.0},
        { -1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0, -1.0},
        { -1.0,  0.5,  0.0,  0.0,  0.0,  0.0,  0.5, -1.0},
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0}};

    public double[,] bispoPreto = new double[8, 8] {
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0},
        { -1.0,  0.5,  0.0,  0.0,  0.0,  0.0,  0.5, -1.0},
        { -1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0, -1.0},
        { -1.0,  0.0,  1.0,  1.0,  1.0,  1.0,  0.0, -1.0},
        { -1.0,  0.5,  0.5,  1.0,  1.0,  0.5,  0.5, -1.0},
        { -1.0,  0.0,  0.5,  1.0,  1.0,  0.5,  0.0, -1.0},
        { -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0},
    };

    public double[,] torreBranco = new double[8,8] {
        {  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
        {  0.5,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        {  0.0,   0.0, 0.0,  0.5,  0.5,  0.0,  0.0,  0.0}};

    public double[,] torrePreto = new double[8, 8] {
        {  0.0,   0.0, 0.0,  0.5,  0.5,  0.0,  0.0,  0.0},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
        {  0.5,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  0.5},
        {  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
    };

    public double[,] rainha = new double[8,8] {
        { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0},
        { -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
        { -1.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -1.0},
        { -0.5,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -0.5},
        {  0.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -0.5},
        { -1.0,  0.5,  0.5,  0.5,  0.5,  0.5,  0.0, -1.0},
        { -1.0,  0.0,  0.5,  0.0,  0.0,  0.0,  0.0, -1.0},
        { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0}};
    
    public double[,] reiBranco = new double[8,8] {
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0},
        { -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0},
        {  2.0,  2.0,  0.0,  0.0,  0.0,  0.0,  2.0,  2.0 },
        {  2.0,  3.0,  1.0,  0.0,  0.0,  1.0,  3.0,  2.0 }};

    public double[,] reiPreto = new double[8, 8] {
        {  2.0,  3.0,  1.0,  0.0,  0.0,  1.0,  3.0,  2.0 },
        {  2.0,  2.0,  0.0,  0.0,  0.0,  0.0,  2.0,  2.0 },
        { -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0},
        { -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
    };

    public char Cor { get; private set; }

    public IA(Jogador j){
        Cor = j.Cor;
    }

    public double getPieceValue(Peca peca, Casa destino){
        if(destino.PecaAtual == null){
            if(peca is Peao){
                if(peca.Cor == 'b'){
                    return peaoBranco[destino.PosX, destino.PosY];
                }
                else{
                    return peaoPreto[destino.PosX, destino.PosY];
                }
            }
            else if(peca is Rainha){
                return rainha[destino.PosX, destino.PosY];
            }
            else if(peca is Torre){
                if(peca.Cor == 'b'){
                    return torreBranco[destino.PosX, destino.PosY];
                }
                else{
                    return torrePreto[destino.PosX, destino.PosY];
                }
            }
            else if(peca is Bispo){
                if(peca.Cor == 'b'){
                    return bispoBranco[destino.PosX, destino.PosY];
                }
                else{
                    return bispoPreto[destino.PosX, destino.PosY];
                }
            }
            else if(peca is Rei){
                if(peca.Cor == 'b'){
                    return reiBranco[destino.PosX, destino.PosY];
                }
                else{
                    return reiPreto[destino.PosX, destino.PosY];
                }
            }
            if(peca is Cavalo){
                return cavalo[destino.PosX, destino.PosY];
            }
            return 0;
        }
        else {
            if(destino.PecaAtual is Peao){
               if(peca.Cor == 'b'){
                    return 10 + peaoBranco[destino.PosX, destino.PosY];
                }
                else{
                    return 10 + peaoPreto[destino.PosX, destino.PosY];
                } 
            }
            else if(destino.PecaAtual is Torre){
               if(peca.Cor == 'b'){
                    return 50 + torreBranco[destino.PosX, destino.PosY];
                }
                else{
                    return 50 + torrePreto[destino.PosX, destino.PosY];
                } 
            }
            else if(destino.PecaAtual is Bispo){
               if(peca.Cor == 'b'){
                    return 30 + bispoBranco[destino.PosX, destino.PosY];
                }
                else{
                    return 30 + bispoPreto[destino.PosX, destino.PosY];
                } 
            }
            else if(destino.PecaAtual is Rei){
               if(peca.Cor == 'b'){
                    return 900 + reiBranco[destino.PosX, destino.PosY];
                }
                else{
                    return 900 + reiPreto[destino.PosX, destino.PosY];
                } 
            }
            else if(destino.PecaAtual is Cavalo){
                return 30 + cavalo[destino.PosX, destino.PosY];
            }
            else if(destino.PecaAtual is Rainha){
                return 90 + rainha[destino.PosX, destino.PosY];
            }
            return 0;
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
                if(atual.PecaAtual!=null && atual.PecaAtual.Cor == this.Cor){
                    List<Movimento> possibilidades = atual.PecaAtual.ListaMovimentos();
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

    public Peca RealizaMovimentoIA(Movimento m)
    {
        Peca pCapt = null;
        //verifica se tem captura de pe�a
        if (m.destino.PecaAtual != null)
        {
            Jogador jCapturado = m.destino.PecaAtual.jDono;
            int posPeca = 0;

            //verifico todas as pe�as at� achar a que eu quero
            foreach (Peca p in jCapturado.conjuntoPecas)
            {
                if (p == m.destino.PecaAtual)
                {
                    pCapt = p;
                    break;
                }
                posPeca++;
            }
            jCapturado.conjuntoPecas.RemoveAt(posPeca);

        }
        m.destino.ColocarPeca(m.origem.PopPeca());


        //verifica se � peao e se chegou ao fim do tabuleiro, se sim, muda o tipo de pe�a
        /*if ((m.destino.PecaAtual is Peao) && (m.destino.PecaAtual as Peao).PodePromover())// (m.destino.PosX == tamTabuleiro - 1))
        {
            //CasaAtual.Tabuleiro.partida.UItab.ativaPromocao(m);
            m.destino.PecaAtual.PromoverPeao(m,1);
        }*/
        return pCapt;
    }
    public void desfazMovimentoIA(Movimento m,Peca pCapt)
    {
        m.origem.ColocarPeca(m.destino.PopPeca());
        if (pCapt != null)
        {
            m.destino.ColocarPeca(pCapt);
            pCapt.jDono.conjuntoPecas.Add(pCapt);

        }


        //verifica se � peao e se chegou ao fim do tabuleiro, se sim, muda o tipo de pe�a
        /*if ((m.destino.PecaAtual is Peao) && (m.destino.PecaAtual as Peao).PodePromover())// (m.destino.PosX == tamTabuleiro - 1))
        {
            //CasaAtual.Tabuleiro.partida.UItab.ativaPromocao(m);
            m.destino.PecaAtual.PromoverPeao(m, 1);
        }*/
    }
    public Jogada minmax(int profundidade,double alfa, double beta, bool max,Tabuleiro tab)
    {

        if (profundidade == 1)
        {
            return melhorJogada(tab);
        }
        if (max)
        {
            Jogada pontuacaoAt = new Jogada(-100000000, null);
            Jogada pontuacaoTemp;
            Peca capt;
            List<Movimento> movimentosPossiveis = listaMovimentosTotal(tab.partida.Jogador2, tab);
            foreach (Movimento m in movimentosPossiveis)
            {

                capt = RealizaMovimentoIA(m);
                pontuacaoTemp = minmax(profundidade - 1,alfa,beta, !max,tab);
                if (pontuacaoTemp.valor >= pontuacaoAt.valor)
                {
                    pontuacaoAt.valor = pontuacaoTemp.valor;
                    pontuacaoAt.movimento = m;
                }
                desfazMovimentoIA(m,capt);

                if (alfa<pontuacaoAt.valor) {
                    alfa = pontuacaoAt.valor;
                }
                if (alfa >= beta) {
                    break;
                }

            }
            return pontuacaoAt;
        }
        else
        {
            Jogada pontuacaoAt = new Jogada(100000000, null);
            Jogada pontuacaoTemp;
            Peca capt;
            List<Movimento> movimentosPossiveis = listaMovimentosTotal(tab.partida.Jogador1, tab);
            foreach (Movimento m in movimentosPossiveis)
            {
                capt = RealizaMovimentoIA(m);
                pontuacaoTemp = minmax(profundidade - 1,alfa,beta, !max, tab);
                if (pontuacaoTemp.valor <= pontuacaoAt.valor)
                {
                    pontuacaoAt.valor = pontuacaoTemp.valor;
                    pontuacaoAt.movimento = m;
                }
                desfazMovimentoIA(m,capt);


                if (beta > pontuacaoAt.valor)
                {
                    beta = pontuacaoAt.valor;
                }
                if (alfa >= beta)
                {
                    break;
                }

            }
            
            return pontuacaoAt;
        }
    }

    //lista todos os movimentos poss�veis de um jogador
    private List<Movimento> listaMovimentosTotal(Jogador j,Tabuleiro tab) {
        List<Movimento> listaMovs = new List<Movimento>();
        foreach (Peca p in j.conjuntoPecas){
            listaMovs.AddRange(p.ListaMovimentos());
        }
        return listaMovs;

    }
}
