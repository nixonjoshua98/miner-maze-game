using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
	/* - - - - INSPECTOR GAMEOBJECTS - - - - */
	[SerializeField]
	Text scoreTxt;


	/* - - - - PUBLIC ACCESSORS - - - - */
	[HideInInspector]
	public float score {
		get { return _score; }
		set { _score = Mathf.Max(0, value); }
	}

	/* - - - - INTERNAL PRIVATES - - - - */
	private float _score = 0.0f;

	public void UpdateScore()
	{
		score += (Time.deltaTime * 2.5f);

		scoreTxt.text = ((int)score).ToString().PadLeft(5, '0');
	}
}
