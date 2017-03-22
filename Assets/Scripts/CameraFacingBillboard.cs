using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    void Start()
    {
        Update();
    }

    void Update()
	{
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}
