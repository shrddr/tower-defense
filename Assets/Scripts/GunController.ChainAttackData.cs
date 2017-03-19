using UnityEngine;

public partial class GunController
{
	private sealed class ChainAttackData
	{
	    public ChainAttackData(GameObject target, float delay)
	    {
	        Target = target;
	        Delay = delay;
	    }

        public GameObject Target { get; private set; }
		public float Delay { get; set; }
	}
}

