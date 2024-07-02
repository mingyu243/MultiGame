using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    public static Managers Instance { get => _instance; set => _instance = value; }

    SceneManagerEx _scene = new SceneManagerEx();
    PlayerManager _player = new PlayerManager();
    NetworkManager _network;

    public static SceneManagerEx Scene => Instance._scene;
    public static PlayerManager Player => Instance._player;
    public static NetworkManager Network => Instance._network;

    void Awake()
    {
        _instance = this;

        _network = this.gameObject.AddComponent<NetworkManager>();
    }
}
