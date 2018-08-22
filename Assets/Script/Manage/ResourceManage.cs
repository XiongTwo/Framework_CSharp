using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ResourceManage {

    private static ResourceManage instance;
    public static ResourceManage Instance
    {
        get
        {
            if (instance == null)
                instance = new ResourceManage();
            return instance;
        }
    }

    private GameObject mRoot;

    public GameObject LoadPrefab(string path, GameObject parent = null)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (mRoot == null)
                mRoot = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resource/Prefab/Root.prefab"));
            if (parent == null)
                return Common.AddChild(mRoot, AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resource/Prefab/" + path + ".prefab"));
            if (parent != null)
                return Common.AddChild(parent, AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resource/Prefab/" + path + ".prefab"));
        }
        return null;
    }

    public string LoadTextFile(string path,string name)
    {
        if (!File.Exists(path+ name))
            return null;
        return File.ReadAllText(path+ name);
    }
    public void SaveTextFile(string path, string name, string text)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        if (File.Exists(path+ name))
            File.Delete(path+ name);
        StreamWriter streamWriter = File.CreateText(path+ name);
        streamWriter.Write(text);
        streamWriter.Close();
        streamWriter.Dispose();
    }
}
