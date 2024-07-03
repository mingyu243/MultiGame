using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Scene_Lobby : Scene_Base, INetworkRunnerCallbacks
{
    [SerializeField] SceneUI_Lobby _sceneUI;

    void Awake()
    {
        SceneType = Define.Scene.Lobby;

        Managers.Network.Runner.AddCallbacks(this);
    }

    void Start()
    {
        // UI에 데이터 삽입.
        _sceneUI.SetNicknameText(Managers.Player.Nickname);
        _sceneUI.BindCreateOrJoinSessionButtonEvent(CreateOrJoinSession);
        _sceneUI.BindCreateOrJoinRandomSessionButtonEvent(CreateOrJoinRandomSession);

        // 처음에는 수동으로 방 목록을 업데이트 해줘야 함.
        _sceneUI.UpdateSessionList(Managers.Network.SessionList, CreateOrJoinSession);
    }

    private void OnDestroy()
    {
        Managers.Network.Runner.RemoveCallbacks(this);
    }

    void CreateOrJoinSession(string sessionName)
    {
        Managers.Network.CreateOrJoinSessionAsync(sessionName).Forget();
    }

    void CreateOrJoinRandomSession()
    {
        Managers.Network.CreateOrJoinRandomSessionAsync().Forget();
    }

    #region INetworkRunnerCallbacks

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        _sceneUI.UpdateSessionList(sessionList, CreateOrJoinSession);
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
