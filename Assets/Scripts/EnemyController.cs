using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int Worth;
    public float MaxHP;

    private LevelController _levelController;
    private NavMeshAgent _navMeshAgent;
    private float _hp;
    private bool _dead;

    void Start()
    {
        _levelController = GameObject.FindWithTag("LevelController").GetComponent<LevelController>();

        var target = GameObject.Find("EnemyTarget").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(target.position);

        _hp = MaxHP;
    }

    public void TakeDamage(float amount)
    {
        if (_dead) return;
        _hp -= amount;
        if (_hp <= 0) Die();
    }

    private void Die()
    {
        _dead = true;
        _levelController.UpdateGold(Worth);
        Destroy(gameObject);
    }
}
