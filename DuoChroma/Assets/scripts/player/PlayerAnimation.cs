using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    // Set up variables.
    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_playerTransform.position.y > 0)
        {
            _playerTransform.localScale = new Vector3(_playerTransform.localScale.x, Mathf.Abs(_playerTransform.localScale.y), _playerTransform.localScale.z);
        }
        else
        {
            _playerTransform.localScale = new Vector3(_playerTransform.localScale.x, -1 * Mathf.Abs(_playerTransform.localScale.y), _playerTransform.localScale.z);
        }
    }
}
