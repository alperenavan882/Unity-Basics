using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.3f;
    public Vector3 offset;

    void LateUpdate()
    {
        //Taking and transferring position datas
        Vector3 desiredPosition = target.position + offset;

        //Transferring another transform data but there is also a calculation 'smoothSpeed * Time.deltaTime'
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        //Transferring the last transform data to the camera position
        transform.position = smoothedPosition;

    }

}
