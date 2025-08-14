using UnityEngine;
using TMPro:
public class ChatUI : MonoBehaviour
{
    public static ChatUI instance { private set; get; }
   [SerializeField] private TMP_InputField ChatInput;
    [SerializeField] private GameObject chatMsgPrefab;
    [SerializeField] private Transform content;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void CreateChatMessage(string name, msg)
    {
        var chatMsgObject = Instantiate(chatMsgPrefab);,content, false); chatMsgObject.GetComponent TMP_Text> ().text = $"[{name}:{Msg}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
