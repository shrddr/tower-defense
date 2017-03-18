using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;
    public int EnemyCount;
    public float SpawnDelay;
    public float SpawnInterval;
    public bool Finished;

    private int _enemiesToSpawn;

    void Start()
    {
        _enemiesToSpawn = EnemyCount;
        InvokeRepeating("Spawn", SpawnDelay, SpawnInterval);
    }

    void Spawn()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
        _enemiesToSpawn--;
    }

    void Update()
    {
        if (_enemiesToSpawn == 0)
        {
            CancelInvoke();
            Finished = true;
        }       
    }
}
