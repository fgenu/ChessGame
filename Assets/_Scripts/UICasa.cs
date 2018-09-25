using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICasa : MonoBehaviour
{

	[HideInInspector]
	public Casa casa;



	void Start()
	{
		Tabuleiro tabuleiro = FindObjectOfType<Tabuleiro>();

		// Assume que o nome é no formato "Casa XY", em que X é qualquer letra e Y qualquer número.
		string coordenadas = this.name.Substring(startIndex:5);

		casa = tabuleiro.GetCasa(coordenadas);
	}

	public UIPiece CurrentUIPiece ()
	{
		UITabuleiro uiTabuleiro = FindObjectOfType<UITabuleiro>();
		
		foreach (var uiPiece in uiTabuleiro.pieces)
		{
			print("looloop");
			if (uiPiece != null && ReferenceEquals(uiPiece.Piece, this.casa.PecaAtual))
				return uiPiece;
		}

		return null;
	}
}
