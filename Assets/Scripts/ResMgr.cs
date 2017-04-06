using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class ResMgr : MonoBehaviour
    {
        private static ResMgr m_Instance;
        public static ResMgr Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    Debug.LogError("ResMgr is NULL !!");
                }
                return m_Instance;
            }
        }

        public List<string> m_NeedPreloadAssetsNames;
        private Dictionary<string, Object> m_PreloadAssets;

        void Awake()
        {
            m_Instance = this;
            m_NeedPreloadAssetsNames = new List<string>();
            m_NeedPreloadAssetsNames.Add("Prefabs/Characters/Enemy");
            m_NeedPreloadAssetsNames.Add("Prefabs/Characters/Player");
            m_NeedPreloadAssetsNames.Add("Prefabs/UI/EnemyItem");
            m_NeedPreloadAssetsNames.Add("Prefabs/UI/StatusItem");
            m_PreloadAssets = new Dictionary<string, Object>();
            PreLoad();
        }

        private void PreLoad()
        {
            for (int i = 0; i < m_NeedPreloadAssetsNames.Count; i++)
            {
                Object res = Resources.Load(m_NeedPreloadAssetsNames[i]);
                m_PreloadAssets.Add(m_NeedPreloadAssetsNames[i], res);
            }
        }

        public Object GetRes(string path)
        {
            Object res = null;
            m_PreloadAssets.TryGetValue(path, out res);
            return res;
        }
    }
}