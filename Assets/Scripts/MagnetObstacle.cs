using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MagnetObstacle : Obstacle
{
    [SerializeField] private float force = 1f;
    [SerializeField] private float diametr = 20f;
    [SerializeField] private int duration = 5;
    [SerializeField] private int cooldown = 10;

    private bool isMagnetActive = false;
    //private int numPoints = 50;
    //private LineRenderer lineRenderer;

    private void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(ActivateMagnetCoroutine());
        //DrawRadius();
    }

    private void FixedUpdate()
    {
        if (isMagnetActive)
        {
            foreach (var collider in Physics.OverlapSphere(transform.position, diametr))
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = transform.position - rb.transform.position;
                    float distance = forceDirection.magnitude;
                    float force = this.force / distance;
                    rb.AddForce(forceDirection.normalized * force, ForceMode.Acceleration);
                    Debug.Log(collider.gameObject.name);
                }
            }
        }
    }
    //private void DrawRadius()
    //{
    //    lineRenderer.positionCount = numPoints;
    //    Vector3 tempPoint = new Vector3();
    //    for (int i = 0; i < numPoints; i++)
    //    {
    //        float angle = i * 2f * Mathf.PI / numPoints;
    //        float x = (diametr / 2) * Mathf.Cos(angle);
    //        float y = (diametr / 2) * Mathf.Sin(angle);

    //        Vector3 point = transform.TransformPoint(new Vector3(x, y, 0f));
    //        if (point.x < 10f && point.x > -10f && point.y < 10 && point.y > -10)
    //        {
    //            lineRenderer.SetPosition(i, point);
    //            tempPoint = point;
    //        }
    //        else
    //        {
    //            lineRenderer.SetPosition(i, tempPoint);
    //        }
    //    }
    //    // Замкнуть окружность
    //    //lineRenderer.loop = true;
    //}
    private IEnumerator ActivateMagnetCoroutine()
    {
        while (true)
        {
            isMagnetActive = true;
            TurnOnLight();
            sound.Play();
            yield return new WaitForSeconds(duration);
            isMagnetActive = false;
            TurnOffLight();
            yield return new WaitForSeconds(cooldown);
        }
    }

}
