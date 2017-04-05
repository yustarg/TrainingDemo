using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class EAttack : State
    {
        public EAttack(GameEntity ge, string name) : base(ge, name) { }

        public override void OnEnter()
        {
            int att = Random.Range(1, 3);
            //m_GameEntity.Anim.SetInteger("AttackNum", att);
            m_GameEntity.Anim.Play("PuTongGongJi_" + att.ToString());
        }

        public override void OnExcute(params object[] p)
        {
            if (!m_GameEntity.Anim.IsPlaying("PuTongGongJi_1") &&
                !m_GameEntity.Anim.IsPlaying("PuTongGongJi_2"))
            {
                int att = Random.Range(1, 3);
                m_GameEntity.Anim.Play("PuTongGongJi_" + att.ToString());
                GameObject target = (GameObject)p[0];
                GEPlayer player = target.GetComponent<GEPlayer>();
                m_GameEntity.Attack(player);
            }          
        }

        public override void OnExit()
        {
            //m_GameEntity.Anim.SetInteger("AttackNum", -1);
        }
    }
}
