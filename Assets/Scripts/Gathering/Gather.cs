using Normal.Realtime;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Gather : MonoBehaviour
{
    [SerializeField] private GatherEvent _gatherEvent;

    public bool trigger ;
    public bool _idInstructor ;


    private void Update()
    {
        if (!trigger)
            return;

        _gatherEvent.Move(new(10f , 10f, 10f));

    }


    // // ----------------------

    // public List<RealtimeAvatarManager> _players = new List<RealtimeAvatarManager>();
    // //public HashSet<RealtimeAvatarManager> _players = new HashSet<RealtimeAvatarManager>();


    // void Awake()
    // {
    //     _AvatarManager.avatarCreated +=  AvatarCreated;
    //     _AvatarManager.avatarDestroyed += AvatarDestroyed;
    // }

    // private void AvatarCreated(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
    // {
    //     _players.Add(avatarManager);

    //     Debug.Log("avatarManager" + avatarManager);
    // }

    // private void AvatarDestroyed(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
    // {
    //     _players.Remove(avatarManager);

    //     Debug.Log("avatarManager" + avatarManager);
    // }



    // // TODO it is either that we can access the XR Origin from the dictionary in hand 
    // // TODO or make a list of player when they are connected.
    // private void GatherPlayers()
    // {
    //     if (!_idInstructor)
    //         return;

    //     Debug.Log("_players" + _players);

    //     // foreach (RealtimeAvatarManager player in _players)
    //     // {

    //     //     var xROrigin = player.gameObject.GetComponent<Gather>().localXROrigin;

    //     //     Debug.Log("player" + player);

    //     //     xROrigin.transform.position = new(5f, 5f, 5f);
    //     // }
    // }
}
