using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Scene_Lobby : Scene_Base, INetworkRunnerCallbacks
{
    void Awake()
    {
        SceneType = Define.Scene.Lobby;

        Managers.Network.Runner.AddCallbacks(this);
    }

    void Start()
    {
        // 처음에는 수동으로 방 목록을 업데이트 해줘야 함.
        SceneUI_Lobby lobbySceneUI = Managers.UI.CurrentSceneUI as SceneUI_Lobby;
        lobbySceneUI.UpdateRoomList(Managers.Network.SessionList);
    }

    private void OnDestroy()
    {
        Managers.Network.Runner.RemoveCallbacks(this);
    }

    public void CreateOrJoinRoom(string roomName)
    {
        CreateOrJoinRoomAsync().Forget();

        async UniTask CreateOrJoinRoomAsync()
        {
            bool isJoin = await Managers.Network.CreateOrJoinRoomAsync(roomName);
            if (isJoin)
            {
            }
        }
    }

    public void CreateOrJoinRandomRoom()
    {
        CreateOrJoinRandomRoomAsync().Forget();

        async UniTask CreateOrJoinRandomRoomAsync()
        {
            bool isJoin = await Managers.Network.CreateOrJoinRandomRoomAsync();
            if (isJoin)
            {
            }
        }
    }

    #region INetworkRunnerCallbacks

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) 
    {
        SceneUI_Lobby lobbySceneUI = Managers.UI.CurrentSceneUI as SceneUI_Lobby;
        lobbySceneUI.UpdateRoomList(sessionList);
    }

    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    #endregion
}
