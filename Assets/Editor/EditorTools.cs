// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using TreeEditor;
// using UnityEditor;
// using UnityEditor.AddressableAssets.Build;
// using UnityEditor.AddressableAssets.Settings;
// using UnityEditor.AddressableAssets.Settings.GroupSchemas;
// using UnityEditor.U2D;
// using UnityEngine;
// using UnityEngine.U2D;
// using UnityEngine.UI;

// namespace iGame {
//     public class EditorTools {

//         [MenuItem("工具/查找裁剪Class")]
//         public static void FindAllScripts() {
//             EditorUtility.DisplayProgressBar("Progress", "Find Class...", 0);
//             string[] dirs = { "Assets/Res" };
//             var asstIds = AssetDatabase.FindAssets("t:Prefab", dirs);
//             int count = 0;
//             List<string> classList = new List<string>();
//             for (int i = 0; i < asstIds.Length; i++) {
//                 string path = AssetDatabase.GUIDToAssetPath(asstIds[i]);
//                 var pfb = AssetDatabase.LoadAssetAtPath<GameObject>(path);
//                 foreach (Transform item in pfb.transform) {
//                     var coms = item.GetComponentsInChildren<Component>();
//                     foreach (var com in coms) {
//                         string tName = com.GetType().FullName;
//                         if (!classList.Contains(tName) && (tName.StartsWith("UnityEngine") || tName.StartsWith("TMPro"))) {
//                             classList.Add(tName);
//                         }
//                     }
//                 }
//                 count++;
//                 EditorUtility.DisplayProgressBar("Find Class", pfb.name, count / (float) asstIds.Length);
//             }
//             for (int i = 0; i < classList.Count; i++) {
//                 classList[i] = string.Format("<type fullname=\"{0}\" preserve=\"all\"/>", classList[i]);
//             }
//             System.IO.File.WriteAllLines(Application.dataPath + "/ClassTypes.txt", classList);
//             EditorUtility.ClearProgressBar();
//         }

//         [MenuItem("工具/平台/微信小游戏", true)]
//         public static bool CheckSelect() {
//             PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, out var ret);
//             Menu.SetChecked("工具/平台/游戏调试", ret.Contains("GAME_DEBUG"));
//             Menu.SetChecked("工具/平台/微信小游戏", ret.Contains("WEBGL_WX"));
//             return true;
//         }

//         [MenuItem("工具/平台/微信小游戏")]
//         public static void 平台_微信小游戏() {
//             AddDefine("WEBGL_WX");
//             SetGroupPathMode(ToPathEnum.Remote);
//         }

//         [MenuItem("工具/平台/游戏调试")]
//         public static void 平台_游戏调试() {
//             ChangeDefine("GAME_DEBUG");
//         }

//         [MenuItem("工具/构建/微信小游戏")]
//         public static void BuildWechatGame() {
//             SetResBuildVer();
//             Debug.Log("开始切换平台");
//             EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WeixinMiniGame, BuildTarget.WeixinMiniGame);
//             Debug.Log("切换平台完成");
//             AddDefine("WEBGL_WX");
//             SetGroupPathMode(ToPathEnum.Local);
//             SetProfiles("Default");
//             Debug.Log("开始构建微信工程");
//             WeChatWASM.WXEditorWin.DoExport();
//             Debug.Log("微信工程构建完成");
//         }

//         [MenuItem("工具/构建/PC")]
//         public static void BuildPC() {
//             SetResBuildVer();
//             Debug.Log("开始切换平台");
//             EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
//             Debug.Log("切换平台完成");
//             SetGroupPathMode(ToPathEnum.Remote);
//             RemoveDefine("WEBGL_WX");
//             SetProfiles("PC");
//             BuildAddressable();
//             BuildPlayerOptions options = new();
//             options.scenes = new [] { "Assets/App/App.unity" };
//             options.locationPathName = "build/windows/" + PlayerSettings.productName + ".exe";
//             options.target = BuildTarget.StandaloneWindows64;
//             options.options = BuildOptions.None;
//             Debug.Log("开始构建exe");
//             BuildPipeline.BuildPlayer(options);
//             Debug.Log("构建exe完成");
//         }

//         private static void SetResBuildVer() {
//             if (App.Config.NetEnv != NetEnvEnum.Online) {
//                 var filePath = "Assets/Res/Common/Static/ver.txt";
//                 var ver = DateTime.Now.ToString("yyyyMMddHHmmss");
//                 Debug.Log("资源版本" + ver);
//                 System.IO.File.WriteAllText(filePath, ver);
//                 AssetDatabase.Refresh();
//             }
//         }

//         /// </summary>
//         [MenuItem("工具/清除数据")]
//         public static void ClearData() {
//             PlayerPrefs.DeleteAll();
//             Debug.Log("清除成功");
//         }

//         [MenuItem("工具/所有Text文本")]
//         public static void ChangeFont_Prefab() {
//             List<Text[]> textList = new List<Text[]>();
//             //获取Asset文件夹下所有Prefab的GUID
//             string[] guids = AssetDatabase.FindAssets("t:Prefab");
//             var str = "";
//             for (int i = 0; i < guids.Length; i++) {
//                 //根据GUID获取路径
//                 var tmpPath = AssetDatabase.GUIDToAssetPath(guids[i]);
//                 if (!string.IsNullOrEmpty(tmpPath)) {
//                     //根据路径获取Prefab(GameObject)
//                     var tmpObj = AssetDatabase.LoadAssetAtPath(tmpPath, typeof(GameObject)) as GameObject;
//                     //获取Prefab及其子物体孙物体.......的所有Text组件
//                     var tmpArr = tmpObj.GetComponentsInChildren<Text>();
//                     foreach (var tem in tmpArr) {
//                         var s = tem.text.Trim();
//                         if (s != string.Empty) {
//                             str += tem.text;
//                         }
//                     }
//                 }
//             }
//             str = new System.Text.RegularExpressions.Regex("[^\u4e00-\u9fa5]").Replace(str, "");
//             var strArray = str.Distinct().ToArray(); //字符去重
//             str = string.Join(string.Empty, strArray); //字符成串
//             Debug.Log(str);
//         }

//         [MenuItem("工具/处理图片在WeiXin平台设置")]
//         public static void SetAllPicWeiXin() {
//             try {
//                 /// 多资源操作批处理 https://docs.unity.cn/cn/2019.4/Manual/AssetDatabaseBatching.html
//                 /// 开始前先把资源数据库锁起来，操作作完以后再刷新
//                 AssetDatabase.StartAssetEditing();
//                 var files = AssetDatabase.FindAssets("t:Texture t:SpriteAtlas", new string[] { "Assets/" }).Select(v => AssetDatabase.GUIDToAssetPath(v));
//                 foreach (var file in files) {
//                     try {
//                         if (file.EndsWith(".ttf")) continue;
//                         if (file.EndsWith(".spriteatlas")) {
//                             var atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(file);
//                             var webglSetting = atlas.GetPlatformSettings(BuildTarget.WebGL.ToString());
//                             var miniSetting = atlas.GetPlatformSettings(BuildTarget.WeixinMiniGame.ToString());
//                             if (webglSetting.overridden) {
//                                 miniSetting.overridden = webglSetting.overridden;
//                                 miniSetting.format = webglSetting.format;
//                                 miniSetting.maxTextureSize = webglSetting.maxTextureSize;
//                                 miniSetting.compressionQuality = webglSetting.compressionQuality;
//                                 atlas.SetPlatformSettings(miniSetting);
//                                 Debug.Log($"设置:{file}");
//                                 EditorUtility.SetDirty(atlas);
//                             }
//                         } else {
//                             var importer = AssetImporter.GetAtPath(file) as TextureImporter;
//                             var webglSetting = importer.GetPlatformTextureSettings(BuildTarget.WebGL.ToString());
//                             var miniSetting = importer.GetPlatformTextureSettings(BuildTarget.WeixinMiniGame.ToString());
//                             if (webglSetting.overridden) {
//                                 miniSetting.overridden = webglSetting.overridden;
//                                 miniSetting.format = webglSetting.format;
//                                 miniSetting.maxTextureSize = webglSetting.maxTextureSize;
//                                 miniSetting.compressionQuality = webglSetting.compressionQuality;
//                                 importer.SetPlatformTextureSettings(miniSetting);
//                                 importer.SaveAndReimport();
//                                 Debug.Log($"设置:{file}");
//                             }
//                         }
//                     } catch (System.Exception ex) {
//                         Debug.LogError($"设置失败:{file} {ex}");
//                         throw;
//                     }
//                 }
//                 AssetDatabase.SaveAssets();
//                 AssetDatabase.Refresh();
//                 Debug.Log("设置完成");
//             } finally {
//                 AssetDatabase.StopAssetEditing();
//             }
//         }

//         public enum ToPathEnum { Local, Remote }
//         static string addressableAssetSettings = "Assets/AddressableAssetsData/AddressableAssetSettings.asset";
//         public static void SetGroupPathMode(ToPathEnum toPath = ToPathEnum.Local) {
//             var settings = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>(addressableAssetSettings);
//             foreach (var group in settings.groups) {
//                 if (!group.Name.Equals("Built In Data")) {
//                     var bundledAssetGroupSchema = group.GetSchema<BundledAssetGroupSchema>();
//                     if (bundledAssetGroupSchema != null) {
//                         if (toPath == ToPathEnum.Remote) {
//                             bundledAssetGroupSchema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteBuildPath);
//                             bundledAssetGroupSchema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteLoadPath);
//                         } else {
//                             bundledAssetGroupSchema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kLocalBuildPath);
//                             bundledAssetGroupSchema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kLocalLoadPath);
//                         }
//                         bundledAssetGroupSchema.UseAssetBundleCache = toPath == ToPathEnum.Remote;
//                         bundledAssetGroupSchema.UseAssetBundleCrc = toPath == ToPathEnum.Remote;
//                     }
//                     var contentUpdateGroupSchema = group.GetSchema<ContentUpdateGroupSchema>();
//                     if (contentUpdateGroupSchema != null) {
//                         contentUpdateGroupSchema.StaticContent = toPath == ToPathEnum.Local;
//                     }
//                 }
//             }
//         }
//         static void BuildAddressable() {
//             Debug.Log("开始构建Addressable资源");
//             AddressableAssetSettings.BuildPlayerContent(out AddressablesPlayerBuildResult result);
//             bool success = string.IsNullOrEmpty(result.Error);
//             if (!success) {
//                 Debug.Log("Addressable 资源构建错误");
//             } else {
//                 Debug.Log("Addressable 资源构建完成");
//             }
//         }
//         static void SetProfiles(string profileName) {
//             var settings_asset = "Assets/AddressableAssetsData/AddressableAssetSettings.asset";
//             AddressableAssetSettings settings = AssetDatabase.LoadAssetAtPath<ScriptableObject>(settings_asset) as AddressableAssetSettings;
//             string profileId = settings.profileSettings.GetProfileId(profileName);
//             settings.activeProfileId = profileId;
//             settings.BuildRemoteCatalog = profileName == "PC";
//             AssetDatabase.SaveAssets();
//             AssetDatabase.Refresh();
//         }
//         static string[] Defines {
//             get {
//                 PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, out var ret);
//                 return ret;
//             }
//         }
//         static void RemoveDefine(string define) {
//             var list = Defines.ToList();
//             var idx = list.IndexOf(define);
//             if (idx != -1) {
//                 list.RemoveAt(idx);
//                 var ret = list.ToArray();
//                 PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, ret);
//                 PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, ret);
//                 PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, ret);
//             }
//         }
//         static void AddDefine(string define) {
//             var list = Defines.ToList();
//             var idx = list.IndexOf(define);
//             if (idx == -1) {
//                 list.Add(define);
//                 var ret = list.ToArray();
//                 PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, ret);
//                 PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, ret);
//                 PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, ret);
//             }
//         }
//         static void ChangeDefine(string define) {
//             if (Defines.Contains(define)) {
//                 RemoveDefine(define);
//             } else {
//                 AddDefine(define);
//             }
//         }
//     }
// }