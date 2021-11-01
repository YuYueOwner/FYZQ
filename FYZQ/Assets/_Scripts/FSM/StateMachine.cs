using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 状态机器类：由Player控制。完成状态的存储，切换，和状态的保持
/// </summary>

public class StateMachine
{
    //用来存储当前机器所控制的所有状态
    public Dictionary<int, StateBase> m_StateCache;

    //定义上一个状态
    public StateBase m_prviousState;
    //定义当前状态
    public StateBase m_currentState;

    //机器初始化时，没有上一个状态
    public StateMachine(StateBase beginState)
    {
        m_prviousState = null;
        m_currentState = beginState;

        m_StateCache = new Dictionary<int, StateBase>();
        //把状态添加到集合中
        AddState(beginState);
        m_currentState.OnEnter();
    }

    public void AddState(StateBase state)
    {
        if (!m_StateCache.ContainsKey(state.ID))
        {
            m_StateCache.Add(state.ID, state);
            state.machine = this;
        }
    }

    //通过Id来切换状态
    public void TranslateState(int id)
    {
        if (!m_StateCache.ContainsKey(id))
        {
            return;
        }

        m_prviousState = m_currentState;
        m_currentState = m_StateCache[id];
        m_currentState.OnEnter();
    }

    //状态保持
    public void Update()
    {
        if (m_currentState != null)
        {
            m_currentState.OnStay();
        }
    }
}
