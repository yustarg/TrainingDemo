using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class Attack : State
    {
        public Attack(GameEntity ge) : base(ge) { }

        public override void OnEnter()
        {
            int att = Random.Range(1, 4);
            m_GameEntity.Anim.SetInteger("AttackNum", att);
        }

        public override void OnExcute(params object[] p)
        {
            
        }

        public override void OnExit()
        {
            m_GameEntity.Anim.SetInteger("AttackNum", -1);
        }
    }
}
