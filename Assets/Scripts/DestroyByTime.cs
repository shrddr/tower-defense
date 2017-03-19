using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float LifeTime;

	void Start()
	{
		Destroy(gameObject, LifeTime);
	}
}
