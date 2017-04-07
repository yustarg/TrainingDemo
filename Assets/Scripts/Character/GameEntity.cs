using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public abstract class GameEntity : MonoBehaviour
    {
        public long ID { get; set; }
        public string Name { get; set; }
        protected StateMachine m_FSM;

        [SerializeField]
        protected float m_MoveSpeed = 20;
        [SerializeField]
        private float m_MovingTurnSpeed = 360;
        [SerializeField]
        private float m_StationaryTurnSpeed = 180;

        private float m_TurnAmount;
        private float m_ForwardAmount;

        //protected Animator m_Animator;
        //public Animator Anim
        //{
        //    get { return m_Animator; }
        //}

        protected Animation m_Animation;
        public Animation Anim
        {
            get { return m_Animation; }
        }

        public int HP { get; set; }
        public int CurHP { get; set; }
        public int Atk { get; set; }
        protected int AtkDistance { get; set; }
        protected UIStatusItem m_UIStatus;
        public float HeadDistance { get; set; }  // ui距离头顶距离

        public virtual void Move(Vector3 dir)
        {
            if (dir.magnitude > 1f) dir.Normalize();
            dir = transform.InverseTransformDirection(dir);

            m_TurnAmount = Mathf.Atan2(dir.x, dir.z);
            m_ForwardAmount = dir.z;

            ApplyExtraTurnRotation();
            Rigidbody r = transform.GetComponent<Rigidbody>();
            transform.Translate(dir * Time.deltaTime * m_MoveSpeed);            
        }

        public virtual void Attack(GameEntity other)
        {
            other.ShowDamage(this);
        }

        public virtual void ShowDamage(GameEntity attacker) { }

        public virtual Transform GetHeadPoint() 
        {
            return transform.FindChild("HeadPoint");
        }

        protected virtual void InitData() { }

        void ApplyExtraTurnRotation()
        {
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }
    }
}