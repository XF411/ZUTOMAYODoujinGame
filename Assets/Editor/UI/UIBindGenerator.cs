using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using GameLogic;


public static class UIBindGenerator 
{
    [MenuItem("GameObject/ScriptGenerator/GeneratorUIBind")]
    public static void GenerateScript()
    {
        // 获取选中的根对象
        GameObject root = Selection.activeGameObject;
        if (root == null)
        {
            Debug.LogError("Please select a root GameObject.");
            return;
        }

        string className = root.name + "Bind";
        string outputPath = "Assets/GameScripts/HotFix/GameLogic/Login/UIBind/" + className + ".cs";

        // 创建保存目录
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

        // 收集组件信息
        Dictionary<string, string> componentInfo = new Dictionary<string, string>();
        CollectComponentInfo(root, root, componentInfo, new Dictionary<string, int>());

        // 生成脚本内容
        string scriptContent = GenerateScriptContent(className, componentInfo);

        // 写入脚本文件
        File.WriteAllText(outputPath, scriptContent);
        AssetDatabase.Refresh();
    }

    private static void CollectComponentInfo(GameObject root, GameObject current, Dictionary<string, string> componentInfo, Dictionary<string, int> nameCount)
    {
        foreach (Transform child in current.transform)
        {
            string componentName = GetUniqueName(child.name, nameCount);
            string componentType = GetComponentType(child.gameObject);

            if (!string.IsNullOrEmpty(componentType))
            {
                componentInfo[componentName] = componentType;
            }

            if (!HasBindScript(child.gameObject))
            {
                CollectComponentInfo(root, child.gameObject, componentInfo, nameCount);
            }
            else
            {
                string bindClassName = child.gameObject.GetComponent<UIBindBase>().GetType().Name;
                componentInfo[componentName] = bindClassName;
            }
        }
    }

    private static string GetUniqueName(string baseName, Dictionary<string, int> nameCount)
    {
        if (!nameCount.ContainsKey(baseName))
        {
            nameCount[baseName] = 0;
            return baseName;
        }
        nameCount[baseName]++;
        return baseName + nameCount[baseName];
    }

    private static string GetComponentType(GameObject obj)
    {
        if (obj.GetComponent<Button>() != null) return "Button";
        if (obj.GetComponent<Text>() != null) return "Text";
        if (obj.GetComponent<Image>() != null) return "Image";
        if (obj.GetComponent<RawImage>() != null) return "RawImage";
        if (obj.GetComponent<Toggle>() != null) return "Toggle";
        if (obj.GetComponent<Slider>() != null) return "Slider";
        if (obj.GetComponent<Scrollbar>() != null) return "Scrollbar";
        if (obj.GetComponent<Dropdown>() != null) return "Dropdown";
        if (obj.GetComponent<InputField>() != null) return "InputField";
        if (obj.GetComponent<Canvas>() != null) return "Canvas";
        if (obj.GetComponent<CanvasGroup>() != null) return "CanvasGroup";
        if (obj.GetComponent<RectTransform>() != null) return "RectTransform";
        if (obj.GetComponent<ScrollRect>() != null) return "ScrollRect";
        if (obj.GetComponent<TMPro.TextMeshProUGUI>() != null) return "TextMeshProUGUI";
        if (obj.GetComponent<TMPro.TMP_InputField>() != null) return "TMP_InputField";
        if (obj.GetComponent<TMPro.TMP_Dropdown>() != null) return "TMP_Dropdown";
        return "RectTransform";
    }


    private static bool HasBindScript(GameObject obj)
    {
        return obj.GetComponent<UIBindBase>() != null;
    }

    private static string GenerateScriptContent(string className, Dictionary<string, string> componentInfo)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using UnityEngine.UI;");
        sb.AppendLine("using TMPro;");
        sb.AppendLine("using GameLogic.Common;");
        sb.AppendLine();
        sb.AppendLine("namespace GameLogic ");
        sb.AppendLine("{");
        sb.AppendLine(" public class " + className + " : UIBindBase");
        sb.AppendLine(" {");

        foreach (var kvp in componentInfo)
        {
            sb.AppendLine($"    public {kvp.Value} {kvp.Key};");
        }

        sb.AppendLine();
        sb.AppendLine("    protected override void BindUI()");
        sb.AppendLine("    {");

        foreach (var kvp in componentInfo)
        {
            sb.AppendLine($"        {kvp.Key} = GameObjectCommon.FindComponentWithName<{kvp.Value}>(\"{kvp.Key}\", gameObject);");
        }

        sb.AppendLine("     }");
        sb.AppendLine(" }");
        sb.AppendLine("}");
        return sb.ToString();
    }
}
