using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has a specific tag
        if (other.CompareTag("GemBall"))
        {
            // TODO: Perform game over
            FindObjectOfType<AudioManager>().Play(SoundCode.GAME_OVER);
        }
    }
}
