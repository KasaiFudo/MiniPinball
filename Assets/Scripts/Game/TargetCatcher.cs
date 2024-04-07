using Unity.VisualScripting;
using UnityEngine;

public class TargetCatcher : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource sound;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sound = GetComponentInChildren<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<Light>().enabled = false;
            gameManager.CatchTheTarget();
            gameManager.AddScore(1000);
            sound.Play();
            Destroy(gameObject, sound.clip.length);
        }
    }
}
