using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class SpeechLine
{
	[Multiline]
	public string text = "hello world";
	public float defaultDuration = 3;
	public AudioClip clip = null;
	public UnityEvent actionEvent;

}

public class OneLiner : MonoBehaviour
{
	public bool random = false;
	public SpeechLine[] lines;

	public TextMeshProUGUI textUI;
	public Canvas myCanvas;

	public int nextLineIndex = 0;
	public bool loopable = true;
	public RectTransform textBacker;
	public float pitchMultiplier = 1.0f;
	public float volumeMultiplier = 0.5f;

	private Camera cam;
	private bool lineActive = false;

	public AudioSource audioSource;
	public AudioSet speechSet;

	private Interactable interactableToEnable = null;

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		if (lineActive)
		{
			Vector3 worldPos = transform.position;
			Vector3 screenPoint = cam.WorldToScreenPoint(worldPos);
			float scaleFactor = myCanvas.scaleFactor;
			Vector2 finalPosition = new Vector2(screenPoint.x / scaleFactor, screenPoint.y / scaleFactor);
			textUI.rectTransform.anchoredPosition = finalPosition;
			textBacker.anchoredPosition = finalPosition;
		}
	}

	public void Play(Interactable interactable)
	{
		if (interactableToEnable != null)
		{
			interactableToEnable.enabled = true;
		}

		interactable.enabled = false;
		interactableToEnable = interactable;

		Play();
	}
	public void JustPlay()
	{
		Play();
	}
	public void Say(string messageTExt)
	{
		SpeechLine speech = new SpeechLine();
		speech.text = messageTExt;
		speech.defaultDuration = 3;
		Play(speech);
		speechLinesUsed.Add(speech);
	}
	private List<SpeechLine> speechLinesUsed = new List<SpeechLine>();

	public void Play(SpeechLine line = null)
	{

		if (line == null)
		{
			if (random)
			{
				line = lines[Random.Range(0, lines.Length)];
			}
			else if (nextLineIndex != -1)
			{
				line = lines[nextLineIndex];
				nextLineIndex = (nextLineIndex + 1) % lines.Length;
				//if (nextLineIndex >= lines.Length && loopable)
				//{
				//	nextLineIndex = 0;
				//}
				//else
				//{
				//	nextLineIndex = -1;
				//}
			}
		}

		if (line == null) return;

		Vector3 worldPos = transform.position;
		Vector3 screenPoint = cam.WorldToScreenPoint(worldPos);
		float scaleFactor = myCanvas.scaleFactor;
		Vector2 finalPosition = new Vector2(screenPoint.x / scaleFactor, screenPoint.y / scaleFactor);
		textUI.rectTransform.anchoredPosition = finalPosition;
		textBacker.anchoredPosition = finalPosition;
		myCanvas.gameObject.SetActive(true);

		if (textPlay != null)
		{
			StopCoroutine(textPlay);
			lineActive = false;
		}

		if (speechSet != null)
		{
			speechSet.PlayRandom(transform.position, volumeMultiplier, volumeMultiplier, pitchMultiplier, pitchMultiplier).transform.parent = transform;
		}

		textPlay = StartCoroutine(TextPlayCoroutine(line));

	}
	private Coroutine textPlay;

	private IEnumerator TextPlayCoroutine(SpeechLine line)
	{
		lineActive = true;
		textUI.text = line.text.Replace("|", "\n");
		textBacker.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textUI.preferredHeight + 10);
		textBacker.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textUI.preferredWidth + 10);
		if (line.actionEvent != null)
		{
			line.actionEvent.Invoke();
		}
		if (line.clip != null)
		{
			audioSource.clip = line.clip;
			audioSource.Play();
			yield return new WaitForSeconds(line.clip.length);
		}
		else
		{
			yield return new WaitForSeconds(line.defaultDuration);
		}

		textUI.text = "";
		myCanvas.gameObject.SetActive(false);
		lineActive = false;
		if (interactableToEnable != null)
		{
			interactableToEnable.enabled = true;
			interactableToEnable = null;
		}


	}
}
