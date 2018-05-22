using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
	public Question[] questions;
	
	void Start ()
	{
	    questions = questions.Concat(Question.Seed()).ToArray();
	}

	void Update () {
	
	}
}
