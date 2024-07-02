using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;

public class Scene_Login : Scene_Base
{
    private void Awake()
    {
        SceneType = Define.Scene.Login;
    }

    public void NextScene()
    {
        NextSceneAsync().Forget();

        async UniTask NextSceneAsync()
        {
            bool isJoin = await Managers.Network.JoinLobbyAsync();
            if (isJoin)
            {
                Managers.Scene.LoadScene(Define.Scene.Lobby);
            }
        }
    }


}