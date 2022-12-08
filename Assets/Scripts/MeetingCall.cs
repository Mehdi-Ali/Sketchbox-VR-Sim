using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingCall : MonoBehaviour
{
    [SerializeField] PlayersManager _playersManager;

    private void Awake()
    {
        _playersManager = FindObjectOfType<PlayersManager>();
    }
    public void MeetingTriggered()
    {
        _playersManager.StartMeeting();
    }
}
