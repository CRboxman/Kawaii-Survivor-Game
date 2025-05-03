using UnityEngine;
using UnityEditor;

public class SceneObjectManager : EditorWindow
{
    [MenuItem("Tools/Scene Manager")]
    public static void ShowWindow()
    {
        // 打开或聚焦窗口
        EditorWindow.GetWindow<SceneObjectManager>("Scene Manager");
    }

    void OnGUI()
    {
        GUILayout.Label("Scene Object Manager", EditorStyles.boldLabel);

        // 创建按钮来生成不同的对象
        if (GUILayout.Button("Create Environment"))
        {
            CreateObject("-----Environment-----");
            CreateObject("");
        }

        if (GUILayout.Button("Create Gameplay"))
        {
            CreateObject("-----Gameplay-----");
            CreateObject("");
        }

        if (GUILayout.Button("Create UI"))
        {
            CreateObject("-----UI-----");
            CreateObject("");
        }

        if (GUILayout.Button("Create Managers"))
        {
            CreateObject("-----Managers-----");
            CreateObject("");
        }
    }

    private void CreateObject(string name)
    {
        GameObject obj = new GameObject(name);
        // 在这里可以继续为这些对象添加默认组件或进行其他初始化
        obj.transform.SetParent(null); // 你可以调整父子关系
        Undo.RegisterCreatedObjectUndo(obj, "Create " + name);  // 支持撤销操作
    }
}
