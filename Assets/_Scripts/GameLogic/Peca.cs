using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca
{
    public Casa CasaAtual { get; private set; }

	public int PosX { get { return CasaAtual.PosX; } }
	public int PosY { get { return CasaAtual.PosY; } }
    public char Cor;
	public int PrimeiroTurnoMovido { get; private set; }
	public int UltimoTurnoMovido { get; private set; }
	public Movimento UltimoMovimento { get; private set; }
	public Jogador jDono;
    public UIPiece uiP;
	public bool primeiraJogada;

	public Peca(Jogador j)
	{
		CasaAtual = null;
		Cor = j.Cor;
		primeiraJogada = true;
		PrimeiroTurnoMovido = 0;
		UltimoTurnoMovido = 0;
		UltimoMovimento = null;
		jDono = j;
	}

    //só para o peao para verificar captura com rei
    

	// TODO: retirar "tabuleiro" do parâmetro, já que a casa tem uma referência a ele
    public virtual List<Movimento> ListaMovimentos(bool verificaXeque = true, bool verificaCaptura = false)
	{
		return null;
	}

	// realiza a movimentação baseado em um único movimento
	public void RealizaMovimento(Movimento m)
	{
		Partida partida = CasaAtual.Tabuleiro.partida;

		m = ValidarMovimento(m);

		if (m.pecaCapturada != null)
			CapturaPeca(m.pecaCapturada, partida);
		if (m.destino.PecaAtual != null)
			CapturaPeca(m.destino.PecaAtual, partida);

		m.destino.ColocarPeca(m.origem.PopPeca());

		if (primeiraJogada)
		{
			primeiraJogada = false;
			PrimeiroTurnoMovido = partida.TurnoAtual;
		}
		UltimoTurnoMovido = partida.TurnoAtual;
		UltimoMovimento = m;

		//verifica se é peao e se chegou ao fim do tabuleiro, se sim, muda o tipo de peça
		if ((this is Peao) && (this as Peao).PodePromover() && m.destino.PecaAtual.jDono == partida.Jogador1)// (m.destino.PosX == tamTabuleiro - 1))
		{
			CasaAtual.Tabuleiro.partida.UItab.ativaPromocao(m);
			//PromoverPeao(m);
		}

		//verifica se é peao e se chegou ao fim do tabuleiro, se sim, muda o tipo de peça
		if ((this is Peao) && (this as Peao).PodePromover() && m.destino.PecaAtual.jDono == partida.Jogador2)// (m.destino.PosX == tamTabuleiro - 1))
		{
			CasaAtual.Tabuleiro.partida.UItab.ativaPromocaoIA(m);

		}

		if (m.movimentoExtra != null)
			RealizaMovimento(m.movimentoExtra);
	}

	public static Movimento ValidarMovimento(Movimento m)
	{
		// Assegura que um movimento legal terá todas as suas propriedades corretas.
		foreach (Movimento daLista in m.origem.PecaAtual.ListaMovimentos())
		{
			if (daLista.origem == m.origem && daLista.destino == m.destino)
			{
				m = daLista;
				break;
			}
		}

		return m;
	}

	private static void CapturaPeca(Peca capturada, Partida partida)
	{
		Jogador jCapturado = capturada.jDono;
		int posPeca = 0;
		partida.TurnoDaUltimaCaptura = partida.TurnoAtual;

		//verifico todas as peças até achar a que eu quero
		foreach (Peca p in jCapturado.conjuntoPecas)
		{
			if (p == capturada)
			{
				break;
			}
			posPeca++;
		}
		jCapturado.conjuntoPecas.RemoveAt(posPeca);

		capturada.TirarDaCasaAtual();
	}

	public Peca PromoverPeao(Movimento m,int tipoNovaPeca)
	{
		int indicePeao = 0;
		Peca peaoAtual = m.destino.PecaAtual;

		//tipoNovaPeca = FUNÇÂOQUEUSAINTERFACEPARADEFINIR (TODO)

		// acha a posicao do peao no array de peças do jogador
		foreach (Peca p in peaoAtual.jDono.conjuntoPecas)
		{
			if (p == peaoAtual)
			{
				break;
			}
			indicePeao++;
		}

		Peca novaPeca;
		//se 1, então vira rainha
		//se 2, então vira torre
		//se 3, então vira cavalo
		//senao, então vira Bispo       
		if (tipoNovaPeca == 1)
		{
			novaPeca = new Rainha(peaoAtual.jDono);
		}
		else if (tipoNovaPeca == 2)
		{
			novaPeca = new Torre(peaoAtual.jDono);
		}
		else if (tipoNovaPeca == 3)
		{
			novaPeca = new Cavalo(peaoAtual.jDono);
		}
		else
		{
			novaPeca = new Bispo(peaoAtual.jDono);
		}

		//define a posição e salva a peça na casa e no jogador
		novaPeca.jDono.conjuntoPecas[indicePeao] = novaPeca;
		m.destino.ColocarPeca(novaPeca);
        novaPeca.CasaAtual = m.destino;

		novaPeca.UltimoTurnoMovido = this.UltimoTurnoMovido;
        

        return novaPeca;
	}

	// Quando uma casa colocar esta peça como atual,
	// esta precisa colocar tal casa como atual.
	public void ValidarNovaCasa(Casa casa)
	{
		if (casa.PecaAtual == this)
			CasaAtual = casa;
	}

	public void TirarDaCasaAtual()
	{
		if (CasaAtual == null)
			return;

		if (CasaAtual.PecaAtual == this)
			CasaAtual.PopPeca();

		CasaAtual = null;
	}

	/* 
	public Casa GetCasaAtual(Tabuleiro tabuleiro)
	{
		if (tabuleiro == null)
		{
			Debug.LogError("Tabuleiro passado é nulo.");
			return null;
		}

		Casa casaAtual = tabuleiro.GetCasa(this.PosX, this.PosY);

		if (casaAtual == null) return null;

		if (casaAtual.PecaAtual != this)
		{
			Debug.LogError("A peça examinada e a peça na casa representada pelo seu X e Y neste tabuleiro são diferentes!");
			return null;
		}
		
		return casaAtual;
	}
	*/

	// Genú: Por enquanto, a função abaixo está bem basicona. Será que,
	// além das regras do xeque, que já foram programadas, 
	// há alguma outra verificação?
	public bool PodeCapturar(Peca alvo)
	{
		if (this.Cor == alvo.Cor)
			return false;
		if (this.Cor != alvo.Cor)
			return true;

		return false;
	}

	public bool PodePercorrer(Movimento movimento, Tabuleiro tabuleiro)
	{
		if (CasaAtual != movimento.origem)
			return false;

		List<Movimento> possibilidades = ListaMovimentos();

		foreach (var possibilidade in possibilidades)
			if (possibilidade.destino == movimento.destino)
				return true;

		return false;
	}



	protected bool PodeRoque(Torre torre, Rei rei,Tabuleiro tabuleiro,Movimento movrei,Movimento movtorre)
	{
		if (torre == null || rei == null || movrei == null || movtorre == null)
			return false;
		// lembrando que as condições de roque são:
		
	


		// rei não pode ter se movimentado nenhuma vez
		// torre não pode ter se movimentado nenhuma vez
		if(!torre.primeiraJogada || !rei.primeiraJogada)
		{
		//	Debug.Log("NÃO É A PRIMEIRA JOGADA!");
			return false;

		}


		// nao pode haver peças entre o rei e a torre
		int linha = rei.CasaAtual.PosY;
//		Debug.Log("linha rei:");
//		Debug.Log(linha);
		int torrepos = torre.PosX;
		int reipos = rei.PosX;
//		Debug.Log("Coluna torre:");
//		Debug.Log(torrepos);
//		Debug.Log("Coluna rei:");
//		Debug.Log(reipos);
		int i,f;
		if(torrepos < reipos)
		{
		//	Debug.Log("a posição entre torre e rei caracteriza um roque maior(ou era para caracterizar)");
			i = torrepos;
			f = reipos;
			
		}
		else
		{
		//	Debug.Log("a posição entre torre e rei caracteriza um roque menor(ou era para caracterizar)");
			i = reipos;
			f = torrepos;
		}
		for(int p=i+1; p < f ;p++)
		{
			if(tabuleiro.GetCasa(p,linha).EstaOcupada())
			{
				//Debug.Log(p);
		//		Debug.Log("TEM CASAS OCUPADAS NO CAMINHO!");
				return false;
			}
		}
		
		// rei nao pode estar em xeque
		if(rei.jDono.EmXeque(false))
		{
		//	Debug.Log("Rei está em xeque!");
			return false;
		}
		
		// rei não pode passar nem terminar em uma casa que está sendo atacada por peça adversaria(rei entraria em xeque)
		//dependendo de quem se mova primeiro (torre ou rei ) antes de chamar a função CausaAutoXeque() sempre teremos um rei em xeque
		// mesmo se a torre o proteger(bloquear o ataque)
		// então o movimento da torre será "simulado"
		Peca movida;
		movida = movtorre.origem.PopPeca();
		// lembrando que se chegamos até aqui não há ninguem ocupando essa casa! (eu acho...), podemos colocar a peça sem receio
		movtorre.destino.ColocarPeca(movida);
		if(movrei.CausaAutoXeque())
		{
			
		//	Debug.Log("rei esta indo para casa sob ataque!(entraria em xeque)");
			// voltar para a torre para a poisção original 
			movtorre.destino.PopPeca();
			movtorre.origem.ColocarPeca(movida);
			return false;
		}
		// voltar para a torre para a poisção original
		movida = movtorre.destino.PopPeca();
		movtorre.origem.ColocarPeca(movida);


		





		return true;


	}





	
	public virtual Movimento Roque(Tabuleiro tabuleiro, Torre torre = null)
	{
		return null;
	}

	public string ListaMovimentosToString()
	{
		string str = "";

		foreach (var movimento in ListaMovimentos())
			str += "[" + movimento.destino.PosX + "," + movimento.destino.PosY + "]";

		return str;
	}
}
