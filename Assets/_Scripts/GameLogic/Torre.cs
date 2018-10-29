using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : Peca
{

	public Torre(Jogador j) : base(j)
	{
	}

	    public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem, bool verificaXeque = true, bool verificaCaptura = false)
	{
		var movimentos = new List<Movimento>();

		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, 0, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, 0, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, +1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, -1, verificaXeque: verificaXeque));

		return movimentos;
	}

	public override void Roque(Tabuleiro tabuleiro,Torre torre = null)
	{
		
		//Rei rei = (Rei)this.jDono.conjuntoPecas[4];
		
		// USADO PARA TESTES
		 
		Rei rei = null;
		foreach(Peca p in this.jDono.conjuntoPecas)
		{
			if(p is Rei)
			{
				rei = (Rei)p;
				//Debug.Log("amem");
				break;
			}
			
		}
		
		// criar "movimento" rei deve ser aproximar da torre andando 2 casas (tanto no roque pequeno quanto no grande)
		// a torre anda 3 casas em casos de roque grande (torre do lado esquerdo)
		// a torre anda 2 casas em casos de roque pequeno(torre do lado direito)
		// dependendo da torre escolhida o movimento pode mudar
		// note que esse metodõ poderia ser chamado pelo rei ou torre(O JOGADOR PODE ESCOLHER FAZER ROQUE CLICANDO NO REI ou TORRE)
		// como a torre que varia eu implementei que a chamada do metodo do rei direciona para o metodo da torre especifica
		
		Casa destinorei,destinotorre;
		int linha = rei.CasaAtual.PosX;
		int colunaTgrande = this.PosY+3, colunaTpequeno = this.PosY-2;
		int colunaRgrande = rei.PosY-2, colunaRpequeno = rei.PosY+2;
	//	Console.WriteLine("select your roque");
		
		if(this.CasaAtual.PosY < rei.CasaAtual.PosY) // roque grande 
		{
	//			Console.WriteLine("coluna Roque grande torre: ");
		//		Console.WriteLine(colunaTgrande);
	//			Console.WriteLine("coluna Roque grande rei: ");
	//			Console.WriteLine(colunaRgrande);

				destinorei = tabuleiro.tabuleiro[linha, colunaRgrande];
				destinotorre = tabuleiro.tabuleiro[linha, colunaTgrande];
		}
		else // roque pequeno
		{
	//			Console.WriteLine("coluna Roque pequeno torre: ");
	//			Console.WriteLine(colunaTpequeno);
	//			Console.WriteLine("coluna Roque pequeno rei: ");
	//			Console.WriteLine(colunaRpequeno);

				destinorei = tabuleiro.tabuleiro[linha, colunaRpequeno];
				destinotorre = tabuleiro.tabuleiro[linha, colunaTpequeno];
		}
		Movimento m = new Movimento(origem: rei.CasaAtual, destino: destinorei );
		Movimento mt = new Movimento(origem: this.CasaAtual, destino: destinotorre); 
	//	Console.WriteLine(this.PodeRoque(this,rei,tabuleiro,m));
		if(this.PodeRoque(this,rei,tabuleiro,m))
		{
	//		Console.WriteLine("realizando roque...");
			this.RealizaMovimento(m);
			this.RealizaMovimento(mt);

		}	
		


	}

}
