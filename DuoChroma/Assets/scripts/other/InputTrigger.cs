using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTrigger : MonoBehaviour {
    public KeyCode triggerKey = KeyCode.X;
    public GameObject fadePlane;
    private bool _hasTriggered = false;

    private void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            fadePlane.GetComponent<Fade>().isFading = false;
        }
    }
}
