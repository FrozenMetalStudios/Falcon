/***************************************************************************************************************
 * Filename: BuildPostProcess.cs
 * 
 * Description: This script allows additional processing to occur after a build, 
 *              thus allowing the customization of the resulting package.
 * 
 * NOTE: This script uses a method only available in Unity PRO. 
 *       It therefore exists as a reminder of future posibilities when PRO is aquired.
 * 
 **************************************************************************************************************/
/*
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Diagnostics;

public class ScriptBatch : MonoBehaviour
{
    // Name of Binary Output File
    public static string OutputFile = "Project_Falcon.exe";

    // Name of Readme File
    public static string ReadmeFile = "Readme.txt";

    // Location of associated Readme file
    public static string PathToReadme = "Assets/Resources/Supporting/" + ReadmeFile;

    [MenuItem("MyTools/Windows Build With Postprocess")]
    public static void BuildGame()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        string[] associatedScenes = new string[] { "Assets/Scene1.unity", "Assets/Scene2.unity" };

        // Build player. NOTE: Requires PRO
        BuildPipeline.BuildPlayer(associatedScenes, path + "/" + OutputFile, BuildTarget.StandaloneWindows, BuildOptions.None);

        // Copy a file from the project folder to the build folder, alongside the built game.
        FileUtil.CopyFileOrDirectory(PathToReadme, path + ReadmeFile);

        // Run the game (Process class from System.Diagnostics).
        Process proc = new Process();
        proc.StartInfo.FileName = path + OutputFile;
        proc.Start();
    }

    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        Debug.Log("Project built at: " + pathToBuiltProject);
    }
}
*/