using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class TrailSyncModel
{
    [RealtimeProperty(1, true, true)]
    private Vector3 _startPoint;

    [RealtimeProperty(2, true, true)]
    private Vector3 _endPoint;

}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class TrailSyncModel : RealtimeModel
{
    public UnityEngine.Vector3 startPoint {
        get {
            return _startPointProperty.value;
        }
        set {
            if (_startPointProperty.value == value) return;
            _startPointProperty.value = value;
            InvalidateReliableLength();
            FireStartPointDidChange(value);
        }
    }
    
    public UnityEngine.Vector3 endPoint {
        get {
            return _endPointProperty.value;
        }
        set {
            if (_endPointProperty.value == value) return;
            _endPointProperty.value = value;
            InvalidateReliableLength();
            FireEndPointDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(TrailSyncModel model, T value);
    public event PropertyChangedHandler<UnityEngine.Vector3> startPointDidChange;
    public event PropertyChangedHandler<UnityEngine.Vector3> endPointDidChange;
    
    public enum PropertyID : uint {
        StartPoint = 1,
        EndPoint = 2,
    }
    
    #region Properties
    
    private ReliableProperty<UnityEngine.Vector3> _startPointProperty;
    
    private ReliableProperty<UnityEngine.Vector3> _endPointProperty;
    
    #endregion
    
    public TrailSyncModel() : base(null) {
        _startPointProperty = new ReliableProperty<UnityEngine.Vector3>(1, _startPoint);
        _endPointProperty = new ReliableProperty<UnityEngine.Vector3>(2, _endPoint);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _startPointProperty.UnsubscribeCallback();
        _endPointProperty.UnsubscribeCallback();
    }
    
    private void FireStartPointDidChange(UnityEngine.Vector3 value) {
        try {
            startPointDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireEndPointDidChange(UnityEngine.Vector3 value) {
        try {
            endPointDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _startPointProperty.WriteLength(context);
        length += _endPointProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _startPointProperty.Write(stream, context);
        writes |= _endPointProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.StartPoint: {
                    changed = _startPointProperty.Read(stream, context);
                    if (changed) FireStartPointDidChange(startPoint);
                    break;
                }
                case (uint) PropertyID.EndPoint: {
                    changed = _endPointProperty.Read(stream, context);
                    if (changed) FireEndPointDidChange(endPoint);
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
        _startPoint = startPoint;
        _endPoint = endPoint;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
