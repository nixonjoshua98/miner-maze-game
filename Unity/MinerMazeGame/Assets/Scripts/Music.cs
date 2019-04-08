using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
	private void Awake()
	{
		if (GameObject.FindGameObjectsWithTag("Music").Length == 1)
			DontDestroyOnLoad(gameObject);
		else
			DestroyImmediate(GameObject.FindGameObjectsWithTag("Music")[1]);
	}
}
