using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour {

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
    public UIButton m_CancelButton;
    public UIGrid m_Grid;
    private GameObject m_EnemyItemPrefab;

    void Awake()
    {
        m_Instance = this;
    }

    void Start()
    {
        EventDelegate.Add(m_CancelButton.onClick, OnCancelClick);
    }

    public void Init(string[] names)
    {
        m_EnemyItemPrefab = Resources.Load<GameObject>("Prefabs/UI/EnemyItem");
        for (int i = 0; i < names.Length; i++)
        {
            GameObject itemGO = NGUITools.AddChild(m_Grid.gameObject, m_EnemyItemPrefab);
            itemGO.GetComponent<EnemyItem>().SetContent(names[i]);
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
}
