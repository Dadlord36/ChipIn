using UnityEditor;

public static class BuildScript
{
    private const string AppName = "Chip-In";
    private const string AndroidBuildsPath = "./builds/Android/";
    private static readonly string[] DefaultScene = {"Assets/Scenes/Main.unity"};
    private const string AndroidExtension = ".apk";

    [MenuItem("Builds/Build For Android_Mono_Development")]
    public static void BuildForAndroid_Mono_Development()
    {
        PerformBuild(BuildTarget.Android, BuildTargetGroup.Android, ScriptingImplementation.Mono2x, AndroidArchitecture.ARMv7,
            BuildOptions.Development, $"{AppName}_Development_ARMv7{AndroidExtension}");
    }

    [MenuItem("Builds/Build For Android_IL2CPP_Release")]
    public static void BuildForAndroid_IL2CPP_Release()
    {
        PerformBuild(BuildTarget.Android, BuildTargetGroup.Android, ScriptingImplementation.IL2CPP, AndroidArchitecture.ARM64, BuildOptions.None,
            $"{AppName}_Release_IL2CPP_ARM64{AndroidExtension}");
    }

    private static void PerformBuild(BuildTarget buildTarget, BuildTargetGroup buildTargetGroup, ScriptingImplementation scriptingImplementation,
        AndroidArchitecture architecture, BuildOptions typeOfBuild, string apkName)
    {
        PlayerSettings.SetScriptingBackend(buildTargetGroup, scriptingImplementation);
        PlayerSettings.Android.targetArchitectures = architecture;
        BuildPipeline.BuildPlayer(DefaultScene, $"{AndroidBuildsPath}{apkName}", buildTarget, typeOfBuild
                                                                                              | BuildOptions.ShowBuiltPlayer);
    }
}