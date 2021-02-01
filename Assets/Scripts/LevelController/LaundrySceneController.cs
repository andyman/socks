using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaundrySceneController : MonoBehaviour
{
	public Player player;
	public OneLiner playerSpeech;

	public SpeechLine[] playerStartLines;

	public Canvas worldCanvas;
	public static bool questionAsked = false;
	public static int radioTries = 0;
	private RigidbodyConstraints startConstraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
	private RigidbodyConstraints freeConstraints = RigidbodyConstraints.FreezeRotation;


	private void Awake()
	{
		questionAsked = false;
		radioTries = 0;
	}

	public void GoToNextScene()
	{
		SceneManager.LoadScene("JunkyardScene");
	}

	public void MarkQuestionAsked()
	{
		questionAsked = true;
	}

	public void IncrementRadioTries()
	{
		radioTries++;
	}
	public void PlayAboutToEnterPortalLine()
	{
		playerSpeech.Play(playerStartLines[3]);
	}

	// Start is called before the first frame update
	IEnumerator Start()
	{
		player.anim.SetBool("grounded", true);
		//player.enabled = false;
		worldCanvas.gameObject.SetActive(false);
		yield return new WaitForSeconds(2.0f);

		// speech line 1

		//player.rb.constraints = startConstraints;

		// free for now
		worldCanvas.gameObject.SetActive(true);
		player.rb.constraints = freeConstraints;

		player.rb.velocity = player.rb.velocity + Vector3.up * 3.0f;
		player.PlayJumpSound();

		// i'm so clean and fresh now
		playerSpeech.Play(playerStartLines[0]);
		float waitTime = Time.time + 2.0f;

		while (Time.time < waitTime)
		{
			if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Jump"))
			{
				waitTime = 0;
				break;
			}
			yield return 0;

		}
		yield return new WaitForSeconds(0.1f);
		// hmm, where's my partner? probably mixed up with the other laundry?
		playerSpeech.Play(playerStartLines[1]);

		waitTime = Time.time + 3.0f;

		while (Time.time < waitTime)
		{
			if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Jump"))
			{
				waitTime = 0;
				break;
			}
			yield return 0;

		}
		yield return new WaitForSeconds(0.1f);
		// I'd better go find them. I miss them already"
		playerSpeech.Play(playerStartLines[2]);
		player.enabled = true;
		yield return new WaitForSeconds(1.0f);
		worldCanvas.gameObject.SetActive(true);
		player.rb.constraints = freeConstraints;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Slash))
		{
			GoToNextScene();
		}
	}
}
