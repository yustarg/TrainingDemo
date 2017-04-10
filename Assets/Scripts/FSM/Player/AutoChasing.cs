using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class AutoChasing : State
    {
        private Vector3 m_Move;

        public AutoChasing(GameEntity ge, string name) : base(ge, name) { }
        
        public override void OnEnter()
        {
            //m_GameEntity.Anim.SetBool("IsRun", true);
            m_GameEntity.Agent.enabled = true;
            m_GameEntity.Anim.CrossFade("BenPao_TY");
        }

        public override void OnExcute(params object[] p)
        {
            
        }

        public override void OnExit()
        {
            m_GameEntity.Agent.enabled = false;
        }
    }
}