using UnityEngine;
using UnityEditor;

public class SceneObjectManager : EditorWindow
{
    [MenuItem("Tools/Scene Manager")]
    public static void ShowWindow()
    {
        // �򿪻�۽�����
        EditorWindow.GetWindow<SceneObjectManager>("Scene Manager");
    }

    void OnGUI()
    {
        GUILayout.Label("Scene Object Manager", EditorStyles.boldLabel);

        // ������ť�����ɲ�ͬ�Ķ���
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
        // ��������Լ���Ϊ��Щ�������Ĭ����������������ʼ��
        obj.transform.SetParent(null); // ����Ե������ӹ�ϵ
        Undo.RegisterCreatedObjectUndo(obj, "Create " + name);  // ֧�ֳ�������
    }
}
