using Normal.Realtime;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GatherEvent : RealtimeComponent<GatherEventModel>
{
    [SerializeField]
    private XROrigin _player = default;

    protected override void OnRealtimeModelReplaced(GatherEventModel previousModel, GatherEventModel currentModel)
    {
        if (previousModel != null) 
            previousModel.eventDidFire -= EventDidFire;

        if (currentModel != null)
            currentModel.eventDidFire += EventDidFire;
    }

    public void Move(Vector3 position)
    {
        model.FireEvent(realtime.clientID, transform.position);
    }

    private void EventDidFire(int senderID, Vector3 position)
    {
        _player.transform.position = position;
        Debug.Log("triggered");
    }
}
