using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class ERun : State
    {
        private Vector3 m_Move;
        
        public ERun(GameEntity ge, string name) : base(ge, name) { }
        
        public override void OnEnter()
        {
            //m_GameEntity.Anim.SetBool("IsRun", true);
            m_GameEntity.Anim.CrossFade("BenPao");
        }

        // 0 move, 1 target
        public override void OnExcute(params object[] p)
        {
            m_Move = (Vector3)p[0];
            GameObject target = (GameObject)p[1];
            m_GameEntity.Move(m_Move);
            m_GameEntity.transform.LookAt(target.transform);
        }

        public override void OnExit()
        {
            //m_GameEntity.Anim.SetBool("IsRun", false);
        }
    }
}