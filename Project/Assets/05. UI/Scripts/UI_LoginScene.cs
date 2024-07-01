using TMPro;
using UnityEngine;

public class UI_LoginScene : BaseSceneUI
{
    [SerializeField] TMP_InputField _nicknameInputField;

    public void OnClickSubmitButton()
    {
        Scene_Login loginScene = (Scene_Login)Managers.Scene.CurrentScene;

        loginScene.SetNickname(_nicknameInputField.text);
        loginScene.LoadNextScene();
    }
}
