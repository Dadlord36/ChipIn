using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;

public static class BuildScript
{
    private const string AppName = "Chip-In";
    private const string AndroidBuildsPath = "./builds/Android/";
    private static readonly string[] DefaultScene = {"Assets/Scenes/Main.unity"};
    private const string AndroidExtension = ".apk";
    private const string ReleaseI2CPP_x64 = "_Release_IL2CPP_ARM64";
    private const string DevelopmentMono_x32 = "_Development_ARMv7";
    private static readonly string FolderRoot = $@"{Environment.CurrentDirectory}\Builds\Android\";


    private const string KeyStorePassword = "TtZWc6#7TK@r";

    [MenuItem("Builds/DeployToAndroid_Mono_Development_ARMv7")]
    public static void DeployToAndroid_Mono_Development_ARMv7()
    {
        DeployToAndroid($"{AppName}{DevelopmentMono_x32}");
    }

    [MenuItem("Builds/DeployToAndroid_Release_IL2CPP_ARM64")]
    public static void DeployToAndroid_Release_IL2CPP_ARM64()
    {
        DeployToAndroid($"{AppName}{ReleaseI2CPP_x64}");
    }

    [MenuItem("Builds/Build For Android_Mono_Development")]
    public static void BuildForAndroid_Mono_Development_ARMv7()
    {
        SetKeyStorePasswords();
        PerformBuild(BuildTarget.Android, BuildTargetGroup.Android, ScriptingImplementation.Mono2x, AndroidArchitecture.ARMv7,
            BuildOptions.Development | BuildOptions.AllowDebugging, $"{AppName}{DevelopmentMono_x32}{AndroidExtension}");
    }

    [MenuItem("Builds/SelectedScene/Build For Android_Mono_Development")]
    public static void BuildSelectedSceneForAndroid_Mono_Development_ARMv7()
    {
        string[] sceneAsArray = null;
        if (Selection.activeObject is SceneAsset sceneAsset)
        {
            sceneAsArray = new[] {AssetDatabase.GetAssetPath(sceneAsset)};
        }
        else
        {
            return;
        }

        SetKeyStorePasswords();
        PerformBuild(BuildTarget.Android, BuildTargetGroup.Android, ScriptingImplementation.Mono2x, AndroidArchitecture.ARMv7,
            BuildOptions.Development | BuildOptions.AllowDebugging, $"{AppName}{DevelopmentMono_x32}{AndroidExtension}", sceneAsArray);

        DeployToAndroid_Mono_Development_ARMv7();
    }

    [MenuItem("Builds/Remove App from Device", false, 100)]
    public static async void RemoveAppFromDevice()
    {
        var process = CreateProcessForAdbTask("uninstall", PlayerSettings.applicationIdentifier);
        process.Start();
        var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        await Task.Run(process.WaitForExit).ContinueWith(delegate
        {
            try
            {
                EditorUtility.DisplayDialog("App removing", $"App was removed successfully", "OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }, continuationOptions: TaskContinuationOptions.OnlyOnRanToCompletion, scheduler: scheduler, cancellationToken: CancellationToken.None);
        
    }

    [MenuItem("Builds/Build For Android_IL2CPP_Release")]
    public static void BuildForAndroid_IL2CPP_Release()
    {
        SetKeyStorePasswords();
        PerformBuild(BuildTarget.Android, BuildTargetGroup.Android, ScriptingImplementation.IL2CPP, AndroidArchitecture.ARM64, BuildOptions.None,
            $"{AppName}{ReleaseI2CPP_x64}{AndroidExtension}");
    }

    [MenuItem("Builds/Build and Deploy/Android/IL2CPP_Release")]
    public static void BuildAndDeployI2CPPToAndroid()
    {
        BuildForAndroid_IL2CPP_Release();
        DeployToAndroid_Release_IL2CPP_ARM64();
    }

    [MenuItem("Builds/Build and Deploy/Android/ReleaseMono32")]
    public static void BuildAndDeployMono32ToAndroid()
    {
        BuildForAndroid_Mono_Development_ARMv7();
        DeployToAndroid_Mono_Development_ARMv7();
    }

    private static Process CreateProcessForAdbTask(string actionType, string arguments)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = "adb.exe",
                Arguments = $"{actionType} {arguments}"
            }
        };
        return process;
    }

    private static async void DeployToAndroid(string appTypeName)
    {
        var process = CreateProcessForAdbTask("install", $"{FolderRoot}{appTypeName}{AndroidExtension}");
        process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs args)
        {
            EditorUtility.DisplayDialog("Deployment Result", $"Deployment of {appTypeName} is failed to finish. Reason: {args.Data}", "OK");
        };
        process.Start();
        var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        await Task.Run(process.WaitForExit).ContinueWith(delegate
        {
            try
            {
                EditorUtility.DisplayDialog("Deployment Result", $"Deployment of {appTypeName} is finished successfully", "OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }, continuationOptions: TaskContinuationOptions.OnlyOnRanToCompletion, scheduler: scheduler, cancellationToken: CancellationToken.None);
    }

    private static void SetKeyStorePasswords()
    {
        PlayerSettings.keystorePass = KeyStorePassword;
        PlayerSettings.keyaliasPass = KeyStorePassword;
    }

    private static void PerformBuild(BuildTarget buildTarget, BuildTargetGroup buildTargetGroup, ScriptingImplementation scriptingImplementation,
        AndroidArchitecture architecture, BuildOptions typeOfBuild, string apkName, string[] scenes = null)
    {
        PlayerSettings.SetScriptingBackend(buildTargetGroup, scriptingImplementation);
        PlayerSettings.Android.targetArchitectures = architecture;

        BuildPipeline.BuildPlayer(scenes == null || scenes.Length == 0 ? DefaultScene : scenes,
            $"{AndroidBuildsPath}{apkName}", buildTarget, typeOfBuild);
    }
}