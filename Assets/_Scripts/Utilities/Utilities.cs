using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Utilities
{
	public static string CoordinatesNumericToAlpha (int x, int y)
	{
		char letter = (char) (x + 'A');
		int number = y + 1;

		return string.Concat(letter.ToString(), number.ToString());
	}

	public static T GetComponentFromMouseOver<T> (string layer)
	{
		if (!Camera.main)
		{
			Debug.LogWarning("Unable to find main camera");
			return default(T);
		}

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask(layer)))
			return hit.transform.GetComponent<T>();
		else
			return default(T);
	}
}
