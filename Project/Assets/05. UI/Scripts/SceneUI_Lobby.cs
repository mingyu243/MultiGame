using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneUI_Lobby : SceneUI_Base
{
    [SerializeField] TMP_Text _nicknameText;

    [SerializeField] TMP_InputField _createOrJoinSessionNameInputField;
    [SerializeField] Button _createOrJoinSessionButton;

    [SerializeField] Button _createOrJoinRandomSessionButton;

    [SerializeField] Transform _sessionlistElementParent;
    [SerializeField] GameObject _sessionlistElementPrefab;
    List<GameObject> _sessionListElements = new();

    void Start()
    {
        // 풀링.
        Managers.Pool.Register("SessionListElement", _sessionlistElementPrefab);
    }

    public void SetNicknameText(string nickname)
    {
        _nicknameText.text = nickname;
    }

    /// <summary>
    /// 포톤 문서에 방 목록을 보여주는 기능은 권장하지 않는다고 되어 있음.
    /// 나는 방이 몇개 없을거니까 상관 없을듯.
    /// [참고] https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/matchmaking
    /// </summary>
    public void UpdateSessionList(List<SessionInfo> sessionList, Action<string> onClicked)
    {
        // 기존에 있던 것 삭제.
        for (int i = 0; i < _sessionListElements.Count; i++)
        {
            Managers.Pool.Release("SessionListElement", _sessionListElements[i]);
        }
        _sessionListElements.Clear();

        // 생성.
        foreach (SessionInfo session in sessionList)
        {
            GameObject go = Managers.Pool.Get("SessionListElement");
            go.transform.SetParent(_sessionlistElementParent, false);
            _sessionListElements.Add(go);

            SessionListElement element = go.GetComponent<SessionListElement>();
            element.SetSessionNameText(session.Name);
            element.SetPlayerCountText(session.PlayerCount, session.MaxPlayers);
            element.BindButtonEvent(() => onClicked?.Invoke(session.Name));
        }
    }

    public void BindCreateOrJoinSessionButtonEvent(Action<string> onClicked)
    {
        _createOrJoinSessionButton.onClick.AddListener(() =>
        {
            onClicked?.Invoke(_createOrJoinSessionNameInputField.text);
        });
    }

    public void BindCreateOrJoinRandomSessionButtonEvent(Action onClicked)
    {
        _createOrJoinRandomSessionButton.onClick.AddListener(() =>
        {
            onClicked?.Invoke();
        });
    }
}
