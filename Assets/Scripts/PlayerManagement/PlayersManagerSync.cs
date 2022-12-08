using Normal.Realtime;
using UnityEngine;

public class PlayersManagerSync : RealtimeComponent<PlayersManagerModel>
{
    public Vector3 GetPosition(uint playerId)
    {
        if(!PlayerExistsInDict(playerId))
            return Vector3.zero; // TODO could be better.
        
        return model.players[playerId].position;
    }
  
    public void IncrementScore(uint playerId)
    {
        if(!PlayerExistsInDict(playerId))
            AddPlayerToDict(playerId);
        
        model.players[playerId].position += Vector3.one;
    }
    
    private void AddPlayerToDict(uint playerId)
    {
        PlayerModel newUserScoreModel = new PlayerModel();
        newUserScoreModel.position = Vector3.zero;
        model.players.Add(playerId, newUserScoreModel);
    }
    
    private bool PlayerExistsInDict(uint playerId)
    {
        try
        {
          PlayerModel _ = model.players[playerId];
          return true;
        }
        catch
        {
          return false;
        }
    }
}