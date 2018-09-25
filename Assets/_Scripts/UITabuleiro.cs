using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabuleiro : MonoBehaviour
{

	public List<UIPiece> pieces = new List<UIPiece>();
	public GameObject whiteStonePawnPrefab;
	public GameObject blackStonePawnPrefab;
	public GameObject menuPrefab;

	private int color = 0; //0 = preto e 1 = branco // TODO: tentar reescrever para trabalhar com Jogador.Cor
	private bool clicked = false; //determina se já houve o clique no menu

	private Vector3 boardOffset = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 pieceOffset = new Vector3(3.0f, 0, 3.0f);

	private UIPiece selectedUIPiece;

	private UICasa startDrag;

	[SerializeField]
	private Partida partida;

	[SerializeField]
	private Tabuleiro tabuleiro;

	void Start()
	{
		partida = FindObjectOfType<Partida>();
		tabuleiro = FindObjectOfType<Tabuleiro>();
	}

	private void Update()
	{
		//if
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (!clicked)
				{ //verifica se o menu está ativo
					SelectColor(); //selecionou a cor
				}
				else
				{
					startDrag = GetSpaceUnderMouse();
				}
			}

			if (Input.GetMouseButtonUp(0) && startDrag)
			{
				UICasa endDrag = GetSpaceUnderMouse();
				//Debug.Log("Mouse Over: " + mouseOver);
				//TryMove(startDrag.casa.posX, startDrag.casa.posY, endDrag.casa.posX, endDrag.casa.posY);
				TryMove(startDrag, endDrag);

				startDrag = null;
			}
		}
	}

	private UICasa GetSpaceUnderMouse()
	{
		return Utilities.GetComponentFromMouseOver<UICasa>(layer: "Spaces");
	}

	private void TryMove(UICasa origem, UICasa destino)
	{
		if (origem == destino) return;

		UIPiece uiPiece = origem.CurrentUIPiece();
		if (uiPiece == null) return;

		uiPiece.TryMovePiece(origem, destino, tabuleiro);
	}

	private void SelectColor()
	{//função responsável por arrumar o tabuleiro de acordo com a cor
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.transform.name == "White")//se o GameObject que recebeu o clique tem o nome White
			{
				GenerateBoard();
				color = 1; //cor bege
				clicked = true; //indica que já escolheu a cor
				DestroyMenu(); //destrói tudo que faz parte do menu
			}
			else if (hit.transform.name == "Black")
			{//se o GameObject que recebeu o clique tem o nome Black
				GenerateBoard();
				color = 0; //cor preta
				clicked = true;
				DestroyMenu();
			}
		}
	}

	private void NewSelectColor() { } // TODO

	private void NewGenerateBoard(Tabuleiro tabuleiro) { } // TODO

	private void GenerateBoard()
	{ //tabuleiro gerado para caso a cor escolhida seja preto quando inverted é false
		int i = 0;

		for (int y = 0; y < 2; y++)
		{ //primeiras duas linhas do tabuleiro
			for (int x = 0; x < 8; x++)
			{ //cada coluna do tabuleiro
				Peca peca = partida.Jogador1.conjuntoPecas[i];
				i++;
				GenerateUIPiece(peca);
			}
		}

		i = 0;

		for (int y = 6; y < 8; y++)
		{ //últimas duas linhas do tabuleiro
			for (int x = 0; x < 8; x++)
			{ //cada coluna do tabuleiro
				Peca peca = partida.Jogador2.conjuntoPecas[i];
				i++;
				GenerateUIPiece(peca);
			}
		}
	}

	private void GenerateUIPiece(Peca peca)
	{
		GameObject prefab = FindObjectOfType<PrefabLib>().GetBestPrefab(peca);
		GameObject go = Instantiate(prefab) as GameObject;
		go.transform.SetParent(transform);
		UIPiece uiPiece = go.GetComponent<UIPiece>();
		pieces.Add(uiPiece);
		uiPiece.Piece = peca;
		uiPiece.UpdatePositionOnBoard(this);


	}

	public void DestroyMenu()
	{ //função responsável por deletar todos os GameObjects com a tag Menu
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Menu");
		foreach (GameObject enemy in enemies)
			GameObject.Destroy(enemy);
	}

	private void MoveUIPiece(UIPiece p, int x, int y)
	{
		float aux_x = 0.3f + 2.0f * Convert.ToSingle(x);
		float aux_z = -8.8f - 2.0f * Convert.ToSingle(y);
		p.transform.position = new Vector3(aux_x, 1.95f, aux_z);
	}

	private void NewMovePiece(Casa origem, Casa destino)
	{

	}

	public UICasa GetUICasa(Casa casa) { return GetUICasa(casa.PosX, casa.PosY); }
	public UICasa GetUICasa(int x, int y)
	{
		UICasa[] children = GetComponentsInChildren<UICasa>();

		string nameSought = "Casa " + Utilities.CoordinatesNumericToAlpha(x, y);

		foreach (var uiCasa in children)
		{
			if (uiCasa.name.CompareTo(nameSought) == 0) // se os nomes forem iguais
			{
				return uiCasa;
			}
		}

		return null;
	}

}
