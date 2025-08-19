using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerChat : NetworkBehaviour
{
    [SerializeField] private Player player;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            StartCoroutine(RegisterWhenReady());
        }
    }

    private System.Collections.IEnumerator RegisterWhenReady()
    {
        while (ChatUI.Instance == null)
            yield return null;

        ChatUI.Instance.RegisterLocalPlayerChat(this);

    }
    public void TrySendMessage(string message)
    {
        if (!IsOwner) return;
        if (string.IsNullOrWhiteSpace(message)) return;

        SendMessageServerRpc(PlayerSettings.playerName, message);
    }

    [ServerRpc]
    private void SendMessageServerRpc(string senderName, string text, ServerRpcParams rpcParams = default)
    {
        ReceiveMessageClientRpc(senderName, text);
    }

    [ClientRpc]
    private void ReceiveMessageClientRpc(string senderName, string text, ClientRpcParams rpcParams = default)
    {
        if (ChatUI.Instance != null)
        {
            ChatUI.Instance.CreateChatMessage(senderName, text);
        }
    }
}
