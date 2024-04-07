using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Vector3 oldPos;
    private AudioSource sound;
    void Start()
    {
        oldPos = transform.position;
        sound = GetComponentInChildren<AudioSource>();
    }
    void FixedUpdate()
    {
        if (oldPos != transform.position)
        {
            if(!sound.isPlaying)
            {
                sound.Play();
            }

        }
        oldPos = transform.position;
    }
}
