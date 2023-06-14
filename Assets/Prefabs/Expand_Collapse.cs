using UnityEditor;
using UnityEngine;

public class Expand_Collapse : EditorWindow
{
     [MenuItem("Custom/Collapse All Children")]
    private static void CollapseAllChildren()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        foreach (var selectedObject in selectedObjects)
        {
            CollapseRecursive(selectedObject);
        }
    }

    [MenuItem("Custom/Expand All Children")]
    private static void ExpandAllChildren()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        foreach (var selectedObject in selectedObjects)
        {
            ExpandRecursive(selectedObject);
        }
    }

    private static void CollapseRecursive(GameObject gameObject)
    {
        var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        var methodInfo = type.GetMethod("SetExpandedRecursive");
        var window = EditorWindow.GetWindow(type);
        methodInfo.Invoke(window, new object[] { gameObject.GetInstanceID(), false });
    }

    private static void ExpandRecursive(GameObject gameObject)
    {
        var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        var methodInfo = type.GetMethod("SetExpandedRecursive");
        var window = EditorWindow.GetWindow(type);
        methodInfo.Invoke(window, new object[] { gameObject.GetInstanceID(), true });
    }


    [MenuItem("Custom/Select Immediate Children")]
    private static void SelectImmediateChildren()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        Selection.objects = new GameObject[0];

        foreach (var selectedObject in selectedObjects)
        {
            int childCount = selectedObject.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = selectedObject.transform.GetChild(i);
                Selection.objects = AddObjectToArray(Selection.objects, child.gameObject);
            }
        }
    }

    private static Object[] AddObjectToArray(Object[] array, Object obj)
    {
        int length = array.Length;
        Object[] newArray = new Object[length + 1];
        array.CopyTo(newArray, 0);
        newArray[length] = obj;
        return newArray;
    }

    private float heightToMove = 2.0f;

     [MenuItem("Custom/Move Objects By Height")]
    private static void OpenWindow()
    {
        GetWindow<Expand_Collapse>().Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Height to Move");
        heightToMove = EditorGUILayout.FloatField(heightToMove);

        
        if (GUILayout.Button("Move Objects"))
        {
            MoveSelectedObjects(heightToMove);
        }
    }

    private static void MoveSelectedObjects(float heightToMove)
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject selectedObject in selectedObjects)
        {
            Undo.RecordObject(selectedObject.transform, "Move Object");
            selectedObject.transform.position -= Vector3.up * heightToMove;
        }
    }
    }


