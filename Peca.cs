using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca {
    public int posX=-1, posY=-1;
    public bool JogadorBaixo;
    public char cor;
    Jogador jDono;

    public Peca(char c,bool cima,Jogador j)
    {
        cor = c;
        JogadorBaixo = cima;
        jDono = j;
    }

    //declara funcao de listar movimentos
    public virtual List<Movimento> listaMovimentos(Casa[,] tab, int x, int y)
    {
        return null;
    }

    //recebe o tabuleiro e realiza a movimentação baseado em um único movimento
    public void realizaMovimento(Movimento m)
    {
        //verifica se tem captura de peça
        if (m.casaCapturada != null)
        {

        }
        //realiza o movimento

        m.casaCapturada.pecaAtual = m.casaAtual.pecaAtual;
        m.casaCapturada.pecaAtual.posX = m.casaCapturada.posX;
        m.casaCapturada.pecaAtual.posY = m.casaCapturada.posY;
        m.casaAtual.pecaAtual = null;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
