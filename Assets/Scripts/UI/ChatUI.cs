using TMPro;
using UnityEngine;

public class ChatUI : MonoBehaviour
{
    public static ChatUI Instance { get; private set; }

    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private GameObject chatMsgPrefab;
    [SerializeField] private Transform content;

    [SerializeField] private PlayerChat localPlayerChat;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (chatInput != null)
        {
            chatInput.onSubmit.AddListener(OnSubmitMessage);
        }
    }

    public void RegisterLocalPlayerChat(PlayerChat chat)
    {
        localPlayerChat = chat;
    }


    private void OnSubmitMessage(string text)
    {
        if (localPlayerChat != null)
        {
            localPlayerChat.TrySendMessage(text);
            chatInput.text = string.Empty;
            chatInput.ActivateInputField();
        }
    }

    public void CreateChatMessage(string senderName, string message)
    {
        var go = Instantiate(chatMsgPrefab, content);
        var textComp = go.GetComponent<TMP_Text>();
        if (textComp != null)
        {
            textComp.text = $"{senderName}: {message}";
        }
    }
}