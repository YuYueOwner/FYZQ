/// <summary>
/// Idle状态
/// </summary>
public class IdleState : StateTemplate<PlayerController>
{

    public IdleState(int id, PlayerController p) : base(id, p)
    {
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        owner.ani.Play("Idle");
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
