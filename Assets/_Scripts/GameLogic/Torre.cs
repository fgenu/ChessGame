using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : Peca
{

	public Torre(Jogador j) : base(j)
	{
	}

	    public override List<Movimento> ListaMovimentos(bool verificaXeque = true, bool verificaCaptura = false)
	{
		var movimentos = new List<Movimento>();

		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, +1, 0, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, -1, 0, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 0, +1, verificaXeque: verificaXeque));
		movimentos.AddRange(Movimento.SeguindoDirecao(CasaAtual, 0, -1, verificaXeque: verificaXeque));

		return movimentos;
	}

	public override Movimento Roque(Tabuleiro tabuleiro,Torre torre = null)
	{
		
		//Rei rei = (Rei)this.jDono.conjuntoPecas[4];
		
		//OQUE ESTA COMENTADO FOI USADO PARA TESTES
		 
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
        if (rei.CasaAtual == null || this.CasaAtual==null)
        {
            return null;
        }
		// criar "movimento" rei deve ser aproximar da torre andando 2 casas (tanto no roque pequeno quanto no grande)
		// a torre anda 3 casas em casos de roque grande (torre do lado esquerdo)
		// a torre anda 2 casas em casos de roque pequeno(torre do lado direito)
		// dependendo da torre escolhida o movimento pode mudar
		// note que esse metodõ poderia ser chamado pelo rei ou torre(O JOGADOR PODE ESCOLHER FAZER ROQUE CLICANDO NO REI ou TORRE)
		// como a torre que varia eu implementei que a chamada do metodo do rei direciona para o metodo da torre especifica
		
		Casa destinorei,destinotorre;
		//MUDANÇA POR CAUSA DO TABULEIRO
		//int linha = rei.CasaAtual.PosX;
		//int colunaTgrande = this.PosY+3, colunaTpequeno = this.PosY-2;
		//int colunaRgrande = rei.PosY-2, colunaRpequeno = rei.PosY+2;
		int linha = rei.CasaAtual.PosY; // linha em que o rei está alinhado com a torre(que é na vdd a coluna da matriz)
		int colunaTgrande = this.PosX+3, colunaTpequeno = this.PosX-2; // coluna que o rei e torre devem se mover (que na vdd é a linha da matriz)
		int colunaRgrande = rei.PosX-2, colunaRpequeno = rei.PosX+2;
		//Debug.Log("select your roque");
/*
		Debug.Log("linha rei:");
		Debug.Log(linha);
		Debug.Log("Coluna rei:");
		Debug.Log(rei.CasaAtual.PosX);
		Debug.Log("Coluna torre:");
		Debug.Log(this.CasaAtual.PosX);
*/
		if(this.CasaAtual.PosX < rei.CasaAtual.PosX) // roque grande 
		{
			//	Debug.Log("Roque grande");
			/*	
				Debug.Log("coluna Roque grande torre: ");
				Debug.Log(colunaTgrande);
				Debug.Log("coluna Roque grande rei: ");
				Debug.Log(colunaRgrande);
			*/
				destinorei = tabuleiro.GetCasa(colunaRgrande,linha);
				destinotorre = tabuleiro.GetCasa(colunaTgrande,linha);
		}
		else // roque pequeno
		{
			//	Debug.Log("Roque pequeno");
			/*	
				Debug.Log("coluna Roque pequeno torre: ");
				Debug.Log(colunaTpequeno);
				Debug.Log("coluna Roque pequeno rei: ");
				Debug.Log(colunaRpequeno);
			*/
				destinorei = tabuleiro.GetCasa(colunaRpequeno,linha);
				destinotorre = tabuleiro.GetCasa(colunaTpequeno,linha);
		}
		Movimento m = new Movimento(origem: rei.CasaAtual, destino: destinorei );
		Movimento mt = new Movimento(origem: this.CasaAtual, destino: destinotorre); 
		//Debug.Log(this.PodeRoque(this,rei,tabuleiro,m));
		if(this.PodeRoque(this,rei,tabuleiro,m,mt))
		{
			//Debug.Log("realizando roque...");
			m.movimentoExtra = mt;
			//this.RealizaMovimento(m);
			//this.RealizaMovimento(mt);
			return m;

		}
		return null;	
		


	}

}
