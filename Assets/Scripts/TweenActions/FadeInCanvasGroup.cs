using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInCanvasGroup : MonoBehaviour
{
	private CanvasGroup canvasGroup;
	public float duration = 1.0f;
	public void OnEnable()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}
	public void Fade(float toAlpha)
	{
		canvasGroup.DOFade(toAlpha, duration);
	}
	public void SetDuration(float duration)
	{
		this.duration = duration;
	}
}
