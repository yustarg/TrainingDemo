using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class Idle : State
    {

        public Idle(GameEntity ge, string name) : base(ge, name) { }
        public override void OnEnter()
        {
            m_GameEntity.Anim.CrossFade("ZhanLi_TY");
        }

        public override void OnExcute(params object[] p)
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}