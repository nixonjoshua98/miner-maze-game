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
    public int currentHealth {
		get { return _currentHealth; }
		set { _currentHealth = Mathf.Max(0, Mathf.Min(maxHealth, value)); }
	}


    /* - -  - COOLDOWNS - - - - */
    private float reduceHealthCooldown;


    private void Start()
    {
        reduceHealthCooldown = reduceHealthTimer;
        _currentHealth = startingHealth;
    }


    public void UpdateHealth()
    {
        reduceHealthCooldown -= Time.deltaTime;

        if (reduceHealthCooldown <= 0.0f)
        {
            reduceHealthCooldown = reduceHealthTimer;
			currentHealth--;
        }

        healthText.text = currentHealth + "/" + maxHealth;
    }

	public bool IsDead()
    {
        return _currentHealth <= 0;
    }
}
