using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Scene_Login : Scene_Base
{
    [SerializeField] SceneUI_Login _sceneUI;

    private void Awake()
    {
        SceneType = Define.Scene.Login;
    }

    private void Start()
    {
        // UI에 데이터 삽입.
        _sceneUI.SetNicknameText(Managers.Player.Nickname);
        _sceneUI.BindOkButtonEvent((nickname) =>
        {
            SetNickname(nickname);
            NextScene();
        });
    }

    #region Private Methods

    void SetNickname(string nickname)
    {
        Managers.Player.Nickname = nickname;
    }

    void NextScene()
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

    #endregion
}