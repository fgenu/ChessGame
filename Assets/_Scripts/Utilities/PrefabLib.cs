using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLib : MonoBehaviour
{

	[SerializeField] public GameObject BlackPawn;
	[SerializeField] public GameObject BlackKnight;
	[SerializeField] public GameObject BlackBishop;
	[SerializeField] public GameObject BlackRook;
	[SerializeField] public GameObject BlackQueen;
	[SerializeField] public GameObject BlackKing;
	[SerializeField] public GameObject WhitePawn;
	[SerializeField] public GameObject WhiteKnight;
	[SerializeField] public GameObject WhiteBishop;
	[SerializeField] public GameObject WhiteRook;
	[SerializeField] public GameObject WhiteQueen;
	[SerializeField] public GameObject WhiteKing;

	public GameObject GetBestPrefab (Peca peca)
	{
		if (peca.Cor == 'p')
		{
			if (peca is Peao)	return BlackPawn;
			if (peca is Cavalo)	return BlackKnight;
			if (peca is Bispo)	return BlackBishop;
			if (peca is Torre)	return BlackRook;
			if (peca is Rainha)	return BlackQueen;
			if (peca is Rei)	return BlackKing;

			Debug.LogWarning("Não há prefab para peças de outro tipo. Por padrão, foi atribuído o de Peão.");
			return BlackPawn;
		}
		else if (peca.Cor == 'b')
		{
			if (peca is Peao)	return WhitePawn;
			if (peca is Cavalo)	return WhiteKnight;
			if (peca is Bispo)	return WhiteBishop;
			if (peca is Torre)	return WhiteRook;
			if (peca is Rainha)	return WhiteQueen;
			if (peca is Rei)	return WhiteKing;

			Debug.LogWarning("Não há prefab para peças de outro tipo. Por padrão, foi atribuído o de Peão.");
			return WhitePawn;
		}

		Debug.LogError("Não há prefab para peças de outra cor.");
		return null;
	}
}
