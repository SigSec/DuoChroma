using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private Transform _cameraTransform;
    private Transform _playerTransform;

    private void Awake()
    {
        _cameraTransform = GetComponent<Transform>();
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        _cameraTransform.position = new Vector3(Mathf.Ceil(_playerTransform.position.x * 100) / 100,
                                                Mathf.Ceil(_playerTransform.position.y * 100) / 100,
                                                _cameraTransform.position.z);
    }
}
