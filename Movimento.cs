using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento {
    public int posX,posY,valor;
    public Casa casaCapturada,casaAtual;

    public Movimento(int x, int y,Casa c,Casa a) {
        posX = x;
        posY = y;
        valor = 0;
        casaCapturada = c;
        casaAtual = a;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
