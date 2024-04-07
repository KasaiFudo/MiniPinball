using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalObstacle : Obstacle
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.AddScore(100);
            sound.pitch = Random.Range(0.9f, 1.1f);
            sound.Play();
            StartCoroutine(LightControl());
        }
    }
}
