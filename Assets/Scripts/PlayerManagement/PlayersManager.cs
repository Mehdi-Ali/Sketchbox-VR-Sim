using System;
using Normal.Realtime;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    private PlayersManagerSync _playerManagerSync;
    private RealtimeAvatarManager _realtimeManager;
    private XROrigin _xROrigin;
    private Camera _camera;
    private uint _localPlayerId;

    public bool TriggerMeeting;
    private bool _isConfected;

    [SerializeField] private Transform _table;



    private void Awake()
    {
        _playerManagerSync = GetComponent<PlayersManagerSync>();
        _realtimeManager = FindObjectOfType<RealtimeAvatarManager>();
        _xROrigin = FindObjectOfType<XROrigin>();
        _camera = _xROrigin.GetComponentInChildren<Camera>();

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
        if (!_isConfected)
            return;

        // should be driven from the OnChange IsOnMeeting Event
        if (_playerManagerSync.MeetingStatus(_localPlayerId))
                OmMeetingStart();
    }

    public void StartMeeting()
    {
        if (_playerManagerSync.IfIsInstructor(_localPlayerId))
            _playerManagerSync.StartMeeting();
    }


    private void OmMeetingStart()
    {
        var xROTrans = _xROrigin.transform;
        Vector3 LookAtPosition = default ; 
        TriggerMeeting = false ;

        xROTrans.position = _playerManagerSync.GetPosition(_localPlayerId);
        
        if (_playerManagerSync.IfIsInstructor(_localPlayerId))
            LookAtPosition = _table.position;

        else
            LookAtPosition = _playerManagerSync.GetInstructorPosition();

        xROTrans.LookAt(LookAtPosition);
        _camera.transform.LookAt(LookAtPosition);
    }
}
