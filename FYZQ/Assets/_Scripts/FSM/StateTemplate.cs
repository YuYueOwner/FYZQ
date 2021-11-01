/// <summary>
/// 状态拥有者
/// </summary>
public class StateTemplate<T> : StateBase
{

    public T owner;   //拥有者(范型)

    public StateTemplate(int id, T o) : base(id)
    {
        owner = o;
    }
}
