using System.Linq;
using UnityEngine;

public class Tower1Controller : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject Target;
    public float AttackRange = 4;
    public float AttackDelay = 5f;
    public float ProjectileSpeed = 3f;
    public float Damage = 3;
    public AudioSource Audio;

    private float _attackCooldown = 0;
    
    private void Start ()
    {
        
    }

    private void Update()
    {
        _attackCooldown -= Time.deltaTime;

        if (AttackIsReady())
        {
            if (Target != null)
                if (!TargetIsInRange())
                    Target = null;

            if (Target == null)
                TryToGetTarget();

            if (Target != null)
                Attack();
        }

        if (Target != null)
        {
            transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        }
    }

    private bool TargetIsInRange()
    {
        var distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);
        return distance <= AttackRange;
    }

    private void Attack()
    {
        _attackCooldown = AttackDelay;
        var projectile = Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
        var projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.Target = Target;
        projectileController.Speed = ProjectileSpeed;
        projectileController.Damage = Damage;
        Audio.Play();
    }

    private bool AttackIsReady()
    {
        return _attackCooldown <= 0;
    }

    private void TryToGetTarget()
    {
        var hitColliders = Physics.OverlapSphere(gameObject.transform.position, AttackRange);
        var enemies = hitColliders.Where(col => col.CompareTag("Enemy")).ToList();
        if (enemies.Any())        
            Target = enemies.OrderBy(en => Vector3.Distance(transform.position, en.transform.position))
                .First()
                .gameObject;
    }
}
