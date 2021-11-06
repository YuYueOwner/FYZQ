using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    None,
    Idle,
    Run,
    Attack
}

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [HideInInspector]
    public Animator ani;
    public PlayerState ps = PlayerState.Idle;
    //控制机器
    public StateMachine machine;
    private Transform Center;
    //移动速度
    public float moveSpeed = 6.0f;
    //视野转动速度
    float speedX = 0.5f, speedY = 2f;
    //观察变化量
    float rotationX, rotationY;

    void Start()
    {
        targetPoint = transform.position;
        ani = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Center = Tools.GetChild<Transform>(this.transform, "Center");
        //IdleState idle = new IdleState(1, this);
        //RunState run = new RunState(2, this);
        //AttackState attack = new AttackState(3, this);

        //machine = new StateMachine(idle);
        //machine.AddState(run);
        //machine.AddState(attack);
    }
    private float time = 0.2f;
    Vector3 targetPoint = Vector3.zero;     //鼠标点击的位置
    private bool isMove = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            rotationX = this.transform.eulerAngles.y;
        }


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //向鼠标点击的位置发射一条射线 && 射线检测到的物体是当前挂着该脚本的物体
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                //双击
                if ((Time.realtimeSinceStartup - time) < 0.2f)
                {
                   
                }
                else
                {
                    time = Time.realtimeSinceStartup;
                }
            }
        }
        PlayerMoveByKey();
        PlayerRotateByKey();


        if (Input.GetMouseButton(1))
        {
            PlayerRotateByMouse();
        }

        //鼠标左键按住可行走可旋转
        if (Input.GetMouseButton(0))
        {
            //旋转
            PlayerRotateByMouse();
            //往前走
            PlayerMoveByMouse();
        }
        //if (Input.GetMouseButtonDown(0))
        //    ps = PlayerState.Attack;
        //if (Input.GetKey(KeyCode.A))
        //    ps = PlayerState.Run;
        //if (Input.GetKeyUp(KeyCode.A))
        //    ps = PlayerState.Idle;

        //根据枚举 让状态机器类去切换状态
        //  UpdateAnimation();
    }


    private void UpdateAnimation()
    {
        switch (ps)
        {
            case PlayerState.Idle:
                machine.TranslateState(1);
                break;
            case PlayerState.Run:
                machine.TranslateState(2);
                break;
            case PlayerState.Attack:
                machine.TranslateState(3);
                break;
        }
    }

    void LateUpdate()
    {
        // machine.Update();
    }


    #region 控制移动
    /// <summary>鼠标控制玩家往前移动</summary>
    private void PlayerMoveByMouse()
    {
        if (Input.GetKey("s")) return;
        characterController.SimpleMove(transform.forward * moveSpeed);
        ani.SetBool("isWalk", true);
    }

    bool isWalk = false;
    bool isWalkBack = false;

    /// <summary>键盘控制前后移动</summary>
    private void PlayerMoveByKey()
    {
        if (Input.GetKey("w"))
        {
            isWalk = true;
            ani.SetBool("isWalk", true);
            float curSpeed = moveSpeed * Input.GetAxis("Vertical");
            characterController.SimpleMove(transform.forward * curSpeed);
        }
        else
        {
            isWalk = false;
            ani.SetBool("isWalk", false);
        }

        if (isWalk) return;

        if (Input.GetKey("s"))
        {
            ani.SetBool("isBack", true);
            float curSpeed = moveSpeed * Input.GetAxis("Vertical");
            characterController.SimpleMove(transform.forward * curSpeed);
        }
        else
        {
            ani.SetBool("isBack", false);
        }
    }
    #endregion


    #region 控制旋转
    /// <summary> 鼠标控制视角左右旋转 </summary>
    private void PlayerRotateByMouse()
    {
        rotationX += Input.GetAxis("Mouse X") * speedX;
        rotationY += Input.GetAxis("Mouse Y") * speedY;
        CameraControl._instance.distance += Input.GetAxis("Mouse Y") * Time.deltaTime * 2;

        this.transform.eulerAngles = new Vector3(this.transform.localEulerAngles.x, Tools.ClampAngle(rotationX), 0);
        Center.transform.localRotation = Quaternion.Euler(new Vector3(Tools.ClampAngle(-rotationY, -40, 85), Center.localEulerAngles.y, Center.localEulerAngles.z));
    }

    /// <summary> 键盘AD控制左右旋转 </summary>
    private void PlayerRotateByKey()
    {
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            rotationX += Input.GetAxis("Horizontal") * speedX;
            this.transform.localRotation = Quaternion.Euler(new Vector3(this.transform.localEulerAngles.x, rotationX, 0));
        }
    }
    #endregion
}
