﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    const int MaxChatMessages = 200;
    public GameObject ChatPanel;
    public List<ChatMessage> chatMessages = new List<ChatMessage>();
    public GameObject TextObject;
    public string cmClour;

    private bool[] Filter = new bool[Enum.GetNames(typeof(ChatFilterType)).Length];

    public GameObject[] FilterObjects = new GameObject[Enum.GetNames(typeof(ChatFilterType)).Length];

    void Awake()
    {
        for (int i = 0; i < Filter.Length; i++)
            Filter[i] = true;
    }

    public void ReceiveChat(string text, ChatType type)
    {
        if (Filtered(type)) return;
        FilterColour(type);
        ChatMessageBody cm = new ChatMessageBody();
        cm.text = "<color=#"+cmClour+">" + text + "</color>";
        cm.type = type;

        ChatMessage newText = Instantiate(TextObject, ChatPanel.transform).GetComponent<ChatMessage>();
        newText.Info = cm;

        chatMessages.Add(newText);

        if (chatMessages.Count > MaxChatMessages)
        {
            Destroy(chatMessages[0].gameObject);
            chatMessages.RemoveAt(0);
        }
    }

    bool Filtered(ChatType type)
    {
       
        switch (type)
        {
            case ChatType.Announcement:
                return !Filter[(int)ChatFilterType.Global];
            case ChatType.Normal:
            case ChatType.Shout:
            case ChatType.Shout2:
            case ChatType.Shout3:
                return !Filter[(int)ChatFilterType.Local];
            case ChatType.Group:
                return !Filter[(int)ChatFilterType.Group];
            case ChatType.Guild:
                return !Filter[(int)ChatFilterType.Guild];
            case ChatType.WhisperIn:
            case ChatType.WhisperOut:
                return !Filter[(int)ChatFilterType.Private];
            case ChatType.System:
            case ChatType.System2:
            case ChatType.Trainer:
            case ChatType.LevelUp:
            case ChatType.Hint:
                return !Filter[(int)ChatFilterType.System];
        }

        return false;
    }

    public void ToggleFilter(int type)
    {
        if (type > Enum.GetNames(typeof(ChatFilterType)).Length) return;

        Filter[type] = !Filter[type];
        FilterObjects[type].SetActive(Filter[type]);
    }
    public string FilterColour(ChatType type)
    {
        
        switch (type)
        {
            case ChatType.Announcement:
                return  cmClour = "9C5100";
            case ChatType.Normal:
                return cmClour = "FFFFFF";
            case ChatType.Shout:
            case ChatType.Shout2:
            case ChatType.Shout3:
                return cmClour = "B54549";
            case ChatType.Group:
                return cmClour = "00199C";
            case ChatType.Guild:
                return cmClour = "9C002F"; 
            case ChatType.WhisperIn:
            case ChatType.WhisperOut:
                return cmClour = "9144B5";
            case ChatType.System:
            case ChatType.System2:
            case ChatType.Trainer:
            case ChatType.LevelUp:
            case ChatType.Hint:
                return cmClour = "9B9C00"; ;
        }

        return cmClour = "FFFFFF"; 
    }

}
