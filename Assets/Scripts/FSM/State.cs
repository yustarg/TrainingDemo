﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public abstract class State 
    {
        public string Name { get; set; }
        public GameEntity m_GameEntity;
        public State(GameEntity ge, string name) {
            m_GameEntity = ge;
            Name = name; 
        }
        public abstract void OnEnter();
        public abstract void OnExcute(params object[] p);
        public abstract void OnExit();

    }
}
