using System;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;

public class NetworkInitializer : MonoBehaviour, INetworkRunnerCallbacks
{
    public static NetworkInitializer Instance { get; private set; }

    public static Action OnLobbyCompleted;

    [SerializeField] private Transform _spawnPos1, _spawnPos2;
    [SerializeField] private NetworkPrefabRef _playerPrefab;

    private Dictionary<PlayerRef, NetworkObject> _players = new();

    //El Manager que se encarga de todo.
    private NetworkRunner _runner;
    private InputHandler _inputHandler;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void StartHost()
    {
        StartGame(GameMode.Host);
    }

    public void StartClient()
    {
        StartGame(GameMode.Client);
    }

    async private void StartGame(GameMode mode)
    {
        Debug.Log("Starting " + mode);
        UIController.Instance.HideMainMenu();

        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    private bool LobbyCompleted()
    {
        return _players.Count >= 2;
    }

    private bool HasPlayer()
    {
        return _players.Count > 0;
    }

    #region Network Interface

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer && !LobbyCompleted())
        {
            var playerPos = HasPlayer() ? _spawnPos2.position : _spawnPos1.position;
            var playerRot = HasPlayer() ? Quaternion.Euler(0, 0, 180) : Quaternion.identity;
            var networkObject = runner.Spawn(_playerPrefab, playerPos, playerRot, player);

            _players.Add(player, networkObject);
        }
        
        if (LobbyCompleted())
            OnLobbyCompleted?.Invoke();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_players.TryGetValue(player, out NetworkObject playerObject))
        {
            runner.Despawn(playerObject);
            _players.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (!_inputHandler && NetworkPlayer.Local)
        {
            _inputHandler = NetworkPlayer.Local.Handler;
        }

        if (_inputHandler)
        {
            input.Set(_inputHandler.GetInputs());
        }
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    #endregion
}