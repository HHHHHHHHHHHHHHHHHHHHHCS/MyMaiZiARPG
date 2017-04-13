using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private float speed = 2;
    private Vector3 offestPos;
    private Transform mRoot;


    public void InitCamera(Transform _target)
    {
        mRoot = transform;
        target = _target;
        offestPos = transform.position - target.position;

    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
        mRoot.position = Vector3.Lerp(mRoot.position, target.position + offestPos, Time.deltaTime * speed);

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {//放大视角
            if (Camera.main.fieldOfView <= 65)
            {
                Camera.main.fieldOfView += 2.5f;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {//缩小视角
            if (Camera.main.fieldOfView >= 45)
            {
                Camera.main.fieldOfView -= 2.5f;
            }
        }
    }
}
