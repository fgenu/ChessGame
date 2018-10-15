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
		controller.SelectObject(this);
	}

	private void OnMouseUp() {
		controller.NoSelectObject(this);
	}

	public void TryMovePiece(UICasa origem, UICasa destino, Tabuleiro tabuleiro)
	{
		var movimento = new Movimento(origem: origem.casa, destino: destino.casa);

        if (Piece.PodePercorrer(movimento, tabuleiro))
        {
            if ((tabuleiro.turno == 1 && origem.casa.PecaAtual.jDono == tabuleiro.partida.Jogador1) || (tabuleiro.turno == 2 && origem.casa.PecaAtual.jDono == tabuleiro.partida.Jogador2))
            { 
                MovePiece(origem, destino);
                tabuleiro.trocaTurno();
            }
        }
	}

	private void MovePiece(UICasa origem, UICasa destino)
	{
		Piece.RealizaMovimento(new Movimento(origem: origem.casa, destino: destino.casa));
		VisuallyMove(origem, destino);
	}

	private void VisuallyMove(UICasa origem, UICasa destino)
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
