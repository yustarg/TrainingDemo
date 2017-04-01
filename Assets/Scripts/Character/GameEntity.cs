using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public abstract class GameEntity : MonoBehaviour
    {
        public long m_ID { get; set; }
        protected StateMachine m_FSM;

        [SerializeField]
        private float m_MoveSpeed = 20;
        [SerializeField]
        private float m_MovingTurnSpeed = 360;
        [SerializeField]
        private float m_StationaryTurnSpeed = 180;

        float m_TurnAmount;
        float m_ForwardAmount;

        protected Animator m_Animator;
        public Animator Anim
        {
            get { return m_Animator; }
        }

        public virtual void Move(Vector3 dir)
        {
            if (dir.magnitude > 1f) dir.Normalize();
            dir = transform.InverseTransformDirection(dir);

            m_TurnAmount = Mathf.Atan2(dir.x, dir.z);
            m_ForwardAmount = dir.z;

            ApplyExtraTurnRotation();
            transform.Translate(dir * Time.deltaTime * m_MoveSpeed);
        }

        void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }
    }
}