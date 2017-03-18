using UnityEngine;

public class Despawner : MonoBehaviour
{
    public LevelController LevelController;

    private AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        LevelController.UpdateLives(-1);
        _audio.Play();
    }
}
