using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Login : BaseScene
{
    public void SetNickname(string nickname)
    {
        Managers.Player.Nickname = nickname;
    }

    public void LoadNextScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
}
