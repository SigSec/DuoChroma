using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {
    public float fadingSpeed;
    public bool isFading = true;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFading = false;
        }
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
            print(_colour.a);
            _colour.a += fadingSpeed * Time.deltaTime;
        }
        _spriteRenderer.color = _colour;
    }
}
