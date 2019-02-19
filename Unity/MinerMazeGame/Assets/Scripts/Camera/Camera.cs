using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	[SerializeField]
	private GameObject player;


    private void LateUpdate()
    {
		// Follow the player's position
		transform.position = player.transform.position + new Vector3(0, 0, -10);
	}
}
