using Unity.VisualScripting;
using UnityEngine;

public class TargetCatcher : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource sound;

    private void Start()
    {
        gameManager = GameManager.Instance;
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
            sound.Play();
            Destroy(gameObject, sound.clip.length);
        }
    }
}
