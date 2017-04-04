using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Training
{
    public class GEPlayer : GameEntity
    {
        AnimatorStateInfo m_Stateinfo;
        private Transform m_MainCam;
        private Transform m_Target;
        private Vector3 m_Dir;
        private Vector3 m_Right;
        private bool m_IsFirstInput;
        // Use this for initialization
        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_MainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
            m_Target = GameObject.FindGameObjectWithTag("Player").transform; m_FSM = new StateMachine();
            m_FSM.AddState("idle", new Idle(this));
            m_FSM.AddState("run", new Run(this));
            m_FSM.AddState("attack", new Attack(this));
            m_FSM.Init("idle");
            m_IsFirstInput = true;
        }

        void Update()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            if (h != 0 || v != 0)
            {
                m_Stateinfo = m_Animator.GetCurrentAnimatorStateInfo(0);
                if (!m_Stateinfo.IsName("Base Layer.Attack1") 
                    && !m_Stateinfo.IsName("Base Layer.Attack2")
                    && !m_Stateinfo.IsName("Base Layer.Attack3"))
                {    
                    m_FSM.ChangeState("run");
                    if (m_IsFirstInput)
                    {
                        m_Dir = m_Target.position - m_MainCam.position;
                        m_Right = Vector3.Cross(Vector3.up, m_Dir);
                    }
                    m_IsFirstInput = false;
                    object[] param = new object[1] { v * new Vector3(m_Dir.x, 0, m_Dir.z) + h * m_Right };
                    m_FSM.Excute(param);
                }
            }
            else
            {
                m_IsFirstInput = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    m_FSM.ChangeState("attack");
                }
                else
                {
                    m_FSM.ChangeState("idle");
                }
            }
        }


    }
}