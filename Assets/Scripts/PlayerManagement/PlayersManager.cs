using System;
using Normal.Realtime;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    private PlayersManagerSync _playerManagerSync;
    private RealtimeAvatarManager _realtimeManager;
    private XROrigin _xROrigin;
    private uint _localPlayerId;

    public bool TriggerMeeting;
    private bool _isConfected;

    [SerializeField] private Transform _table;



    private void Awake()
    {
        _playerManagerSync = GetComponent<PlayersManagerSync>();
        _realtimeManager = FindObjectOfType<RealtimeAvatarManager>();
        _xROrigin = FindObjectOfType<XROrigin>();

        _realtimeManager.avatarCreated += AvatarCreated;
        _realtimeManager.avatarDestroyed += AvatarDestroyed;

        _isConfected = false;
    }

    private void AvatarCreated(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
    {
        if (avatar.realtimeView.isOwnedLocallySelf)
        {
            _localPlayerId = _playerManagerSync.AddPlayerToDict();
            _isConfected = true;
        }
    }

    private void AvatarDestroyed(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
    {
        if (avatar.realtimeView.isOwnedLocallySelf)
        {
            _playerManagerSync.RemovePlayerToDict(_localPlayerId);
            _isConfected = false;
        }
    }

    private void Update()
    {
        if (TriggerMeeting && _playerManagerSync.IfIsInstructor(_localPlayerId))
            StartMeeting();

        if (!_isConfected)
            return;

        if (_playerManagerSync.MeetingStatus(_localPlayerId))
                OmMeetingStart();
    }

    private void StartMeeting()
    {
        _playerManagerSync.StartMeeting();
    }


    private void OmMeetingStart()
    {
        var xROTrans = _xROrigin.transform;
        TriggerMeeting = false ;

        xROTrans.position = _playerManagerSync.GetPosition(_localPlayerId);
        if (_playerManagerSync.IfIsInstructor(_localPlayerId))
            xROTrans.LookAt(_table.position);
        else
            xROTrans.LookAt(_playerManagerSync.GetInstructorPosition());

    }
}
