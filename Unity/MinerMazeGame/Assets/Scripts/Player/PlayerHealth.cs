using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    /* - - - - INSPECTOR GAMEOBJECTS - - - - */
    [SerializeField]
    Text healthText;


    /* - - - - INSPECTOR PRIVATES - - - - */
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int startingHealth;

	[SerializeField, Range(0.1f, 5.0f)]
	private float reduceHealthTimer;


    /* - - - - INTERNAL PRIVATES - - - - */
    private int _currentHealth;


    /* - - - - PUBLIC ACCESSORS - - - - */
    public int currentHealth { get { return _currentHealth; } }


    /* - -  - COOLDOWNS - - - - */
    private float reduceHealthCooldown;


    private void Start()
    {
        reduceHealthCooldown = reduceHealthTimer;
        _currentHealth = startingHealth;
    }


    private void Update()
    {
        reduceHealthCooldown -= Time.deltaTime;

        if (reduceHealthCooldown <= 0.0f)
        {
            reduceHealthCooldown = reduceHealthTimer;
            _currentHealth = Mathf.Max(0, _currentHealth - 1);
        }

        healthText.text = currentHealth + "/" + maxHealth;
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("HealthPotion"))
		{
			_currentHealth = Mathf.Min(100, _currentHealth + 20);
			Destroy(collision.gameObject);
			MineManager.instance.SpawnNewPotion();
		}

		else if (collision.CompareTag("Enemy"))
		{
			_currentHealth = Mathf.Max(0, _currentHealth - 10);
		}
	}


	public bool IsDead()
    {
        return _currentHealth <= 0;
    }
}
