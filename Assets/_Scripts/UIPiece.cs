using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: fazer funcionar a promoção do peão
[RequireComponent(typeof(MeshRenderer))]
public class UIPiece : MonoBehaviour
{
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
		selectedColor = new Color(255,0,0);
  	}

	public Peca Piece { get; set; }

	public void StartHighlight() {
		iTween.ColorTo(gameObject, iTween.Hash(
		"color", selectedColor,
		"time", animationTime,
		"easetype", iTween.EaseType.linear,
		"looptype", iTween.LoopType.pingPong
		));
	}

	public void StopHighlight() {
		iTween.Stop(gameObject);
		material.color = normalColor;
	}

	private void OnMouseDown() {
		UITabuleiro uiTabuleiro = FindObjectOfType<UITabuleiro>();
		UICasa casa;
		List<Movimento> possibilidades = Piece.ListaMovimentos();
		foreach (var possibilidade in possibilidades){
				casa = uiTabuleiro.GetUICasa(possibilidade.destino);
				casa.StartHighlight();
		}
		controller.SelectObject(this);
	}

	private void OnMouseUp() {
		UITabuleiro uiTabuleiro = FindObjectOfType<UITabuleiro>();
		UICasa casa;
		List<Movimento> possibilidades = Piece.ListaMovimentos();
		foreach (var possibilidade in possibilidades){
				casa = uiTabuleiro.GetUICasa(possibilidade.destino);
				casa.StopHighlight();
		}
		controller.NoSelectObject(this);
	}

	public void TryMovePiece(UICasa origem, UICasa destino, Tabuleiro tabuleiro)
	{
		var movimento = new Movimento(origem: origem.casa, destino: destino.casa);

        if (Piece.PodePercorrer(movimento, tabuleiro))
        {
            if ((origem.casa.PecaAtual.jDono == tabuleiro.partida.JogadorDaVez()))
            {
				MovePiece(origem, destino);
				tabuleiro.partida.PassarAVez();
            }
        }
	}

	private void MovePiece(UICasa origem, UICasa destino)
	{
		var movimento = Peca.ValidarMovimento(new Movimento(origem: origem.casa, destino: destino.casa));
		
		UIPiece captured = null;
		if (destino.casa.PecaAtual != null)
		{
			captured = destino.CurrentUIPiece();
		}
		else if (movimento.pecaCapturada != null)
		{
			var uiTabuleiro = FindObjectOfType<UITabuleiro>();
			captured = uiTabuleiro.GetUICasa(movimento.pecaCapturada.CasaAtual).CurrentUIPiece();
		}

		if (captured != null)
			Destroy(captured.gameObject);

		Piece.RealizaMovimento(movimento);

		VisuallyMove(movimento);
	}

	private void VisuallyMove(Movimento movimento)
	{
		// TODO: Fazer movimento mais smooth baseado na posição de origem

		UpdatePositionOnBoard(FindObjectOfType<UITabuleiro>()); // temporário
	}

	public void UpdatePositionOnBoard(UITabuleiro uiTabuleiro)
	{

		UICasa casa = uiTabuleiro.GetUICasa(Piece.CasaAtual);

		if (casa == null)
			return;

		float x, y, z;

		// Fica nas mesmas coordenadas da casa, mas acima dela.
		x = casa.transform.position.x;
		y = casa.transform.position.y + casa.GetComponent<Collider>().bounds.size.y / 2;
		z = casa.transform.position.z;

		this.transform.position = new Vector3(x, y, z);
	}

}
