using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            m_NeedPreloadAssetsNames.Add("Prefabs/Characters/Enemy.prefab");
            m_NeedPreloadAssetsNames.Add("Prefabs/Characters/Player.prefab");
            m_NeedPreloadAssetsNames.Add("Prefabs/UI/EnemyItem.prefab");
            m_NeedPreloadAssetsNames.Add("Prefabs/UI/StatusItem.prefab");
            m_PreloadAssets = new Dictionary<string, Object>();
            PreLoad();
        }

        private void PreLoad()
        {
            for (int i = 0; i < m_NeedPreloadAssetsNames.Count; i++)
            {
#if ASSETBUNDLE
                string resName = Convert2ABName(m_NeedPreloadAssetsNames[i]);
                AssetBundle loadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "AssetBundles/" + resName));
                Object res = loadedAssetBundle.LoadAsset(GetAssetName(m_NeedPreloadAssetsNames[i]));
                loadedAssetBundle.Unload(false);
#else
                int end = m_NeedPreloadAssetsNames[i].LastIndexOf('.');
                string resPath = m_NeedPreloadAssetsNames[i].Substring(0, (end == -1 ? m_NeedPreloadAssetsNames[i].Length : end));
                Object res = Resources.Load(resPath);
#endif
                m_PreloadAssets.Add(m_NeedPreloadAssetsNames[i], res);
            }
        }

        private string Convert2ABName(string oriName)
        {
            string resName = oriName.ToLower();
            return resName.Replace('/', '_');
        }

        private string GetAssetName(string oriName)
        {
            return Path.GetFileName(oriName);
        }

        public Object GetRes(string path)
        {
            Object res = null;
            m_PreloadAssets.TryGetValue(path, out res);
            return res;
        }

        public void Unload()
        {
            m_PreloadAssets.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}