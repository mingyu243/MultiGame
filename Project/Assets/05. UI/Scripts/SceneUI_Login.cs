using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SceneUI_Login : SceneUI_Base
{
    [SerializeField] TMP_InputField _nicknameInputField;

    void Start()
    {
        _nicknameInputField.text = Managers.Player.Nickname;
    }

    public void OnClickOKButton()
    {
        Managers.Player.Nickname = _nicknameInputField.text;

        Scene_Login loginScene = Managers.Scene.CurrentScene as Scene_Login;
        loginScene.NextScene();
    }
}
