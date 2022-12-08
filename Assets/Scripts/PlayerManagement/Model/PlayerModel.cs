using System.Security.Cryptography;
using Normal.Realtime;
using UnityEngine;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class PlayerModel
{
    [RealtimeProperty(1, true, false)] 
    private Vector3 _meetingPosition;

    [RealtimeProperty(2, true, false)] 
    private bool _isInstructor;

    [RealtimeProperty(3, true, false)] 
    private bool _isInMeeting;

    //try to use the constructor and see if it works.
    // PlayerModel(Vector3 position, bool isInstructor, bool isInMeeting)
    // {
    //     _meetingPosition = position ;
    //     _isInstructor = isInstructor;
    //     _isInMeeting = isInMeeting;
    // }
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class PlayerModel : RealtimeModel {
    public UnityEngine.Vector3 meetingPosition {
        get {
            return _meetingPositionProperty.value;
        }
        set {
            if (_meetingPositionProperty.value == value) return;
            _meetingPositionProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public bool isInstructor {
        get {
            return _isInstructorProperty.value;
        }
        set {
            if (_isInstructorProperty.value == value) return;
            _isInstructorProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public bool isInMeeting {
        get {
            return _isInMeetingProperty.value;
        }
        set {
            if (_isInMeetingProperty.value == value) return;
            _isInMeetingProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public enum PropertyID : uint {
        MeetingPosition = 1,
        IsInstructor = 2,
        IsInMeeting = 3,
    }
    
    #region Properties
    
    private ReliableProperty<UnityEngine.Vector3> _meetingPositionProperty;
    
    private ReliableProperty<bool> _isInstructorProperty;
    
    private ReliableProperty<bool> _isInMeetingProperty;
    
    #endregion
    
    public PlayerModel() : base(null) {
        _meetingPositionProperty = new ReliableProperty<UnityEngine.Vector3>(1, _meetingPosition);
        _isInstructorProperty = new ReliableProperty<bool>(2, _isInstructor);
        _isInMeetingProperty = new ReliableProperty<bool>(3, _isInMeeting);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _meetingPositionProperty.UnsubscribeCallback();
        _isInstructorProperty.UnsubscribeCallback();
        _isInMeetingProperty.UnsubscribeCallback();
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _meetingPositionProperty.WriteLength(context);
        length += _isInstructorProperty.WriteLength(context);
        length += _isInMeetingProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _meetingPositionProperty.Write(stream, context);
        writes |= _isInstructorProperty.Write(stream, context);
        writes |= _isInMeetingProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.MeetingPosition: {
                    changed = _meetingPositionProperty.Read(stream, context);
                    break;
                }
                case (uint) PropertyID.IsInstructor: {
                    changed = _isInstructorProperty.Read(stream, context);
                    break;
                }
                case (uint) PropertyID.IsInMeeting: {
                    changed = _isInMeetingProperty.Read(stream, context);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _meetingPosition = meetingPosition;
        _isInstructor = isInstructor;
        _isInMeeting = isInMeeting;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
