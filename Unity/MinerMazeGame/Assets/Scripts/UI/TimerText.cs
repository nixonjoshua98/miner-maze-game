using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
	[SerializeField]
	private Text text;


	private void FixedUpdate()
	{
		text.text = Mathf.CeilToInt(Time.timeSinceLevelLoad).ToString();
	}
}
