using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    private int nextPrefabIndex = 0;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        // Server tarafýndan çalýþtýrýlýr ve her yeni oyuncu baðlandýðýnda çaðrýlýr
        GameObject playerPrefab = playerPrefabs[nextPrefabIndex];
        nextPrefabIndex = (nextPrefabIndex + 1) % playerPrefabs.Length;

        GameObject playerInstance = Instantiate(playerPrefab);
        NetworkObject networkObject = playerInstance.GetComponent<NetworkObject>();
        networkObject.SpawnAsPlayerObject(clientId, true);
    }

    private void OnDestroy()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }
}