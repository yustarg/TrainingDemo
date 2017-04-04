using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Training
{
    public class GEEnemy : GameEntity
    {
        AnimatorStateInfo m_Stateinfo;
        void Start()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_FSM = new StateMachine();
            m_FSM.AddState("idle", new Idle(this));
            m_FSM.AddState("run", new Run(this));
            m_FSM.AddState("attack", new Attack(this));
            m_FSM.Init("idle");
        }

        void Update()
        {
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //float v = CrossPlatformInputManager.GetAxis("Vertical");
            //if (h != 0 || v != 0)
            //{
            //    m_Stateinfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            //    if (!m_Stateinfo.IsName("Base Layer.Attack1")
            //        && !m_Stateinfo.IsName("Base Layer.Attack2")
            //        && !m_Stateinfo.IsName("Base Layer.Attack3"))
            //    {
            //        m_FSM.ChangeState("run");
            //        object[] param = new object[2] { h, v };
            //        m_FSM.Excute(param);
            //    }
            //}
            //else
            //{
            //    if (Input.GetButtonDown("Fire1"))
            //    {
            //        m_FSM.ChangeState("attack");
            //    }
            //    else
            //    {
            //        m_FSM.ChangeState("idle");
            //    }
            //}
        }
    }
}
