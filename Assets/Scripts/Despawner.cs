using UnityEngine;

public class Despawner : MonoBehaviour
{
    public LevelController LevelController;

    public AudioSource Audio;

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        LevelController.UpdateLives(-1);
        Audio.Play();
    }
}
