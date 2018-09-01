using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador {
    public Peca[] conjuntoPecas = new Peca[16];
    char cor;
    public Jogador(char c){
        cor = c;
        inicializaPecas();
    }
    //inicializa as pecas do jogador
    void inicializaPecas()
    {
        //inicializa as peças especiais da linha mais a borda
        conjuntoPecas[0] = new Torre(cor);
        conjuntoPecas[1] = new Cavalo(cor);
        conjuntoPecas[2] = new Bispo(cor);
        conjuntoPecas[3] = new Rainha(cor);
        conjuntoPecas[4] = new Rei(cor);
        conjuntoPecas[5] = new Bispo(cor);
        conjuntoPecas[6] = new Cavalo(cor);
        conjuntoPecas[7] = new Torre(cor);

        //inicializa os peoes
        for(int i = 8; i < 16; i++)
        {
            conjuntoPecas[i] = new Peao(cor);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
