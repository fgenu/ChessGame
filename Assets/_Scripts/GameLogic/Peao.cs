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

		// En Passant
		var tabuleiro = CasaAtual.Tabuleiro;
		Casa casaEsquerda = tabuleiro.GetCasa(PosX - 1, PosY);
		Casa casaDireita = tabuleiro.GetCasa(PosX + 1, PosY);

		foreach (var casa in new List<Casa> { casaEsquerda, casaDireita })
		{
			if (casa != null)
			{
				Peca alvo = casa.PecaAtual;
				if ((alvo != null) && (PodeCapturar(alvo)) && (alvo is Peao) && (alvo.PrimeiroTurnoMovido == tabuleiro.TurnoAtual - 1))
				{
					// Se o alvo andou mais de uma casa...
					if (alvo.PosY + 1 * mod != alvo.UltimoMovimento.origem.PosY)
					{
						Casa destino = tabuleiro.GetCasa(alvo.PosX, alvo.PosY + 1 * mod);
						Movimento enPassant = new Movimento(origem: CasaAtual, destino: destino, tipo: Movimento.Tipo.Normal);
						enPassant.pecaCapturada = alvo;
						movimentos.Add(enPassant);
					}
				}
			}
		}

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
