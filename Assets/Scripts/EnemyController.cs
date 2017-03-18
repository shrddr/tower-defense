using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public int Worth;
    public float MaxHP;

    private LevelController _levelController;
    private NavMeshAgent _navMeshAgent;
    private Image _healthBar;
    private float _hp;
    private bool _dead;

    void Start()
    {
        _levelController = GameObject.FindWithTag("LevelController").GetComponent<LevelController>();

        var target = GameObject.Find("EnemyTarget").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(target.position);

        _hp = MaxHP;
        _healthBar = transform.FindChild("EnemyCanvas").FindChild("HealthBG").FindChild("Health").GetComponent<Image>();
    }

    public void TakeDamage(float amount)
    {
        if (_dead) return;
        _hp -= amount;
        _healthBar.fillAmount = _hp / MaxHP;
        if (_hp < 0) Die();
    }

    private void Die()
    {
        _dead = true;
        _levelController.UpdateGold(Worth);
        Destroy(gameObject);
    }
}
