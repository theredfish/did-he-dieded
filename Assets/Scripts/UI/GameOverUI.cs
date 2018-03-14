using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
	public float fadeInTime = 0.5f;
    public float fadeOutTime = 1f;
    Color originalColor;
	Graphic uiElement;
    bool isFading = false;

    // Use this for initialization
    void Awake () {
		uiElement = GetComponent<Graphic> ();
		originalColor = uiElement.color;
		uiElement.color = Color.clear;
	}

    public void FadeInOut()
    {
        if (!isFading)
        {
            StartCoroutine(FadeInOutRoutine());
        }

    }

	IEnumerator FadeInOutRoutine() {
        isFading = true;

        for (float t = 0.01f; t < fadeInTime; t += Time.deltaTime)
		{
			uiElement.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t/fadeInTime));
			yield return null;
		}

        StartCoroutine(FadeOutRoutine());

        isFading = false;
    }

    IEnumerator FadeOutRoutine() {
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            uiElement.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
	}
}
