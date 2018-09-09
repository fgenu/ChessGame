using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador {
    public Peca[] conjuntoPecas = new Peca[16];
    char cor;
    public Jogador(char c,bool cima){
        cor = c;
        inicializaPecas(cima);
    }
    //inicializa as pecas do jogador
    void inicializaPecas(bool cima)
    {
        //inicializa as peças especiais da linha mais a borda
        conjuntoPecas[0] = new Torre(cor,cima,this);
        conjuntoPecas[1] = new Cavalo(cor,cima,this);
        conjuntoPecas[2] = new Bispo(cor,cima,this);
        conjuntoPecas[3] = new Rainha(cor,cima, this);
        conjuntoPecas[4] = new Rei(cor,cima, this);
        conjuntoPecas[5] = new Bispo(cor,cima, this);
        conjuntoPecas[6] = new Cavalo(cor,cima, this);
        conjuntoPecas[7] = new Torre(cor,cima, this);

        //inicializa os peoes
        for (int i = 8; i < 16; i++)
        {
            conjuntoPecas[i] = new Peao(cor,cima, this);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
