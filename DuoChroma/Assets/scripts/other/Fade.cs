// Fade object script
// By Justinas "SigmaPi" Grigas
// Version 0.2.0
// This script grabs the sprite renderer that is attached to the gameobject and either increments or decrements the _colour value.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {
    // The speed at witch the _colour changes.
    public float fadingSpeed;
    public bool isFading = false;
    private SpriteRenderer _spriteRenderer;
    private Color _colour;

    public bool HasFaded()
    {
        if (_colour.a == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _colour = _spriteRenderer.color;
        if (isFading && _colour.a > 0)
        {
            _colour.a -= fadingSpeed * Time.deltaTime;
        }
        else if (!isFading && _colour.a < 1)
        {
            _colour.a += fadingSpeed * Time.deltaTime;
        }
        _spriteRenderer.color = _colour;

        if (!isFading && _colour.a >= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
