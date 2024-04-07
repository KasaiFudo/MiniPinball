using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringObstacle : Obstacle
{
    public float springForce = 10f;

    // ����������, ����� ������ ��������� ������ � ���� ��������
    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ����������� �� ������ ��������� ������ (�� ������ ��������� ��� �������)
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
