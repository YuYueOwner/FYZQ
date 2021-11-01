using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CameraControl : MonoBehaviour
{
    public static CameraControl _instance;
    //视野转动速度
    float speedX = 5f;
    float speedY = 5f;
    //上下观察范围
    float minY = -60;
    float maxY = 60;
    //观察变化量
    float rotationX;
    float rotationY;

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        //rotationY += Input.GetAxis("Mouse Y") * speedY;
        //rotationY = Mathf.Clamp(rotationY, minY, maxY);
        //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }

    public void PlayerVerticleRotate(Transform tf)
    {
        rotationX += Input.GetAxis("Mouse X") * speedX;
        if (rotationX < 0)
        {
            rotationX += 360;
        }
        if (rotationX > 360)
        {
            rotationX -= 360;
        }
        tf.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }


    private float localPosZ = -10;

    public Transform center;

    public void LerpLocalPosion()
    {
        rotationX += Input.GetAxis("Mouse X") * speedX;
        if (rotationX < 0)
        {
            rotationX += 360;
        }
        if (rotationX > 360)
        {
            rotationX -= 360;
        }
        tf.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }




}
