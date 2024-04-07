using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected Material newMaterial;
    protected Material mainMaterial;
    protected GameManager gameManager;
    protected AudioSource sound;

    private void Awake()
    {
        mainMaterial = GetComponent<Renderer>().material;
        gameManager = FindObjectOfType<GameManager>();
        sound = GetComponentInChildren<AudioSource>();
    }

    protected void TurnOnLight()
    {
        GetComponent<Renderer>().material = newMaterial;
        GetComponent<Light>().enabled = true;
    }
    protected void TurnOffLight()
    {
        GetComponent<Renderer>().material = mainMaterial;
        GetComponent<Light>().enabled = false;
    }

    protected IEnumerator LightControl()
    {
        TurnOnLight();
        yield return new WaitForSeconds(0.5f);
        TurnOffLight();
    }
}
