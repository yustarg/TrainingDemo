using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class BuildAssetBundle
{
    public static float count = 0;

    const string BuildAssetBundlesMenu = "Build/Build AssetBundles ";
    const string AssetBundlesPath = "/../AssetBundles/";
    const string PC = "PC";
    const string IOS = "IOS";
    const string Android = "Android";
    const string AssetBundles = "/AssetBundles";

    static BuildTarget sTarget = BuildTarget.StandaloneWindows64;

#if UNITY_EDITOR && UNITY_STANDALONE
    [MenuItem(BuildAssetBundlesMenu + "(" + PC + ")")]
    public static void ShowPCWindow()
    {
        sTarget = BuildTarget.StandaloneWindows64;
        BuildAssetBundles();
    }
#elif UNITY_EDITOR && UNITY_ANDROID
    [MenuItem(BuildAssetBundlesMenu + "(" + Android + ")")]
    public static void ShowAndroidWindow()
    {
        sTarget = BuildTarget.Android;
        BuildAssetBundles();
    }
#endif

    public static void BuildAssetBundles()
    {
        if (!Directory.Exists(Application.dataPath + "/Resources"))
        {
            EditorUtility.DisplayDialog("提示", "请保证有Resources目录", "好的");
            return;
        }
        InternalBuildAssetBundles();
    }

    public static void CopyAssetBundles(BuildTarget target)
    {
        string path = GetPlatformPath(target);
        string streamingAssetBundlesPath = Application.streamingAssetsPath + "/AssetBundles";
        if (Directory.Exists(streamingAssetBundlesPath))
            Directory.Delete(streamingAssetBundlesPath, true);
        AssetDatabase.Refresh();
        DirectoryCopy(path, streamingAssetBundlesPath, true, ".manifest");
        AssetDatabase.Refresh();
    }

    static void InternalBuildAssetBundles()
    {
        AssetDatabase.Refresh();

        List<string> assetList = new List<string>();
        string assetFullPath = string.Format("{0}/{1}", Application.dataPath, "Resources");
        List<string> extList = new List<string>() { "*.prefab" };
        foreach (string ext in extList)
            assetList.AddRange(Directory.GetFiles(assetFullPath, ext, SearchOption.AllDirectories));
        foreach (string asset in assetList)
        {
            string fileName = Path.GetFileName(asset);
            string assetPath = asset.Replace(Application.dataPath + "/", "");
            string assetbundleName = assetPath.Substring("Resources".Length + 1);
            assetbundleName = assetbundleName.Replace(Path.DirectorySeparatorChar, '_');
            AssetImporter ai = AssetImporter.GetAtPath("Assets/" + assetPath);
            ai.assetBundleName = assetbundleName;
        }

        // 根据平台得到输出目录
        string path = GetPlatformPath(sTarget);
        //// 执行Build操作
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        BuildPipeline.BuildAssetBundles(path,
            BuildAssetBundleOptions.IgnoreTypeTreeChanges,
            sTarget);
        AssetDatabase.Refresh();
    }

    static string GetPlatformPath(BuildTarget target)
    {
        return Application.dataPath + AssetBundlesPath;
    }

    private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, string ignoreExt)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        // If the destination directory doesn't exist, create it. 
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            if (file.Extension == ignoreExt)
                continue;
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, true);
        }

        // If copying subdirectories, copy them and their contents to new location. 
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs, ignoreExt);
            }
        }
    }

    static string CalcMD5(string file)
    {
        if (!File.Exists(file))
            return string.Empty;
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(file))
                return Encoding.UTF8.GetString(md5.ComputeHash(stream));
        }
    }
}
