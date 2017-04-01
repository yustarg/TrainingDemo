using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public abstract class State 
    {
        public GameEntity m_GameEntity;
        public State(GameEntity ge) {
            m_GameEntity = ge;
        }
        public abstract void OnEnter();
        public abstract void OnExcute(params object[] p);
        public abstract void OnExit();

    }
}
