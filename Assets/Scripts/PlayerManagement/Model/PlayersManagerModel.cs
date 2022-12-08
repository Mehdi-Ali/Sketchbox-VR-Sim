using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class PlayersManagerModel
{
    [RealtimeProperty(1, true, false)] 
    private RealtimeDictionary<PlayerModel> _players;

    [RealtimeProperty(2, true, false)] 
    private uint _lastID = 0;

    [RealtimeProperty(3, true, false)] 
    private int _gatheringPointsIndex = 0;

    [RealtimeProperty(4, true, false)] 
    private bool _instructorAssigned = false;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class PlayersManagerModel : RealtimeModel {
    public uint lastID {
        get {
            return _lastIDProperty.value;
        }
        set {
            if (_lastIDProperty.value == value) return;
            _lastIDProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public int gatheringPointsIndex {
        get {
            return _gatheringPointsIndexProperty.value;
        }
        set {
            if (_gatheringPointsIndexProperty.value == value) return;
            _gatheringPointsIndexProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public bool instructorAssigned {
        get {
            return _instructorAssignedProperty.value;
        }
        set {
            if (_instructorAssignedProperty.value == value) return;
            _instructorAssignedProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public Normal.Realtime.Serialization.RealtimeDictionary<PlayerModel> players {
        get => _players;
    }
    
    public enum PropertyID : uint {
        Players = 1,
        LastID = 2,
        GatheringPointsIndex = 3,
        InstructorAssigned = 4,
    }
    
    #region Properties
    
    private ModelProperty<Normal.Realtime.Serialization.RealtimeDictionary<PlayerModel>> _playersProperty;
    
    private ReliableProperty<uint> _lastIDProperty;
    
    private ReliableProperty<int> _gatheringPointsIndexProperty;
    
    private ReliableProperty<bool> _instructorAssignedProperty;
    
    #endregion
    
    public PlayersManagerModel() : base(null) {
        RealtimeModel[] childModels = new RealtimeModel[1];
        
        _players = new Normal.Realtime.Serialization.RealtimeDictionary<PlayerModel>();
        childModels[0] = _players;
        
        SetChildren(childModels);
        
        _playersProperty = new ModelProperty<Normal.Realtime.Serialization.RealtimeDictionary<PlayerModel>>(1, _players);
        _lastIDProperty = new ReliableProperty<uint>(2, _lastID);
        _gatheringPointsIndexProperty = new ReliableProperty<int>(3, _gatheringPointsIndex);
        _instructorAssignedProperty = new ReliableProperty<bool>(4, _instructorAssigned);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _lastIDProperty.UnsubscribeCallback();
        _gatheringPointsIndexProperty.UnsubscribeCallback();
        _instructorAssignedProperty.UnsubscribeCallback();
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _playersProperty.WriteLength(context);
        length += _lastIDProperty.WriteLength(context);
        length += _gatheringPointsIndexProperty.WriteLength(context);
        length += _instructorAssignedProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _playersProperty.Write(stream, context);
        writes |= _lastIDProperty.Write(stream, context);
        writes |= _gatheringPointsIndexProperty.Write(stream, context);
        writes |= _instructorAssignedProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.Players: {
                    changed = _playersProperty.Read(stream, context);
                    break;
                }
                case (uint) PropertyID.LastID: {
                    changed = _lastIDProperty.Read(stream, context);
                    break;
                }
                case (uint) PropertyID.GatheringPointsIndex: {
                    changed = _gatheringPointsIndexProperty.Read(stream, context);
                    break;
                }
                case (uint) PropertyID.InstructorAssigned: {
                    changed = _instructorAssignedProperty.Read(stream, context);
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
        _players = players;
        _lastID = lastID;
        _gatheringPointsIndex = gatheringPointsIndex;
        _instructorAssigned = instructorAssigned;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */