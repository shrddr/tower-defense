using UnityEngine;
using UnityEngine.AI;

public class ClickToWalk : MonoBehaviour
{
    public Vector3 ShowDestination;
    private NavMeshAgent _navMeshAgent;

	void Start()
	{
	    _navMeshAgent = GetComponent<NavMeshAgent>();
	    _navMeshAgent.destination = transform.position;
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
	}
}
