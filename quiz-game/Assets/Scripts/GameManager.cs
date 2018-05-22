using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
	public Question[] questions;
	private static int score = 0;

	private IList<Question> unanswered;
	private Question currentQuestion;

	private float delayTime = 1.5f;
	IEnumerator DisplayNextQuestion() {
		yield return new WaitForSeconds(delayTime);
		//Application.LoadLevel("scene");
		trueButton.SetActive(true);
		falseButton.SetActive(true);
		answerText.text = "";
		SetCurrentQuestion();
	}

	[SerializeField]
	private Text questionText;

	[SerializeField]
	private Text answerText;

	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private GameObject trueButton;

	[SerializeField]
	private GameObject falseButton;

	public void UserSelect(bool answer) {
		trueButton.SetActive(false);
		falseButton.SetActive(false);

		if(answer == currentQuestion.isTrue) {
			answerText.text = "Yeeep!";
			answerText.color = new Color(29 / 255f, 174 / 255f, 96 / 255f);
			score += 1;
		}
		else {
			answerText.text = "Nop :(";
			answerText.color = new Color(216 / 255f, 27 / 255f, 70 / 255f);
		}

		scoreText.text = "Bodovi: " + score;

		StartCoroutine("DisplayNextQuestion");
	}

	void SetCurrentQuestion() {
		int index = UnityEngine.Random.Range(0, unanswered.Count);

		currentQuestion = unanswered[index];
		questionText.text = currentQuestion.statement;

		unanswered.Remove(currentQuestion);
	}
	
	void Start () {
		unanswered = new List<Question>();
		if (!unanswered.Any ()) {
			unanswered = questions.ToList();
		}

		trueButton.SetActive(true);
		falseButton.SetActive(true);
		answerText.text = "";

		SetCurrentQuestion();
	}

	void Update () {
	
	}
}
