using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace Training
{
    public class UIMgr : MonoBehaviour
    {
        private static UIMgr m_Instance;
        public static UIMgr Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    Debug.LogError("UIMgr is NULL !!");
                }
                return m_Instance;
            }
        }

        public UIPanel m_WarningPanel;
        public UIPanel m_StatusPanel;
        public UIButton m_CancelButton;
        public UIButton m_SwitchMotionBlurButton;
        public UIGrid m_Grid;

        void Awake()
        {
            m_Instance = this;
        }

        void Start()
        {
            EventDelegate.Add(m_CancelButton.onClick, OnCancelClick);
            EventDelegate.Add(m_SwitchMotionBlurButton.onClick, OnSwitchMotionBlurClick);
        }

        public void Init(string[] names)
        {
            GameObject enemyItemPrefab = (GameObject)ResMgr.Instance.GetRes("Prefabs/UI/EnemyItem");
            for (int i = 0; i < names.Length; i++)
            {
                GameObject itemGO = NGUITools.AddChild(m_Grid.gameObject, enemyItemPrefab);
                itemGO.GetComponent<UIEnemyItem>().SetContent(names[i]);
            }
            m_Grid.Reposition();
        }

        public void ShowWarningPanel()
        {
            m_WarningPanel.alpha = 1;
            m_WarningPanel.GetComponent<TweenAlpha>().ResetToBeginning();
            m_WarningPanel.GetComponent<TweenAlpha>().enabled = true;
        }

        private void OnCancelClick()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.SendMessage("CancelAutoChasing");
        }

        private void OnSwitchMotionBlurClick()
        {
            MotionBlur blur = Camera.main.GetComponent<MotionBlur>();
            blur.enabled = !blur.enabled;
        }

        public UIStatusItem GenStatusItem(GameEntity entity)
        {
            GameObject statusItemPrefab = (GameObject)ResMgr.Instance.GetRes("Prefabs/UI/StatusItem");
            GameObject itemGO = NGUITools.AddChild(m_StatusPanel.gameObject, statusItemPrefab);
            UIStatusItem item = itemGO.GetComponent<UIStatusItem>();
            item.SetContent(entity);
            return item;
        }
    }
}