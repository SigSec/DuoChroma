/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.0
Date:		10/04/2018 19:12
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	// Public variables.
	public SpriteRenderer rHpSprite;
	public SpriteRenderer gHpSprite;

	[HideInInspector] public float rHp;
	[HideInInspector] public float gHp;

	[Range(1f, 60f)]
	public float rHpMax;
	[Range(-60f, 0f)]
	public float rHpDecay;
	[Range(0f, 60f)]
	public float rHpRegen;

	[Range(1f, 60f)]
	public float gHpMax;
	[Range(-60f, 0f)]
	public float gHpDecay;
	[Range(0f, 60f)]
	public float gHpRegen;
	// Private variables.

	private float _healRate;

	private bool _healRed;
	private bool _isRed;
	private bool _isHealing = false;
	// Custom functions.

	public void Heal(bool red, float rate)
	{
		_isHealing = true;
		_healRed = red;
		_healRate = rate;
	}
	// Monobehaviour functions.

	private void Awake()
	{
		rHp = rHpMax;
		gHp = gHpMax;
	}

	private void Update()
	{
		_isRed = gameObject.GetComponent<GravityManager>().isRed;

		// Heal the player.
		if (_isHealing)
		{
			if (_healRed)
			{
				rHp += _healRate * Time.deltaTime;
			}

			else
			{
				gHp += _healRate * Time.deltaTime;
			}
		}

		// Check if dead
		if (rHp <= 0 || gHp <= 0)
		{
			// reload scene if dead.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else
		{
			// Decay and regen health.
			if (_isRed)
			{
				rHp += rHpDecay * Time.deltaTime;
				gHp += gHpRegen * Time.deltaTime;
			}
			else
			{
				gHp += gHpDecay * Time.deltaTime;
				rHp += rHpRegen * Time.deltaTime;
			}
			
			// Control value overflow
			if (rHp > rHpMax)
			{
				rHp = rHpMax;
				if (_isHealing && _healRed)
				{
					_isHealing = false;
				}
			}

			if (gHp > gHpMax)
			{
				gHp = gHpMax;

				if (_isHealing && !_healRed)
				{
					_isHealing = false;
				}
			}

			// Update the fade plane

			rHpSprite.color = new Color(rHpSprite.color.r, rHpSprite.color.g, rHpSprite.color.b, 1 - (rHp / rHpMax));
			gHpSprite.color = new Color(gHpSprite.color.r, gHpSprite.color.g, gHpSprite.color.b, 1 - (gHp / gHpMax));
		}
	}
}