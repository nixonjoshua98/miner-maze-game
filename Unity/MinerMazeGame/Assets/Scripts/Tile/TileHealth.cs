using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHealth : MonoBehaviour
{
	private float health = 2.0f;

	private SpriteRenderer sRenderer;

	private void Awake()
	{
		sRenderer = GetComponent<SpriteRenderer>();
	}


	public void TakeDamage(float dmg)
	{
		health -= dmg;
	}

	public bool IsDestroyed()
	{
		return health <= 0.0f;
	}
}
