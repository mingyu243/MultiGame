using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomListElement : MonoBehaviour
{
    [SerializeField] TMP_Text _roomNameText;
    [SerializeField] TMP_Text _playerCountText;
    [SerializeField] Button _button;

    public void SetRoomNameText(string roomName)
    {
        _roomNameText.text = roomName;
    }

    public void SetPlayerCountText(int current, int max)
    {
        _playerCountText.text = $"{current} / {max}";
    }

    public void BindButtonEvent(UnityAction onClicked)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(onClicked);
    }
}
