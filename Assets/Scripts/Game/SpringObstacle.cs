using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringObstacle : Obstacle
{
    public float springForce = 10f;

    // Вызывается, когда другой коллайдер входит в зону триггера
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, принадлежит ли другой коллайдер игроку (вы можете настроить это условие)
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = transform.position - collision.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-direction.normalized * springForce, ForceMode.Impulse);
            gameManager.AddScore(300);
            sound.pitch = Random.Range(0.9f, 1.1f);
            sound.Play();
            StartCoroutine(LightControl());
        }
    }
}
