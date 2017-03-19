using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public partial class GunController : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject Target;

    public float AttackRange = 4;
    public float AttackDelay = 5f;
    public float ProjectileSpeed = 3f;
    public float DamagePerProjectile = 3;
    public int ProjectilesPerAttack = 1;
    public float ProjectileChainAttackDelay = 1f;

    private float _attackCooldown = 0;
    private readonly List<ChainAttackData> _delayedAttacks = new List<ChainAttackData>();
    private AudioSource _shootSound;

    private void Start ()
    {
        _shootSound = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        _attackCooldown -= Time.deltaTime;

        ManageDelayedAttacks();

        if (AttackIsReady())
        {
            if (Target != null)
                if (!TargetHelper.TargetIsInRange(gameObject, Target, AttackRange))
                    Target = null;

            if (Target == null)
                Target = TargetHelper.TryToGetTarget(gameObject, AttackRange);

            if (Target != null)
                Attack();
        }
    }

    private void ManageDelayedAttacks()
    {
        foreach (var chainAttackData in _delayedAttacks)
        {
            chainAttackData.Delay -= Time.deltaTime;
            if(chainAttackData.Delay <= 0)
                FireProjectile(chainAttackData.Target);
        }
        _delayedAttacks.RemoveAll(data => data.Delay <= 0);
    }

    private void Attack()
    {
        _attackCooldown = AttackDelay;

        if(ProjectilesPerAttack == 1)
            FireProjectile(Target);
        if (ProjectilesPerAttack > 1)
        {
            FireProjectile(Target);
            for (var attackNumber = 1; attackNumber < ProjectilesPerAttack; attackNumber++)          
                _delayedAttacks.Add(new ChainAttackData(Target, ProjectileChainAttackDelay * attackNumber));          
        }
        _shootSound.Play();
    }

    private void FireProjectile(GameObject target)
    {
        var projectile = Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
        var projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.Target = target;
        projectileController.Speed = ProjectileSpeed;
        projectileController.Damage = DamagePerProjectile;
    }

    private bool AttackIsReady()
    {
        return _attackCooldown <= 0;
    }
}
