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
        // Oyun baþladýðýnda canvas pasif hale getirilir
        gameOverCanvas.SetActive(false);
        // Restart butonuna týklama event'ini baðla
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        ExitButton.onClick.AddListener(ExitButtonCall);
    }

    public void ExitButtonCall()
    {
        Application.Quit();
    }

    // Canvas'ý aktif hale getiren fonksiyon
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
            // Oyun bittiðinde GameOverManager üzerinden canvas'ý aktif hale getirin
            ShowGameOverCanvasClientRpc();
        }
    }

    private void OnRestartButtonClicked()
    {
        if (IsServer)
        {
            // Sunucu tarafýnda tüm oyuncularý yeniden spawn noktasýna gönder
            RestartGameClientRpc();
        }
    }

    [ClientRpc]
    private void RestartGameClientRpc()
    {
        // Debug.Log ekleyelim
        Debug.Log("Restarting game...");

        // Her bir client kendi oyuncusunu spawn noktasýna taþýr
        NetworkObject networkObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();

        if (networkObject != null)
        {
            Debug.Log(networkObject);
            Time.timeScale = 1f;
            networkObject.transform.GetChild(0).position = spawnPoint.position;
            networkObject.transform.GetChild(0).GetComponent<ClientNetworkTransform>().Teleport(spawnPoint.position, Quaternion.identity, new Vector3(17, 17, 17));
            // Debug.Log ile spawn iþleminin gerçekleþtiðini doðrulayalým
            Debug.Log("Player respawned at spawn point.");
        }
        else
        {
            // NetworkObject bulunamadýðýnda bir hata mesajý yazdýralým
            Debug.LogError("NetworkObject not found!");
        }

        // Canvas'ý tekrar kapat
        gameOverCanvas.SetActive(false);
    }
}