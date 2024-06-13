using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor;

namespace GameScripts.Editor
{
    public static class LubanTools
    {
        [MenuItem("Game Framework/Tools/Luban 转表")]
        public static void BuildLubanExcel()
        {
            string batFilePath = Application.dataPath + "/../Luban/MiniTemplate/gen.bat";
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = batFilePath,
                UseShellExecute = true,
                WorkingDirectory = System.IO.Path.GetDirectoryName(batFilePath)
            };

            Process process = Process.Start(processInfo);
            if (process == null)
            {
                UnityEngine.Debug.LogError("无法启动转表进程。请检查路径：" + batFilePath);
            }
            else
            {
                UnityEngine.Debug.Log("转表工具已启动，路径：" + batFilePath);
            }
        }
        
        [MenuItem("Game Framework/Tools/打开表格目录")]
        public static void OpenConfigFolder()
        {
            OpenFolder.Execute(Application.dataPath + @"/../Luban/MiniTemplate/Datas");
        }
    }
}