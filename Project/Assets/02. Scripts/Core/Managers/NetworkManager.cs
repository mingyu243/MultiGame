using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    NetworkRunner _runner;
    public NetworkRunner Runner { get => _runner; set => _runner = value; }

    string _lobbyName = "default";

    // 방 목록 캐싱.
    List<SessionInfo> _sessionList;
    public List<SessionInfo> SessionList { get => _sessionList; set => _sessionList = value; }

    private void Awake()
    {
        if (Runner == null)
        {
            Runner = this.gameObject.AddComponent<NetworkRunner>();
        }
    }

    /// <summary>
    /// 세션들이 존재하는 로비로 입장.
    /// 로비도 여러 개 존재할 수 있지만, 이 프로젝트에선 하나로 할 예정.
    /// </summary>
    public async UniTask<bool> JoinLobbyAsync()
    {
        StartGameResult result = await Runner.JoinSessionLobby(SessionLobby.Shared, _lobbyName);
        
        if (result.Ok)
        {
            // all good.
            return true;
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
            return false;
        }
    }

    /// <summary>
    /// 해당 세션으로 입장.
    /// </summary>
    public async UniTask<bool> CreateOrJoinSessionAsync(string sessionName)
    {
        StartGameResult result = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = sessionName,
            Scene = SceneRef.FromIndex((int)Define.Scene.WaitingRoom)
        });

        if (result.Ok)
        {
            // all good.
            return true;
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
            return false;
        }
    }

    /// <summary>
    /// 랜덤 방으로 입장.
    /// 디폴트 값은 가장 오래된 방부터 입장.
    /// </summary>
    public async UniTask<bool> CreateOrJoinRandomSessionAsync()
    {
        StartGameResult result = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
        });

        if (result.Ok)
        {
            // all good.
            return true;
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
            return false;
        }
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
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log("OnSessionListUpdated");
        SessionList = sessionList;
    }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) => Debug.Log("OnShutdown");
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) => Debug.Log("OnUserSimulationMessage");
}
