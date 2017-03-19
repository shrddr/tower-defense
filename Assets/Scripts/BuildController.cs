using UnityEngine;

public class BuildController : MonoBehaviour
{
    private BuildManager _buildManager;
    private Behaviour _halo;

    // Use this for initialization
	private void Start ()
	{
	    _buildManager = BuildManager.Instance;
        _halo = (Behaviour)gameObject.GetComponent("Halo");
	    _halo.enabled = false;
	}

    // Update is called once per frame
    private void Update ()
    {
		
	}

    private void OnMouseDown()
    {
        _halo.enabled = true;
        _buildManager.OpenBuildMenu(this);
    }

    public void Deactivate()
    {
        _halo.enabled = false;
    }
}
