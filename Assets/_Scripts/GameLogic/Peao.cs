using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peao : Peca
{
	public Peao(Jogador j) : base(j)
	{

	}

	public override List<Movimento> ListaMovimentos(bool verificaXeque=true,bool verificaCaptura=false)
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

            movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 1, 1 * mod, passos: 1, tipo: Movimento.Tipo.SemCaptura, verificaXeque: verificaXeque));
            movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, -1, 1 * mod, passos: 1, tipo: Movimento.Tipo.SemCaptura, verificaXeque: verificaXeque));
        }
        else
        {
            // Movimentos sem captura
            if (primeiraJogada)
                movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 0, 1 * mod, passos: 2, tipo: Movimento.Tipo.SemCaptura, verificaXeque: verificaXeque));
            else
                movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 0, 1 * mod, passos: 1, tipo: Movimento.Tipo.SemCaptura, verificaXeque: verificaXeque));

        }


        // Movimentos com captura
        movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 1, 1 * mod, passos: 1, tipo: Movimento.Tipo.SomenteCaptura, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, -1, 1 * mod, passos: 1, tipo: Movimento.Tipo.SomenteCaptura, verificaXeque: verificaXeque));

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
