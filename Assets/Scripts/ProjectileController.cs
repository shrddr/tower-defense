using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject Target;
    public float Speed = 10;

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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
           // Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void MoveToTarget()
    {
        var step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
    }
}
