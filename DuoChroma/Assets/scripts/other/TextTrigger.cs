using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {
    public KeyCode triggerKey;
    public float _fadingSpeed = 1;
    public bool isFading = false;

    private SpriteRenderer _spriteRenderer;
    private bool _hasTriggered;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        Color colour = _spriteRenderer.color;

        if (Input.GetKeyDown(triggerKey) && colour.a >= 1)
        {
            _hasTriggered = true;
            isFading = true;
        }

        if (isFading && colour.a > 0)
        {
            colour.a -= _fadingSpeed * Time.deltaTime;
        }
        else if (!isFading && colour.a < 1)
        {
            colour.a += _fadingSpeed * Time.deltaTime;
        }

        _spriteRenderer.color = colour;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_hasTriggered)
        {
            isFading = false;
        }
    }
}
