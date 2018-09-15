using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca
{
	public int posX = -1, posY = -1;
	public bool JogadorBaixo; // Genú: Era essa a variável que era melhor reescrever, né?
	public char cor;
	Jogador jDono;

	public Peca(char c, bool cima, Jogador j)
	{
		cor = c;
		JogadorBaixo = cima; // Genú: demorei bastante pra entender o que o "cima" significa...
		jDono = j;
	}

	//declara funcao de listar movimentos
	// Genú: O comentário acima é desnecessário, porque lendo rapidamente a declaração da função eu entendo 
	// que ela deve listar movimentos. Pelo tipo e pelo nome. É bom comentar código, mas é melhor ainda se ele pode
	// ser compreendido facilmente sem que seja necessário comentar. Veteranos comentam pouco e tem vários autores
	// que falam isso; depois eu posso te passar alguma referência de texto.
	public virtual List<Movimento> ListaMovimentos(Casa[,] tab, int x, int y)
	{
		return null;
	}

	// Genú: O nome da função abaixo é placeholder, acho melhor trocar o "listaMovimentos" por ela. Receber uma instância
	// de tabuleiro dá muito mais flexibilidade, como usar métodos sobre ele.
	public virtual List<Movimento> GenuListaMovimentos(Tabuleiro tabuleiro, Casa origem);

	//recebe o tabuleiro e realiza a movimentação baseado em um único movimento
	public void RealizaMovimento(Movimento m) // Genú: Acho que "Mover" é um nome melhor e mais conciso. Também é sinônimo de "realizar movimento"
	{
		//verifica se tem captura de peça
		if (m.destino != null)
		{

		}
		//realiza o movimento

		m.destino.pecaAtual = m.origem.pecaAtual;
		m.destino.pecaAtual.posX = m.destino.posX;
		m.destino.pecaAtual.posY = m.destino.posY;
		m.origem.pecaAtual = null;
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
	public bool PodeCapturar(Peca alvo)
	{
		if (this.cor == alvo.cor)
			return false;

		if (this.cor != alvo.cor)
			return true;
	}
}
