using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI_Lobby : SceneUI_Base
{
    [SerializeField] TMP_Text _nicknameText;

    [SerializeField] TMP_InputField _joinOrCreateRoomNameInputField;

    [SerializeField] Transform _roomlistElementParent;
    [SerializeField] GameObject _roomlistElementPrefab;
    List<GameObject> _roomListElements = new();

    void Start()
    {
        _nicknameText.text = Managers.Player.Nickname;
        Managers.Pool.Register("RoomListElement", _roomlistElementPrefab);
    }

    /// <summary>
    /// 포톤 문서에 방 목록을 보여주는 기능은 권장하지 않는다고 되어 있음.
    /// 나는 방이 몇개 없을거니까 상관 없을듯.
    /// [참고] https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/matchmaking
    /// </summary>
    public void UpdateRoomList(List<SessionInfo> sessionList)
    {
        // 기존에 있던 것 삭제.
        for (int i = 0; i < _roomListElements.Count; i++)
        {
            Managers.Pool.Release("RoomListElement", _roomListElements[i]);
        }
        _roomListElements.Clear();

        // 생성.
        foreach (SessionInfo session in sessionList)
        {
            GameObject go = Managers.Pool.Get("RoomListElement");
            go.transform.SetParent(_roomlistElementParent, false);
            _roomListElements.Add(go);

            RoomListElement element = go.GetComponent<RoomListElement>();
            element.SetRoomNameText(session.Name);
            element.SetPlayerCountText(session.PlayerCount, session.MaxPlayers);
            element.BindButtonEvent(() => CreateOrJoinRoom(session.Name));
        }
    }

    public void OnClickCreateOrJoinRoomButton()
    {
        CreateOrJoinRoom(_joinOrCreateRoomNameInputField.text);
    }

    public void OnClickCreateOrJoinRandomRoomButton()
    {
        Scene_Lobby lobbyScene = Managers.Scene.CurrentScene as Scene_Lobby;
        lobbyScene.CreateOrJoinRandomRoom();
    }

    void CreateOrJoinRoom(string roomName)
    {
        Scene_Lobby lobbyScene = Managers.Scene.CurrentScene as Scene_Lobby;
        lobbyScene.CreateOrJoinRoom(roomName);
    }
}
