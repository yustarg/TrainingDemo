using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class UIStatusItem : MonoBehaviour
    {
        public UILabel m_LName;
        public UILabel m_LDamage;
        public UIProgressBar m_PbHP;
        private string m_Name;
        private Transform m_Head;
        private float m_Distance = 3f;
        private GameEntity m_Entity;
        private Camera m_MainCam;

        void Start()
        {
            m_PbHP.value = 1;
            m_MainCam = Camera.main;
        }

        void LateUpdate()
        {
            if (m_Head == null) return;
            Vector3 dir = m_Entity.transform.position - m_MainCam.transform.position;
            Vector3 camForward = m_MainCam.transform.forward;
            if (Vector3.Dot(dir, camForward) < 0)
            {
                m_LName.gameObject.SetActive(false);
                m_PbHP.gameObject.SetActive(false);
            }
            else
            {
                m_LName.gameObject.SetActive(true);
                m_PbHP.gameObject.SetActive(true);
            }
            float ratio = m_Distance / Vector3.Distance(m_Head.position, m_MainCam.transform.position);
            transform.position = WorldToUI(m_Head.position);
            transform.localScale = Vector3.one * ratio;
        }

        public static Vector3 WorldToUI(Vector3 point)
        {
            Vector3 pt = Camera.main.WorldToScreenPoint(point);
            Vector3 ff = UICamera.currentCamera.ScreenToWorldPoint(pt);
            ff.z = 0;
            return ff;
        }

        public void SetContent(GameEntity entity)
        {
            m_Entity = entity;
            m_LName.text = entity.Name;
            m_Name = entity.Name;
            m_Head = entity.GetHeadPoint();
            m_Distance = entity.HeadDistance;
        }

        public void UpdateHP(int minusHP, float ratio)
        {
            m_PbHP.value = ratio;
            ShowMinus(minusHP);
        }

        private void ShowMinus(int minus)
        {
            m_LDamage.text = minus.ToString();
            m_LDamage.gameObject.SetActive(true);
            TweenPosition tp = m_LDamage.GetComponent<TweenPosition>();
            tp.ResetToBeginning();
            tp.from = WorldToUI(m_Head.position);
            tp.to = new Vector3(tp.from.x + 20, tp.from.y + 20, m_Head.position.z);
            TweenAlpha ta = m_LDamage.GetComponent<TweenAlpha>();
            ta.ResetToBeginning();
            ta.PlayForward();
            tp.PlayForward();
        }
    }
}
