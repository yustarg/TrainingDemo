using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class StateMachine
    {
        private Dictionary<string, State> m_States;
        public State m_CurState;

        public StateMachine()
        {
            m_States = new Dictionary<string, State>();
        }

        public void AddState(string name, State state)
        {
            if (!m_States.ContainsKey(name))
            {
                m_States.Add(name, state);
            }
        }

        public void RemoveState(string name)
        {
            if (m_States.ContainsKey(name))
            {
                m_States.Remove(name);
            }
        }

        public void ChangeState(string name)
        {
            if (m_States.ContainsKey(name))
            {
                m_CurState.OnExit();
                State s = m_States[name];
                s.OnEnter();
                m_CurState = s;
            }
        }

        public bool IsInState(string name)
        {
            return m_CurState.Name == name;
        }

        public void Init(string name)
        {
            if (m_States.ContainsKey(name))
            {
                m_CurState = m_States[name];
                m_CurState.OnEnter();
            }
        }

        public void Excute(params object[] p)
        {
            m_CurState.OnExcute(p);
        }
    }
}