using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItem : MonoBehaviour {

    public UILabel m_LName;
    public UIButton m_BItem;

    private string m_Name;
    private GameObject m_Target;
    
    // Use this for initialization
	void Start () {
        EventDelegate.Add(m_BItem.onClick, OnHeadClick);
        m_Target = GameObject.FindGameObjectWithTag("Player");
	}

    private void OnHeadClick()
    {
        m_Target.SendMessage("AttackByName", m_Name, SendMessageOptions.DontRequireReceiver);
    }

    public void SetContent(string name)
    {
        m_LName.text = name;
        m_Name = name;
    }
}
