using Assets.Scripts;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public int GoldPrice = 10;
    public float TargetSeekRange = 5;
    private GameObject _target;

    // Use this for initialization
    private	void Start ()
    {
		
	}

    // Update is called once per frame
    private void Update ()
    {
        if (_target != null)
        {
            var newRotation = Quaternion.LookRotation(transform.position - _target.transform.position);
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
        }

        if (_target != null)
            if (!TargetHelper.TargetIsInRange(gameObject, _target, TargetSeekRange))
                _target = null;

        if (_target == null)
            _target = TargetHelper.GetNearestTargetInRange(gameObject, TargetSeekRange);
    }
}
