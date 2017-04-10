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
            //m_GameEntity.transform.LookAt();
            int att = Random.Range(1, 4);
            //m_GameEntity.Anim.SetInteger("AttackNum", att);
            float height = m_GameEntity.GetComponent<GEPlayer>().Height;
            m_GameEntity.Anim.Play("PuTongGongJi" + att.ToString() + "_JJ");
            RaycastHit hit;
            Ray ray = new Ray(new Vector3(m_GameEntity.transform.position.x, m_GameEntity.transform.position.y + height,
                                        m_GameEntity.transform.position.z), m_GameEntity.transform.forward);
            //Debug.DrawRay();
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
            //m_GameEntity.Anim.CrossFade("ZhanLi_TY");

        }
    }
}
