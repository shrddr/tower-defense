using Assets.Scripts;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject Target;
    public float Speed = 10;
    public float Damage = 5;
    public float Aoe = 0;

    private bool _done;

    // Use this for initialization
    private void Start ()
    {
    
	}
	
	// Update is called once per frame
	private void Update ()
	{
	    if (Target == null)
	        Destroy(gameObject);
	    else
	        MoveToTarget();
	}


    protected void OnTriggerEnter(Collider other)
    {
        if (_done) return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            _done = true;
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

        }
    }

    private void MoveToTarget()
    {
        var step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
        transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
    }
}
