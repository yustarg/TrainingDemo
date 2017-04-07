using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class GEPlayer : GameEntity
    {
        public float Height { get; set; }
        
        private const string IDLE = "idle";
        private const string RUN = "run";
        private const string ATTACK = "attack";
        private Transform m_MainCam;
        private Transform m_Target;
        private Vector3 m_Dir;
        private Vector3 m_Right;
        private bool m_IsFirstInput;
        private bool m_IsAutoChasing;
        private GameObject m_EnemyTarget;

        void Awake()
        {
            Height = GetComponent<CapsuleCollider>().height;        
        }

        // Use this for initialization
        void Start()
        {
            this.InitData(); 
            m_Animation = GetComponent<Animation>();
            m_MainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
            m_Target = GameObject.FindGameObjectWithTag("Player").transform; 
            m_FSM = new StateMachine();
            m_FSM.AddState(IDLE, new Idle(this, IDLE));
            m_FSM.AddState(RUN, new Run(this, RUN));
            m_FSM.AddState(ATTACK, new Attack(this, ATTACK));
            m_FSM.Init(IDLE);
            m_IsFirstInput = true;
            m_UIStatus = UIMgr.Instance.GenStatusItem(this);
        }

        void OnEnable()
        {
            EasyTouch.On_SimpleTap += On_SimpleTap;
        }

        void OnDisable()
        {
            UnsubscribeEvent();
        }

        void UnsubscribeEvent()
        {
            EasyTouch.On_SimpleTap -= On_SimpleTap;
        }

        private void On_SimpleTap(Gesture gesture)
        {
            if (!m_FSM.IsInState(ATTACK))
            {
                m_FSM.ChangeState(ATTACK);
            }
        }

        void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (h != 0 || v != 0)
            {
                if (IsNotPlayingAttackAnim())
                {
                    m_FSM.ChangeState(RUN);
                    //if (m_IsFirstInput)
                    //{
                        m_Dir = m_Target.position - m_MainCam.position;
                        m_Right = Vector3.Cross(Vector3.up, m_Dir);
                    //}
                    m_IsFirstInput = false;
                    object[] param = new object[1] { v * new Vector3(m_Dir.x, 0, m_Dir.z) + h * m_Right };
                    m_FSM.Excute(param);
                }
            }
            else if (m_IsAutoChasing)
            {
                float distance = Vector3.Distance(m_EnemyTarget.transform.position, m_Target.transform.position);
                if (distance <= AtkDistance)
                {
                    if (IsNotPlayingAttackAnim())
                    {
                        m_FSM.ChangeState(ATTACK);                    
                    }
                    //m_IsAutoChasing = false;
                }
                else 
                {
                    m_FSM.ChangeState(RUN);
                    m_Dir = m_EnemyTarget.transform.position - m_Target.position;
                    object[] param = new object[1] { new Vector3(m_Dir.x, 0, m_Dir.z) };
                    m_FSM.Excute(param);
                }     
            }
            else
            {
                //m_IsFirstInput = true;
                if (IsNotPlayingAttackAnim())
                {
                    m_FSM.ChangeState(IDLE);
                }
            }
        }

        private bool IsNotPlayingAttackAnim()
        {
            return (!m_Animation.IsPlaying("PuTongGongJi1_JJ") &&
                        !m_Animation.IsPlaying("PuTongGongJi2_JJ") &&
                        !m_Animation.IsPlaying("PuTongGongJi3_JJ"));
        }

        protected override void InitData()
        {
            this.HP = 100;
            this.CurHP = this.HP;
            this.Atk = 10;
            this.AtkDistance = 5;
            HeadDistance = 3;
            Name = "阿余";
        }

        // send message
        public void AttackByName(string name)
        {
            m_IsAutoChasing = false;
            string hash = name.GetHashCode().ToString();
            m_EnemyTarget = GameObject.Find(hash);
            if (m_EnemyTarget == null)
            {
                UIMgr.Instance.ShowWarningPanel();
                return;
            }
            m_Target.transform.LookAt(m_EnemyTarget.transform);
            m_IsAutoChasing = true;
        }

        public void CancelAutoChasing()
        {
            m_IsAutoChasing = false;
        }

        public override void ShowDamage(GameEntity attacker)
        {
            base.ShowDamage(attacker);
            this.CurHP -= attacker.Atk;
            m_UIStatus.UpdateHP(attacker.Atk, (float)this.CurHP / (float)this.HP);
            //print("ShowDamage !!!!!" + attacker.Atk);
        }
    }
}