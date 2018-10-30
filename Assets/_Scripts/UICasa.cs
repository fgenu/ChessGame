using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICasa : MonoBehaviour
{

	[HideInInspector]
	public Casa casa;

	public float animationTime = 1f;
  	public float threshold = 1.5f;
	
	private HighlightController controller;
  	private Material material;
  	private Color normalColor;
  	private Color selectedColor;

	private void Awake() {
		material = GetComponent<MeshRenderer>().material;
		controller = FindObjectOfType<HighlightController>();

		normalColor = material.color;
		selectedColor = new Color(0,255,0);
  	}

	void Start()
	{
		Tabuleiro tabuleiro = GetComponentInParent<UITabuleiro>().Tabuleiro;

		// Assume que o nome é no formato "Casa XY", em que X é qualquer letra e Y qualquer número.
		string coordenadas = this.name.Substring(startIndex: 5);

		casa = tabuleiro.GetCasa(coordenadas);
        casa.uiC = this;
	}

	public UIPiece CurrentUIPiece()
	{
		UITabuleiro uiTabuleiro = GetComponentInParent<UITabuleiro>();

		foreach (var uiPiece in uiTabuleiro.pieces)
		{
			if (uiPiece != null && ReferenceEquals(uiPiece.Piece, this.casa.PecaAtual))
				return uiPiece;
		}

		return null;
	}

	public void StartHighlight() {
		iTween.ColorTo(gameObject, iTween.Hash(
		"color", selectedColor,
		"time", animationTime + Random.Range(0f,0.1f),
		"easetype", iTween.EaseType.linear,
		"looptype", iTween.LoopType.pingPong
		));
	}

	public void StopHighlight() {
		Debug.Log("Stopping iTweens"); 
		iTween.Stop(gameObject);
		material.color = normalColor;
	}
}
