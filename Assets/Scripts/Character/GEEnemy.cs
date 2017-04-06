using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class GEEnemy : GameEntity
    {
        private const string IDLE = "idle";
        private const string RUN = "run";
        private const string ATTACK = "attack";
        private const float MAXDISTANCE = 20;
        private AnimatorStateInfo m_Stateinfo;
        private GameObject m_Target;

        void Start()
        {
            InitData();
            m_Target = GameObject.FindGameObjectWithTag("Player");
            m_Animation = GetComponentInChildren<Animation>();
            m_FSM = new StateMachine();
            m_FSM.AddState(IDLE, new EIdle(this, IDLE));
            m_FSM.AddState(RUN, new ERun(this, RUN));
            m_FSM.AddState(ATTACK, new EAttack(this, ATTACK));
            m_FSM.Init(IDLE);
            m_UIStatus = UIMgr.Instance.GenStatusItem(this);
        }

        void OnEnable()
        {
            if (m_UIStatus != null) m_UIStatus.gameObject.SetActive(true); 
        }

        void OnDisable()
        {
            if (m_UIStatus != null) m_UIStatus.gameObject.SetActive(false);            
        }

        void Update()
        {
            float distance = Vector3.Distance(transform.position, m_Target.transform.position);
            if (distance < MAXDISTANCE && distance > AtkDistance)
            {
                if (m_FSM.IsInState(IDLE) || m_FSM.IsInState(ATTACK))
                {
                    if (!m_Animation.IsPlaying("PuTongGongJi_1") &&
                        !m_Animation.IsPlaying("PuTongGongJi_2"))
                        m_FSM.ChangeState(RUN);                
                }

            }
            else if (distance <= AtkDistance)
            {
                if (!m_FSM.IsInState(ATTACK))
                {
                    m_FSM.ChangeState(ATTACK);
                }
                transform.LookAt(m_Target.transform);
                m_FSM.Excute(m_Target);
            }
            else
            {
                if (!m_FSM.IsInState(IDLE))
                {
                    m_FSM.ChangeState(IDLE);
                }
            }
            if (m_FSM.IsInState(RUN))
            {
                Vector3 dir = m_Target.transform.position - transform.position;
                object[] param = new object[2] { dir, m_Target };
                m_FSM.Excute(param);
            }
        }


        protected override void InitData()
        {
            this.HP = 100;
            this.CurHP = this.HP;
            this.Atk = 8;
            this.AtkDistance = 5;
            this.m_MoveSpeed = 3f;
            HeadDistance = 10;
        }

        public override void ShowDamage(GameEntity attacker)
        {
            base.ShowDamage(attacker);
            this.CurHP -= attacker.Atk;
            m_UIStatus.UpdateHP(attacker.Atk, (float)this.CurHP / (float)this.HP);
        }

        public Transform GetHeadPoint()
        {
            return transform.FindChild("HeadPoint");
        }
    }
}
