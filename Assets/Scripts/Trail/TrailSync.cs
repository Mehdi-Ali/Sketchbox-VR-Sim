using Normal.Realtime;
using UnityEngine;

public class TrailSync : RealtimeComponent<TrailSyncModel>
{
    private DrawCurve _curve;

    private void Awake()
    {
        _curve = GetComponent<DrawCurve>();
    }

        protected override void OnRealtimeModelReplaced(TrailSyncModel previousModel, TrailSyncModel currentModel)
        {

            if (previousModel != null) 
            {
                previousModel.startPointDidChange -= StartPointDidChange;
                previousModel.endPointDidChange -= EndPointDidChange;
            }
            
            if (currentModel != null) 
            {
                if (currentModel.isFreshModel)
                {
                    currentModel.startPoint = _curve.StartPoint;
                    currentModel.endPoint = _curve.EndPoint;
                }
            
                UpdateCurve();

                currentModel.startPointDidChange += StartPointDidChange;
                currentModel.endPointDidChange -= EndPointDidChange;
            }
    }

    private void StartPointDidChange(TrailSyncModel model, Vector3 value)
    {
        UpdateCurve();
    }

    private void EndPointDidChange(TrailSyncModel model, Vector3 value)
    {
        UpdateCurve();
    }


    private void UpdateCurve()
    {
        var startPoint =  model.startPoint;
        var endPoint =  model.endPoint;

        _curve.StartPoint = startPoint;
        _curve.EndPoint = endPoint;
        _curve.MidPoint = new( endPoint.x, 5f, endPoint.z);
    }

    public void SetCurve(Vector3 start, Vector3 end)
    {
        model.startPoint = start;
        model.endPoint = end;
    }
}
