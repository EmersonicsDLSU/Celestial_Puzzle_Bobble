using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has a specific tag
        if (other.CompareTag("GemBall") )
            Debug.Log($"Name: {other.name} Mobility:{other.GetComponentInChildren<GemBall_Status>().GetMobility()}");
        if (other.CompareTag("GemBall") && 
            other.GetComponentInChildren<GemBall_Status>().GetMobility() == EGemBallMobility.STATIC)
        {
            // TODO: Perform game over
            FindObjectOfType<AudioManager>().Play(SoundCode.GAME_OVER);
            FindObjectOfType<PlayerStatus>()._isLose = true;
            // open lose window 
            FindObjectOfType<UI_Settings>().OpenLoseWindow();
            Debug.Log($"GAMEOVER!");
        }
    }
}
