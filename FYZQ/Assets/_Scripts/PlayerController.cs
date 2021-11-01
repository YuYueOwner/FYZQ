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
    float speedX = 2f, speedY = 2f;
    //观察变化量
    float rotationX, rotationY;

    void Start()
    {
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

    void Update()
    {
        PlayerMoveByKey();
        PlayerRotateByKey();

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            rotationX = this.transform.eulerAngles.y;
        }

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
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        characterController.SimpleMove(forward * moveSpeed);
    }

    /// <summary>键盘控制前后移动</summary>
    private void PlayerMoveByKey()
    {
        if (Input.GetKey("w") || Input.GetKey("s"))
        {
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            float curSpeed = moveSpeed * Input.GetAxis("Vertical");
            characterController.SimpleMove(forward * curSpeed);
        }
    }
    #endregion


    #region 控制旋转
    /// <summary> 鼠标控制视角左右旋转 </summary>
    private void PlayerRotateByMouse()
    {
        rotationX += Input.GetAxis("Mouse X") * speedX;
        rotationY += Input.GetAxis("Mouse Y") * speedY;

        this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, Tools.ClampAngle(rotationX), 0);
        Center.transform.localEulerAngles = new Vector3(Tools.ClampAngle(-rotationY, -40, 90), Center.localEulerAngles.y, Center.localEulerAngles.z);
        //Debug.LogError("rotationX:" + rotationX + "       rotationY:" + rotationY);
    }

    /// <summary> 键盘AD控制左右旋转 </summary>
    private void PlayerRotateByKey()
    {
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            rotationX += Input.GetAxis("Horizontal") * speedX;
            //  transform.Rotate(new Vector3(0, h, 0) * Time.deltaTime);
            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, rotationX, 0);
        }
    }
    #endregion
}
