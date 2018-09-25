using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPiece : MonoBehaviour {

	public Peca Piece { get; set; }

	public void TryMovePiece (UICasa origem, UICasa destino, Tabuleiro tabuleiro)
	{
		var movimento = new Movimento(origem:origem.casa, destino:destino.casa);

		if (Piece.PodePercorrer(movimento, tabuleiro))
			MovePiece(origem, destino);
	}
	
	private void MovePiece (UICasa origem, UICasa destino)
	{
		Piece.RealizaMovimento(new Movimento(origem:origem.casa, destino:destino.casa));
		VisuallyMove(origem, destino);
	}

	private void VisuallyMove (UICasa origem, UICasa destino)
	{
		// TODO: Fazer movimento mais smooth baseado na posição de origem

		UpdatePositionOnBoard(FindObjectOfType<UITabuleiro>()); // temporário
	}

	public void UpdatePositionOnBoard(UITabuleiro uiTabuleiro)
	{
		UICasa casa = uiTabuleiro.GetUICasa(Piece.posX, Piece.posY);
		print("My new position is: " + Piece.posX + ", " + Piece.posY);

		float x, y, z;

		// Fica nas mesmas coordenadas da casa, mas acima dela.
		x = casa.transform.position.x;
		y = casa.transform.position.y + casa.GetComponent<Collider>().bounds.size.y / 2;
		z = casa.transform.position.z;

		this.transform.position = new Vector3(x, y, z);
	}

}
