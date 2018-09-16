using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabuleiro : MonoBehaviour {

	public Piece[,] pieces = new Piece[8,8];
	public GameObject whiteStonePawnPrefab;
	public GameObject blackStonePawnPrefab;
	public GameObject menuPrefab;

	private int color=0; //0 = preto e 1 = branco
	private bool clicked=false; //determina se já houve o clique no menu

	private Vector3 boardOffset = new Vector3(0.0f,0.0f,0.0f);
	private Vector3 pieceOffset = new Vector3(3.0f,0,3.0f);

	private Piece selectedPiece;

	private Vector2 mouseOver;
	private Vector2 startDrag;
	private Vector2 endDrag;

	// Use this for initialization
	private void Start () {
	}
	
	private void Update(){
		UpdateMouseOver();

		//Debug.Log(mouseOver);

		//if
		{
			int x = (int)mouseOver.x;
			int y = (int)mouseOver.y;
			if(Input.GetMouseButtonDown(0)){
				if(!clicked){ //verifica se o menu está ativo
					selectColor(); //selecionou a cor
				}
				else{
					selectPiece(x, y); //função que serve para verificar que a peça foi selecionada
				}
			}
			
			if(Input.GetMouseButtonUp(0))
				//Debug.Log("Mouse Over: " + mouseOver);
				TryMove((int)startDrag.x, (int)startDrag.y, x/2, y/2 + 11);
		}
	}

	private void UpdateMouseOver(){ //função responsável por lidar com os movimentos do mouse
		
		if(!Camera.main){
			Debug.Log("Unable to find main camera");
			return;
		}

		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board"))){
			mouseOver.x = (int)hit.point.x;
			mouseOver.y = (int)hit.point.z;
		}
		else{
			mouseOver.x = -1;
			mouseOver.y = -1;
		}
	}

	private void selectColor(){//função responsável por arrumar o tabuleiro de acordo com a cor
		RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    	if(Physics.Raycast (ray, out hit))
		{
			if(hit.transform.name == "White")//se o GameObject que recebeu o clique tem o nome White
			{
				GenerateBoardInverted();
				color = 1; //cor bege
				clicked = true; //indica que já escolheu a cor
				DestroyMenu(); //destrói tudo que faz parte do menu
			}
			else if(hit.transform.name == "Black"){//se o GameObject que recebeu o clique tem o nome Black
				GenerateBoard();  
				clicked = true;   
				DestroyMenu();           
			}
		}
	}

	private void selectPiece(int x, int y){
		//fora da matriz
		//Cada casa do tabuleiro na posição y estava sendo lida de -8 até -22 por algum motivo
		//Cada casa do tabuleiro na posição x estava sendo lida de 0 até 14 porque parece que cada casa do tabuleiro corresponde a duas casas da matriz graficamente
		if(x/2 < 0 || x/2 >= pieces.Length || y/2 > -4 || y/2 < -11){ //checa os limites da matriz na interface do tabuleiro
			Debug.Log(x);
			Debug.Log(y);
			return;
		}

		//x/2 porque, como disse acima, x está com o valor dobrado pq cada casa do tabuleiro corresponde a 2 da matriz
		//y/2 porque, além do valor ser dobrado, y começava a partir de -4 e ia até -22(dividindo por 2 dá o limite de 11)
		Piece p = pieces[x/2, y/2 + 11]; //procura a peça no x,y de quando o clique começou
		if(p!=null){
			selectedPiece = p;
			startDrag = mouseOver;
			Debug.Log(selectedPiece.name);
		}
	}

	private void TryMove(int x1, int y1, int x2, int y2){
		selectedPiece = pieces[x1/2, 7-(y1/2+11)]; //procura a peça nas posições de quando o clique começou
		//A posição y também estava sendo lida de forma espelhada, a última linha do tabuleiro correspondia a primeira linha da matriz
		//Por isso esse 7-, já que a matriz vai de 0 a 7 nas colunas e linhas

		MovePiece(selectedPiece, x2, 7-y2); //move a peça para o clique terminou
	}

	private void GenerateBoard(){ //tabuleiro gerado para caso a cor escolhida seja preto
		for(int y = 0; y < 2; y++){ //primeiras duas linhas do tabuleiro
			for(int x = 0; x < 8; x++){ //cada coluna do tabuleiro
				GenerateWhitePiece(x,y);
			}
		}
		for(int y = 6; y < 8; y++){ //últimas duas linhas do tabuleiro
			for(int x = 0; x < 8; x++){ //cada coluna do tabuleiro
				GenerateBlackPiece(x,y);
			}
		}
	}

	private void GenerateBoardInverted(){ //tabuleiro gerado para caso a cor escolhida seja bege
		for(int y = 0; y < 2; y++){
			for(int x = 0; x < 8; x++){
				GenerateBlackPiece(x,y);
			}
		}
		for(int y = 6; y < 8; y++){
			for(int x = 0; x < 8; x++){
				GenerateWhitePiece(x,y);
			}
		}
	}

	private void GenerateWhitePiece(int x, int y){//função responsável por gerar as peças brancas na matriz do tabuleiro
		GameObject go = Instantiate(whiteStonePawnPrefab) as GameObject;
		go.transform.SetParent(transform);
		Piece p = go.GetComponent<Piece>();
		pieces[x,y] = p;
		MovePiece(p, x, y);
	}

	private void GenerateBlackPiece(int x, int y){ //função responsável por gerar as peças pretas na matriz do tabuleiro
		GameObject go = Instantiate(blackStonePawnPrefab) as GameObject;
		go.transform.SetParent(transform);
		Piece p = go.GetComponent<Piece>();
		pieces[x,y] = p;
		MovePiece(p, x, y);
	}

	public void DestroyMenu(){ //função responsável por deletar todos os GameObjects com a tag Menu
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Menu");
   		foreach(GameObject enemy in enemies)
   			GameObject.Destroy(enemy);
	}

	private void MovePiece(Piece p, int x, int y){
		float aux_x = 0.3f + 2.0f*Convert.ToSingle(x);
		float aux_z = -8.8f - 2.0f*Convert.ToSingle(y);
		p.transform.position = new Vector3(aux_x,1.95f,aux_z);
	}
}
