using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

public static class ProjectDiceAndroidBuild
{
    private const string DefaultBuildPath = "/tmp/projectdice-build/ProjectDice.apk";
    private const string ApplicationIdentifier = "com.brokenradiolab.projectdice";

    public static void BuildApk()
    {
        string outputPath = Environment.GetEnvironmentVariable("PROJECT_DICE_APK_PATH");

        if (string.IsNullOrWhiteSpace(outputPath))
        {
            outputPath = DefaultBuildPath;
        }

        string outputDirectory = Path.GetDirectoryName(outputPath);

        if (!string.IsNullOrEmpty(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        if (scenes.Length == 0)
        {
            throw new InvalidOperationException("At least one enabled scene is required to build Project Dice.");
        }

        PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, ApplicationIdentifier);

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outputPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);

        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new InvalidOperationException($"Android APK build failed: {report.summary.result}");
        }
    }
}
