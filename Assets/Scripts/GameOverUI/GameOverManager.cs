using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;

public class GameOverManager : NetworkBehaviour
{
    public GameObject gameOverCanvas;
    public Button restartButton;
    public Transform spawnPoint;
    public Button ExitButton;

    private void Start()
    {
        // Oyun ba�lad���nda canvas pasif hale getirilir
        gameOverCanvas.SetActive(false);
        // Restart butonuna t�klama event'ini ba�la
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        ExitButton.onClick.AddListener(ExitButtonCall);
    }

    public void ExitButtonCall()
    {
        Application.Quit();
    }

    // Canvas'� aktif hale getiren fonksiyon
    public void ShowGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
    }

    [ClientRpc]
    private void ShowGameOverCanvasClientRpc()
    {
        ShowGameOverCanvas();
    }

    public void PlayerDied()
    {
        if (IsServer)
        {
            // Oyun bitti�inde GameOverManager �zerinden canvas'� aktif hale getirin
            ShowGameOverCanvasClientRpc();
        }
    }

    private void OnRestartButtonClicked()
    {
        if (IsServer)
        {
            // Sunucu taraf�nda t�m oyuncular� yeniden spawn noktas�na g�nder
            RestartGameClientRpc();
        }
    }

    [ClientRpc]
    private void RestartGameClientRpc()
    {
        // Debug.Log ekleyelim
        Debug.Log("Restarting game...");

        // Her bir client kendi oyuncusunu spawn noktas�na ta��r
        NetworkObject networkObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();

        if (networkObject != null)
        {
            Debug.Log(networkObject);
            Time.timeScale = 1f;
            networkObject.transform.GetChild(0).position = spawnPoint.position;
            networkObject.transform.GetChild(0).GetComponent<ClientNetworkTransform>().Teleport(spawnPoint.position, Quaternion.identity, new Vector3(17, 17, 17));
            // Debug.Log ile spawn i�leminin ger�ekle�ti�ini do�rulayal�m
            Debug.Log("Player respawned at spawn point.");
        }
        else
        {
            // NetworkObject bulunamad���nda bir hata mesaj� yazd�ral�m
            Debug.LogError("NetworkObject not found!");
        }

        // Canvas'� tekrar kapat
        gameOverCanvas.SetActive(false);
    }
}