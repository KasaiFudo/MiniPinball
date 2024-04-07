using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroutObstacle : Obstacle
{
    [SerializeField] private float resetSpeed = .5f;
    [SerializeField] private float diametr = 5f;
    [SerializeField] private float duration = 2f;
    [SerializeField] private int cooldown = 10;

    private bool isObstacleActive = true;
    private IEnumerator coroutine;
    private void FixedUpdate()
    {
        if (isObstacleActive)
        {
            foreach (var collider in Physics.OverlapSphere(transform.position, diametr))
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * resetSpeed);
                    gameManager.AddScore(3);
                    if (coroutine == null)
                    {
                        coroutine = DischargeOff();
                        StartCoroutine(coroutine);
                    }
                }
            }
        }
    }
    private IEnumerator DischargeOff()
    {
        sound.Play();
        yield return new WaitForSeconds(duration);
        isObstacleActive = false;
        TurnOffLight();
        yield return new WaitForSeconds(cooldown);
        isObstacleActive = true;
        TurnOnLight();
        coroutine = null;
    }
}
