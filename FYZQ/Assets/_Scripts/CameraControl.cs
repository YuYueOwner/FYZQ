using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CameraControl : MonoBehaviour
{
    public static CameraControl _instance;
    //相机距离人物高度
    public float m_Height = 5f;
    //相机距离人物距离
    public float m_Distance = 5f;
    //相机跟随速度
    public float m_Speed = 4f;
    //目标位置
    Vector3 m_TargetPosition;
    //要跟随的人物
    public Transform follow;
    [HideInInspector]
    public float distance = 0;

    private void Awake()
    {
        _instance = this;
    }


    //相机平滑的跟随人物移动
    void LateUpdate()
    {
        //   distance = Mathf.Clamp(distance, 2, 18);
        // Debug.LogError("Hou" + distance);
        //得到这个目标位置
        m_TargetPosition = follow.position + Vector3.up * m_Height - follow.forward * m_Distance;
        //相机位置
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, m_Speed * Time.deltaTime);
        //相机时刻看着人物
        transform.LookAt(follow);
        
    }
}
