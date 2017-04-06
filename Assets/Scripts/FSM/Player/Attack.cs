using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class Attack : State
    {
        public Attack(GameEntity ge, string name) : base(ge, name) { }

        public override void OnEnter()
        {
            int att = Random.Range(1, 4);
            //m_GameEntity.Anim.SetInteger("AttackNum", att);
            m_GameEntity.Anim.Play("PuTongGongJi" + att.ToString() + "_JJ");
            RaycastHit hit;
            Ray ray = new Ray(m_GameEntity.transform.position, m_GameEntity.transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Enemy")
                {
                    GameEntity ge = hit.transform.GetComponent<GameEntity>();
                    m_GameEntity.Attack(ge);
                }
            }
        }

        public override void OnExcute(params object[] p)
        {
            
        }

        public override void OnExit()
        {
            //m_GameEntity.Anim.SetInteger("AttackNum", -1);
            m_GameEntity.Anim.CrossFade("ZhanLi_TY");

        }
    }
}
