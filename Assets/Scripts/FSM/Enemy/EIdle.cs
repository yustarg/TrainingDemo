using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class EIdle : State
    {

        public EIdle(GameEntity ge, string name) : base(ge, name) { }
        public override void OnEnter()
        {
            m_GameEntity.Anim.CrossFade("ZhanLi");
        }

        public override void OnExcute(params object[] p)
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}