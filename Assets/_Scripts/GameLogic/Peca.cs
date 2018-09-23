using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca
{
	public int posX = -1, posY = -1,tamTabuleiro=8;
	public bool jogadorCima; 
	public char cor;
	public Jogador jDono;

	public Peca(bool cima, Jogador j)
	{
		cor = j.Cor;
		jogadorCima = cima;
		jDono = j;
	}

	//declara funcao de listar movimentos
	// Genú: O comentário ajogadorCima é desnecessário, porque lendo rapidamente a declaração da função eu entendo 
	// que ela deve listar movimentos. Pelo tipo e pelo nome. É bom comentar código, mas é melhor ainda se ele pode
	// ser compreendido facilmente sem que seja necessário comentar. Veteranos comentam pouco e tem vários autores
	// que falam isso; depois eu posso te passar alguma referência de texto.
	public virtual List<Movimento> ListaMovimentos(Casa[,] tab, int x, int y)
	{
		return null;
	}

    public virtual void defineInimigo(Jogador i)
    {
        
    }


    // Genú: O nome da função abaixo é placeholder, acho melhor trocar o "listaMovimentos" por ela. Receber uma instância
    // de tabuleiro dá muito mais flexibilidade, como usar métodos sobre ele.

    //public virtual List<Movimento> GenuListaMovimentos(Tabuleiro tabuleiro, Casa origem);

	//recebe o tabuleiro e realiza a movimentação baseado em um único movimento
	public void RealizaMovimento(Movimento m) // Genú: Acho que "Mover" é um nome melhor e mais conciso. Também é sinônimo de "realizar movimento"
	{
		//verifica se tem captura de peça
		if (m.destino.pecaAtual != null)
		{
            Jogador jCapturado = m.destino.pecaAtual.jDono;
            int posPeca=0;

            //verifico todas as peças até achar a que eu quero
            foreach(Peca p in jCapturado.conjuntoPecas)
            {
                if (p == m.destino.pecaAtual)
                {
                    break;
                }
                posPeca++;
            }
            jCapturado.conjuntoPecas.RemoveAt(posPeca);

		}
		//realiza o movimento

		m.destino.pecaAtual = m.origem.pecaAtual;
		m.destino.pecaAtual.posX = m.destino.posX;
		m.destino.pecaAtual.posY = m.destino.posY;
		m.origem.pecaAtual = null;

        //verifica se é peao e se chegou ao fim do tabuleiro, se sim, muda o tipo de peça
        if((this is Peao) && (m.destino.posX==tamTabuleiro-1))
        {
            int tipoNovaPeca = 0, posPeao=0;
            Peca peaoAtual = m.destino.pecaAtual;

            //tipoNovaPeca = FUNÇÂOQUEUSAINTERFACEPARADEFINIR

            //acha a posicao do peao no array de peças do jogador
            foreach(Peca p in peaoAtual.jDono.conjuntoPecas)
            {
                if (p == peaoAtual)
                {
                    break;
                }
                posPeao++;
            }

            Peca novaPeca;
            //se 0, então vira rainha
            //se 1, então vira torre
            //se 3, então vira cavalo
            //senao, então vira Bispo       
            if (tipoNovaPeca == 0)
            {
                novaPeca = new Rainha(peaoAtual.jogadorCima,peaoAtual.jDono);
            }     
            else if (tipoNovaPeca == 1)
            {
                novaPeca = new Torre(peaoAtual.jogadorCima, peaoAtual.jDono);
            }
            else if (tipoNovaPeca == 2)
            {
                novaPeca = new Cavalo(peaoAtual.jogadorCima, peaoAtual.jDono);
            }
            else
            {
                novaPeca = new Bispo(peaoAtual.jogadorCima, peaoAtual.jDono);
            }
            //define a posição e salva a peça na casa e no jogador
            novaPeca.SetPosition(peaoAtual.posX, peaoAtual.posY);
            m.destino.pecaAtual = novaPeca;
            novaPeca.jDono.conjuntoPecas[posPeao] = novaPeca;
        }
	}

	// Genú: sorry, eu escrevi isso mas não gostei, acho que deve haver um jeito melhor de fazer isso
	public void SetPosition(int x, int y)
	{
		posX = x;
		posY = y;
	}

	// Genú: Por enquanto, a função abaixo está bem basicona, 
	// mas regras especiais que impedem uma peça de capturar outra se encaixariam aqui. 
	// Por exemplo, uma peça não pode capturar outra se o seu movimento deixaria o 
	// próprio rei em cheque. (TODO)
	/*public bool PodeCapturar(Peca alvo)
	{
		if (this.cor == alvo.cor)
			return false;

		if (this.cor != alvo.cor)
			return true;
	}*/
}
