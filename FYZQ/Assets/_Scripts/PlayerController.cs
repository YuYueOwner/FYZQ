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
    [HideInInspector]
    public Animator ani;
    public PlayerState ps = PlayerState.Idle;
    //控制机器

    #region move
    public StateMachine machine;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    #endregion

    void Start()
    {
        ani = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        //IdleState idle = new IdleState(1, this);
        //RunState run = new RunState(2, this);
        //AttackState attack = new AttackState(3, this);

        //machine = new StateMachine(idle);
        //machine.AddState(run);
        //machine.AddState(attack);
    }
    private float recordCurrentYangel = 0;
    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            recordCurrentYangel = this.transform.eulerAngles.y;
        }

        if (Input.GetMouseButton(1))
        {
            CameraView();
        }

        //鼠标左键按住行走
        if (Input.GetMouseButton(0))
        {
            CameraView();
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            characterController.SimpleMove(forward * speed);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    ps = PlayerState.Attack;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    ps = PlayerState.Run;
        //}
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    ps = PlayerState.Idle;
        //}

        //根据枚举 让状态机器类去切换状态
        //  UpdateAnimation();
    }


    //前后移动
    private void Move()
    {
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        characterController.SimpleMove(forward * curSpeed);
        float h = 50 * Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, h, 0) * Time.deltaTime);　//左右旋转
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

    /// <summary> 相机视角 </summary>
    private void CameraView()
    {
        CameraControl._instance.PlayerVerticleRotate(this.transform);
    }
}
