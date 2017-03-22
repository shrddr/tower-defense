using Assets.Scripts;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject Target;
    public float Speed = 10;
    public float Damage = 5;
    public float Aoe = 0;

    private bool _damageDone;

    private void Start ()
    {

    }
	
	private void Update ()
	{
	    if (Target == null)
	        Destroy(gameObject);
	    else
	        MoveToTarget();
	}

    protected void OnTriggerEnter(Collider other)
    {
        if (_damageDone) return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            DetachEffects();
            Destroy(gameObject);

            if (Explosion != null)
                Instantiate(Explosion, transform.position, transform.rotation);

            if (Aoe < 0.001)
            {
                other.gameObject.GetComponent<EnemyController>().TakeDamage(Damage);
            }
            else
            {
                foreach (var enemy in TargetHelper.GetTargets(gameObject, Aoe))
                    enemy.gameObject.GetComponent<EnemyController>().TakeDamage(Damage);
            }

            _damageDone = true;
        }
    }

    private void MoveToTarget()
    {
        var step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
        var direction = Target.transform.position - transform.position;
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
    }

    private void DetachEffects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag("Effect"))
                child.parent = null;
        }
    }
}
