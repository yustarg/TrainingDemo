using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class Run : State
    {
        private Vector3 m_Move;
        
        public Run(GameEntity ge, string name) : base(ge, name) { }
        
        public override void OnEnter()
        {
            //m_GameEntity.Anim.SetBool("IsRun", true);
            m_GameEntity.Anim.CrossFade("BenPao_TY");
        }

        public override void OnExcute(params object[] p)
        {
            m_Move = (Vector3)p[0];
            m_GameEntity.Move(m_Move);
        }

        public override void OnExit()
        {
            //m_GameEntity.Anim.SetBool("IsRun", false);
        }
    }
}