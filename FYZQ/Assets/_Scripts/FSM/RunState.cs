using UnityEngine;
using System.Collections;

/// <summary>
/// Run状态
/// </summary>

public class RunState : StateTemplate<PlayerController>
{

    public RunState(int id, PlayerController p) : base(id, p)
    {
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        owner.ani.Play("Run");
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
    }
}
