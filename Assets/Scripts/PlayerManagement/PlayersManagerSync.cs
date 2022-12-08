using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class PlayersManagerSync : RealtimeComponent<PlayersManagerModel>
{
    [SerializeField] private Transform[] GatheringPoints;

    public uint AddPlayerToDict()
    {
        var playerId = model.lastID ++;
        var isInstructor = false;
        var meetingPosition = Vector3.zero;

        meetingPosition = GatheringPoints[model.gatheringPointsIndex].position;
        if (model.gatheringPointsIndex < GatheringPoints.Length - 1)
            model.gatheringPointsIndex++;

        if(!model.instructorAssigned) 
        {
            isInstructor = true;
            meetingPosition = GatheringPoints[0].position;
            model.instructorAssigned = true;
        }


        PlayerModel newPlayerModel = new PlayerModel();
        newPlayerModel.meetingPosition = meetingPosition;
        newPlayerModel.isInstructor = isInstructor;
        newPlayerModel.isInMeeting = false;

        model.players.Add(playerId, newPlayerModel);

        return playerId;
    }

    public void RemovePlayerToDict(uint playerId)
    {
        model.players.Remove(playerId);
    }

    public bool MeetingStatus(uint playerId)
    {
        return model.players[playerId].isInMeeting;
    }

    public Vector3 GetPosition(uint playerId)
    {
        return model.players[playerId].meetingPosition;
    }

    public Vector3 GetInstructorPosition()
    {
        return GatheringPoints[0].position;
    }

    public bool IfIsInstructor(uint playerId)
    {
        return model.players[playerId].isInstructor;
    }

    public void StartMeeting()
    {
        foreach(var player in model.players)
        {
            player.Value.isInMeeting = true;
        }

        Invoke(nameof(EndMeeting), 5f);
    }
    
    public void EndMeeting()
    {
        foreach(var player in model.players)
        {
            player.Value.isInMeeting = false;
        }
    }
    
}