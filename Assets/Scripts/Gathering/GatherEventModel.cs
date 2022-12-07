
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class GatherEventModel
{
    [RealtimeProperty(1, true)] private int _trigger;
    [RealtimeProperty(2, true)] private int _senderID;
    [RealtimeProperty(3, true)] private Vector3 _position;


    public void FireEvent(int senderID, Vector3 position)
    {
        this.trigger++;
        this.senderID = senderID;
        this.position = position;
    }

    public delegate void EventHandler(int senderID, Vector3 position);
    public event EventHandler eventDidFire;

    [RealtimeCallback(RealtimeModelEvent.OnDidRead)]
    private void DidRead() {
        if (eventDidFire != null && trigger != 0)
            eventDidFire(senderID, position);
    }
    
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class GatherEventModel : RealtimeModel {
    public int trigger {
        get {
            return _triggerProperty.value;
        }
        set {
            if (_triggerProperty.value == value) return;
            _triggerProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public int senderID {
        get {
            return _senderIDProperty.value;
        }
        set {
            if (_senderIDProperty.value == value) return;
            _senderIDProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public UnityEngine.Vector3 position {
        get {
            return _positionProperty.value;
        }
        set {
            if (_positionProperty.value == value) return;
            _positionProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public enum PropertyID : uint {
        Trigger = 1,
        SenderID = 2,
        Position = 3,
    }
    
    #region Properties
    
    private ReliableProperty<int> _triggerProperty;
    
    private ReliableProperty<int> _senderIDProperty;
    
    private ReliableProperty<UnityEngine.Vector3> _positionProperty;
    
    #endregion
    
    public GatherEventModel() : base(null) {
        _triggerProperty = new ReliableProperty<int>(1, _trigger);
        _senderIDProperty = new ReliableProperty<int>(2, _senderID);
        _positionProperty = new ReliableProperty<UnityEngine.Vector3>(3, _position);
        
        SubscribeEventCallback(Normal.Realtime.RealtimeModelEvent.OnDidRead, DidRead);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _triggerProperty.UnsubscribeCallback();
        _senderIDProperty.UnsubscribeCallback();
        _positionProperty.UnsubscribeCallback();
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _triggerProperty.WriteLength(context);
        length += _senderIDProperty.WriteLength(context);
        length += _positionProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _triggerProperty.Write(stream, context);
        writes |= _senderIDProperty.Write(stream, context);
        writes |= _positionProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.Trigger: {
                    changed = _triggerProperty.Read(stream, context);
                    break;
                }
                case (uint) PropertyID.SenderID: {
                    changed = _senderIDProperty.Read(stream, context);
                    break;
                }
                case (uint) PropertyID.Position: {
                    changed = _positionProperty.Read(stream, context);
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
        _trigger = trigger;
        _senderID = senderID;
        _position = position;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
