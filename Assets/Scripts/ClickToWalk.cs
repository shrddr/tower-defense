using UnityEngine;
using UnityEngine.AI;

public class ClickToWalk : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

	void Start()
	{
	    _navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update()
	{
	    if (Input.GetMouseButton(0))
	    {
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        RaycastHit hit;

	        if (Physics.Raycast(ray, out hit))
	        {
	            _navMeshAgent.SetDestination(hit.point);
	        }
	    }

        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Enemy"))
            {
                var enemyController = hit.collider.gameObject.GetComponent<EnemyController>();
                enemyController.TakeDamage(1);
            }
        }
    }
}
