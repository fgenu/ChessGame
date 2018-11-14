using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : Peca
{
	public Rei(Jogador j) : base(j)
	{

	}

	public override List<Movimento> ListaMovimentos(bool verificaXeque = true, bool verificaCaptura = false)
	{
		var movimentos = new List<Movimento>();

		// Como torre:
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, +1, 0, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, -1, 0, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 0, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 0, -1, passos: 1, verificaXeque: verificaXeque));

		// Como bispo:
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, +1, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, +1, -1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, -1, +1, passos: 1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, -1, -1, passos: 1, verificaXeque: verificaXeque));

		// roque: 
		Torre torre1 = null;
		Torre torre2 = null;
		int c = 0;
		// garante que listamos corretamente a(s) torres que estão no tabuleiro
		foreach(Peca p in this.jDono.conjuntoPecas)
		{
			if(p is Torre)
			{
				c++;
				if(c == 1)
				{
					torre1 = (Torre)p;
					//Debug.Log("amem2");
					
				}
				if(c == 2)
				{
					torre2 = (Torre)p;
					break;
				}
			}
			
		}
		Movimento possibilidade;
		if(torre1 != null)
		{
			possibilidade = this.Roque(this.CasaAtual.Tabuleiro,torre1);
			if(possibilidade != null)
			{
				movimentos.Add(possibilidade);
			}
			
			
		}
		if(torre2 != null)
		{
			possibilidade = this.Roque(this.CasaAtual.Tabuleiro,torre2);
			if(possibilidade != null)
			{
				movimentos.Add(possibilidade);
			}
			
		}
		
		return movimentos;
	}

	public override Movimento Roque(Tabuleiro tabuleiro, Torre escolhida)
	{
		
		return escolhida.Roque(tabuleiro); // obs: eu fiz isso para evitar redundancia nas funções, a diferença é que esse aqui considera uma torre especifica que chama a função
	}								// ou seja o jogador escolhe qual das torres ele pode usar para o roque

}
