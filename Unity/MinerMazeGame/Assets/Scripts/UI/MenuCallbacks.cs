using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCallbacks : MonoBehaviour
{
	public void OpenMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void StartEasyLevel()
	{
		SceneManager.LoadScene("EasyLevel");
	}


	public void StartMediumLevel()
	{
		SceneManager.LoadScene("MediumLevel");
	}


	public void StartHardLevel()
	{
		SceneManager.LoadScene("HardLevel");
	}
}
