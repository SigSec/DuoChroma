// Spike script, By Justinas "SigPi" Grigas
// Make sure your player has a Player tag attached.
// This script should be attached to objects that you want to act as spikes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour {

    public string playerTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if the object that collided with the spike is the player,
        // and if so, reload the scene.
        if (collision.gameObject.tag == playerTag)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
