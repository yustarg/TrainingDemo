using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class Run : State
    {
        private Vector3 m_Move;
        public Run(GameEntity ge) : base(ge) { }
        
        public override void OnEnter()
        {
            m_GameEntity.Anim.SetBool("IsRun", true);
        }

        public override void OnExcute(params object[] p)
        {
            m_Move = (float)p[1] * Vector3.forward + (float)p[0] * Vector3.right;
            m_GameEntity.Move(m_Move);
        }

        public override void OnExit()
        {
            m_GameEntity.Anim.SetBool("IsRun", false);
        }
    }
}