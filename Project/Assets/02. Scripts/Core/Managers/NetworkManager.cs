using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    NetworkRunner _runner;
    public NetworkRunner Runner { get => _runner; set => _runner = value; }

    string _lobbyName = "default";

    private void Awake()
    {
        if (Runner == null)
        {
            Runner = this.gameObject.AddComponent<NetworkRunner>();
        }
    }

    public void JoinLobby()
    {
        Runner.JoinSessionLobby(SessionLobby.Shared, _lobbyName);
    }

    public async UniTask JoinRoomAsync(string sessionName)
    {
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = sessionName,
        });
    }

    public void OnConnectedToServer(NetworkRunner runner) => Debug.Log("OnConnectedToServer");
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) => Debug.Log("OnConnectFailed");
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) => Debug.Log("OnConnectRequest");
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) => Debug.Log("OnCustomAuthenticationResponse");
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) => Debug.Log("OnDisconnectedFromServer");
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) => Debug.Log("OnHostMigration");
    public void OnInput(NetworkRunner runner, NetworkInput input) => Debug.Log("OnInput");
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) => Debug.Log("OnInputMissing");
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) => Debug.Log("OnObjectEnterAOI");
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) => Debug.Log("OnObjectExitAOI");
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) => Debug.Log("OnPlayerJoined");
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) => Debug.Log("OnPlayerLeft");
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) => Debug.Log("OnReliableDataProgress");
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) => Debug.Log("OnReliableDataReceived");
    public void OnSceneLoadDone(NetworkRunner runner) => Debug.Log("OnSceneLoadDone");
    public void OnSceneLoadStart(NetworkRunner runner) => Debug.Log("OnSceneLoadStart");
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) => Debug.Log("OnSessionListUpdated");
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) => Debug.Log("OnShutdown");
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) => Debug.Log("OnUserSimulationMessage");
}
