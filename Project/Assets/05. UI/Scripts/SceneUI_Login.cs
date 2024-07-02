using TMPro;
using UnityEngine;

public class SceneUI_Login : SceneUI_Base
{
    [SerializeField] TMP_InputField _nicknameInputField;

    void Start()
    {
        _nicknameInputField.text = Managers.Player.Nickname;
    }

    public void OnClickSubmitButton()
    {
        Managers.Player.Nickname = _nicknameInputField.text;

        Managers.Network.JoinLobby();
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
}
