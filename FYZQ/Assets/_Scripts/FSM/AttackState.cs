using UnityEngine;
using System.Collections;

/// <summary>
/// Attack状态
/// </summary>
public class AttackState : StateTemplate<PlayerController>
{

    public AttackState(int id, PlayerController p) : base(id, p)
    {
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        owner.ani.Play("Attack");
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
        if (!owner.ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {

            OnExit();
        }
    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
        owner.ps = PlayerState.Idle;
    }
}
