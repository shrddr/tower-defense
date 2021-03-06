using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public partial class GunController : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject ProjectileEffect;
    public GameObject Target;

    public float AttackRange = 4;
    public float AttackDelay = 5f;
    public float ProjectileSpeed = 3f;
    public float DamagePerProjectile = 3;
    public int ProjectilesPerAttack = 1;
    public float ProjectileChainAttackDelay = 1f;

    private StatDataContainer _stats = new StatDataContainer();

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
                Target = TargetHelper.GetNearestTargetInRange(gameObject, AttackRange);

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
        var projectile = Instantiate(Projectile, transform.position, transform.rotation);
        var projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.Target = target;
        projectileController.Speed = ProjectileSpeed;
        projectileController.Damage = DamagePerProjectile;

        if (ProjectileEffect != null)
        {
            var projectileEffect = Instantiate(ProjectileEffect, transform.position, transform.rotation);
            projectileEffect.transform.SetParent(projectile.transform);
        }

    }

    private bool AttackIsReady()
    {
        return _attackCooldown <= 0;
    }
}
