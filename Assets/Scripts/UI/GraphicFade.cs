using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicFade : MonoBehaviour {
	public float fadeInTime = 1f;
	Color originalColor;
	Graphic uiElement;

	// Use this for initialization
	void Awake () {
		uiElement = GetComponent<Graphic> ();
		originalColor = uiElement.color;
		uiElement.color = Color.clear;
	}

	public void FadeIn() {
		StartCoroutine(FadeInRoutine());
	}

	IEnumerator FadeInRoutine() {
		for (float t = 0.01f; t < fadeInTime; t += Time.deltaTime)
		{
			uiElement.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t/fadeInTime));
			yield return null;
		}
	}
}
