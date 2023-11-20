using UnityEngine;

public class GameScene : BaseScene
{
    //UI_GameScene _sceneUI;
    public Canvas Canvas;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Canvas = FindObjectOfType<Canvas>();

        Screen.SetResolution(1980, 1080, true);

        Debug.Log("초기화");

    }

    public override void Clear()
    {
    }
}