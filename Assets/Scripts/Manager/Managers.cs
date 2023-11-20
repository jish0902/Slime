using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Managers : MonoBehaviour
{
    private static Managers s_instance = new();
    
    private readonly List<Tuple<Action, short>> _actions = new();
    private readonly object _lock = new();

    public static Managers Instance
    {
        get
        {
            Init();
            return s_instance;
        }
    }

    private void Awake()
    {
        Init();
    }



    public static void Init()
    {
        if (s_instance == null)
        {
            var go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            //s_instance._data.Init();

            s_instance._pool.Init();
            s_instance._Data.Init();
            //s_instance._sound.Init();
        }
    }

    public static void Clear()
    {
        //Sound.Clear();
        Scene.Clear();
        //UI.Clear();
        Pool.Clear();
    }


    #region Contents

    private readonly DataManager _Data = new();

    public static DataManager Data => Instance._Data;

    #endregion


    #region Core

    private readonly PoolManager _pool = new();
    private readonly SceneManagerEx _scene = new();
    private readonly ResourceManager _resource = new();

    public static PoolManager Pool => Instance._pool;
    public static ResourceManager Resource => Instance._resource;
    public static SceneManagerEx Scene => Instance._scene;

    #endregion
}