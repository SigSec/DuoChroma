/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.0
Date:		10/04/2018 19:12
*/

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	private PlayerManager _playerManager;

	private SpriteRenderer _healthPlaneR;
	private SpriteRenderer _healthPlaneG;

	private float _healthG;
	private float _healthR;

	private float _healthDecay;
	private float _healthRegen;
	private float _healthMax;
	private float _healingSpeed;

	public bool isHealing;

	private void Awake()
	{
		// Assign components.
		_playerManager = GetComponent<PlayerManager>();

		_healthPlaneR = GameObject.Find("HealthPlaneR").GetComponent<SpriteRenderer>();
		_healthPlaneG = GameObject.Find("HealthPlaneG").GetComponent<SpriteRenderer>();

		// Assign variables based on Player Manager.
		_healthMax = _playerManager.healthMax;
		_healthRegen = _playerManager.healthRegen;
		_healthDecay = _playerManager.healthDecay;
		_healingSpeed = _playerManager.healingSpeed;

		_healthG = _healthMax;
		_healthR = _healthMax;
	}

	private void Update()
	{
		// Update variables.
		bool isRed = _playerManager.isRed;

		// Check if the player is dead.
		if (_healthG <= 0 || _healthR <= 0)
		{
			_playerManager.Restart();
		}

		// Adjust health values depending on which side the player is on.
		else
		{
			if (isRed)
			{
				_healthR -= _healthDecay * Time.deltaTime;

				if (_healthG < _healthMax)
				{
					_healthG += _healthRegen * Time.deltaTime;
				}

				if (isHealing && _healthR < _healthMax)
				{
					_healthR += _healingSpeed * Time.deltaTime;
				}
				else
				{
					isHealing = false;
				}
			}

			else
			{
				_healthG -= _healthDecay * Time.deltaTime;

				if (_healthR < _healthMax)
				{
					_healthR += _healthRegen * Time.deltaTime;
				}

				if (isHealing && _healthG < _healthMax)
				{
					_healthG += _healingSpeed * Time.deltaTime;
				}
				else
				{
					isHealing = false;
				}
			}
		}

		// Update the opacity of the planes, to display health to the player.
		_healthPlaneG.color = new Color(_healthPlaneG.color.r, _healthPlaneG.color.g, _healthPlaneG.color.b, 1 - (_healthG / _healthMax));
		_healthPlaneR.color = new Color(_healthPlaneR.color.r, _healthPlaneR.color.g, _healthPlaneR.color.b, 1 - (_healthR / _healthMax));
	}
}