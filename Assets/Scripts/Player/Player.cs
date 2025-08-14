using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using UnityEditor;

public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerChat 
    public float moveSpeed = 5f;
    public NetworkVariable<FixedString32Bytes> playerNName = new
        NetworkVariable<FixedString32Bytes>(value: "",
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) return;
        if (PlayerSettings.PlayerName.Length <= 0)
        {
            Debug.Log("Cannot assign an empty name to player");
        }
        playerNName.Value = PlayerSettings.PlayerName;
        Debug.Log($"This player name is {playerNName.Value}");
    }


    private void Update()
    {
        // Only process input for the local player
        if (!IsOwner) return;

        Vector3 input = new Vector3(
            Input.GetAxis("Horizontal"),
            0f,
            Input.GetAxis("Vertical")
        );


        Vector3 move = input * moveSpeed * Time.deltaTime;

        // Send the movement to the server
        MoveServerRpc(move);
    }

    [ServerRpc]
    private void MoveServerRpc(Vector3 move, ServerRpcParams rpcParams = default)
    {
        // Apply movement on the server
        transform.position += move;
    }
}
