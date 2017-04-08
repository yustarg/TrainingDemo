using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

/// <summary>
/// 发布工具
/// </summary>
public class BuildPackage
{
    // 菜单定义
    const string PackageBuilderMenu = "Build/Build Package ";
    const string PC = "(PC)";
    const string IOS = "(IOS)";
    const string Android = "(Android)";

    // 固定场景路径
    static readonly string[] Scenes = new string[]
    {
        "Assets/Scenes/demo.unity",
    };

    // 宏定义
    const string Defines = "ASSETBUNDLE";

    private static BuildPackage sWindow = null;

    // 环境变量
    public static string sPlatform = string.Empty;
    public static string sBuildPath = string.Empty;
    public static BuildTargetGroup sTargetGroup = BuildTargetGroup.Standalone;
    public static BuildTarget sTarget = BuildTarget.StandaloneWindows64;

#if UNITY_EDITOR && UNITY_STANDALONE_WIN
    [MenuItem(PackageBuilderMenu + PC)]
    static void BuildPCPackage()
    {
        sPlatform = "PC";
        sTargetGroup = BuildTargetGroup.Standalone;
        sTarget = BuildTarget.StandaloneWindows64;
        Build();
    }

#elif UNITY_EDITOR && UNITY_ANDROID
    [MenuItem(PackageBuilderMenu + Android)]
    static void BuildAndroidApk()
    {
        PlayerSettings.Android.preferredInstallLocation = AndroidPreferredInstallLocation.PreferExternal;
        sPlatform = "Android";
        sTargetGroup = BuildTargetGroup.Android;
        sTarget = BuildTarget.Android;
        Build();
    }
#endif

    static void Build()
    {
        switch (sTarget)
        {
            case BuildTarget.StandaloneWindows64:
                sBuildPath = EditorUtility.SaveFilePanel("保存exe路径", Application.dataPath + "/..", "package", "exe");
                break;
            case BuildTarget.Android:
                sBuildPath = EditorUtility.SaveFilePanel("保存apk路径", Application.dataPath + "/..", "package", "apk");
                break;
            default:
                break;
        }
        if (string.IsNullOrEmpty(sBuildPath))
        {
            Debug.Log("Build package cancel");
            return;
        }
        AssetDatabase.RenameAsset("Assets/Resources", "Resources_");
        BuildAssetBundle.CopyAssetBundles(sTarget);
        InternalBuild();
        AssetDatabase.RenameAsset("Assets/Resources_", "Resources");
    }

    // 打包
    static void InternalBuild()
    {
        if (string.IsNullOrEmpty(sBuildPath))
            return;
        // 设置不变场景
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            scene.enabled = false;
            foreach (string s in Scenes)
                scene.enabled = scene.enabled || (s == scene.path);
        }
        EditorBuildSettings.scenes = scenes;
        // 设置宏定义
        PlayerSettings.SetScriptingDefineSymbolsForGroup(sTargetGroup, Defines);
        // 打包
        BuildPipeline.BuildPlayer(Scenes, sBuildPath, sTarget, BuildOptions.None);
        AssetDatabase.Refresh();
        Debug.Log("Build Package OK!");
    }
}
