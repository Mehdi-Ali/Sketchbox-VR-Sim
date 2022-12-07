using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

public class BrushStroke : RealtimeComponent<BrushStrokeModel>
{
    [SerializeField]
    private BrushStrokeMesh _mesh = null;

    private Vector3    _ribbonEndPosition;
    private Quaternion _ribbonEndRotation = Quaternion.identity;

    private Vector3    _previousRibbonPointPosition;
    private Quaternion _previousRibbonPointRotation = Quaternion.identity;


    private void Update() 
    {
        AnimateLastRibbonPointTowardsBrushTipPosition();
        AddRibbonPointIfNeeded();
    }

    public void BeginBrushStrokeWithBrushTipPoint(Vector3 position, Quaternion rotation)
    {
        model.brushTipPosition = position;
        model.brushTipRotation = rotation;

        _ribbonEndPosition = position;
        _ribbonEndRotation = rotation;
        _mesh.UpdateLastRibbonPoint(_ribbonEndPosition, _ribbonEndRotation);
    }

    public void MoveBrushTipToPoint(Vector3 position, Quaternion rotation) {
        model.brushTipPosition = position;
        model.brushTipRotation = rotation;
    }

    public void EndBrushStrokeWithBrushTipPoint(Vector3 position, Quaternion rotation) 
    {
        AddRibbonPoint(position, rotation);
        model.brushStrokeFinalized = true;
    }

    protected override void OnRealtimeModelReplaced(BrushStrokeModel previousModel, BrushStrokeModel currentModel) 
    {
        _mesh.ClearRibbon();

        if (previousModel != null) 
            previousModel.ribbonPoints.modelAdded -= RibbonPointAdded;
        
        if (currentModel != null)
        {
            foreach (RibbonPointModel ribbonPoint in currentModel.ribbonPoints)
                _mesh.InsertRibbonPoint(ribbonPoint.position, ribbonPoint.rotation);

            _ribbonEndPosition = model.brushTipPosition;
            _ribbonEndRotation = model.brushTipRotation;
            _mesh.UpdateLastRibbonPoint(model.brushTipPosition, model.brushTipRotation);

            _mesh.skipLastRibbonPoint = model.brushStrokeFinalized;
            currentModel.ribbonPoints.modelAdded += RibbonPointAdded;
        } 

    }

    private void AddRibbonPointIfNeeded() 
    {
        if (!realtimeView.isOwnedLocallySelf)
            return;

        if (model.brushStrokeFinalized)
            return;

        if (Vector3.Distance(_ribbonEndPosition, _previousRibbonPointPosition) >= 0.01f ||
            Quaternion.Angle(_ribbonEndRotation, _previousRibbonPointRotation) >= 10.0f)
        {
            AddRibbonPoint(_ribbonEndPosition, _ribbonEndRotation);

            _previousRibbonPointPosition = _ribbonEndPosition;
            _previousRibbonPointRotation = _ribbonEndRotation;
        }
    }

    private void AddRibbonPoint(Vector3 position, Quaternion rotation)
    {
        RibbonPointModel ribbonPoint = new RibbonPointModel();
        ribbonPoint.position = position;
        ribbonPoint.rotation = rotation;
        model.ribbonPoints.Add(ribbonPoint);
    }

    private void RibbonPointAdded(RealtimeArray<RibbonPointModel> ribbonPoints, RibbonPointModel ribbonPoint, bool remote)
    {
        _mesh.InsertRibbonPoint(ribbonPoint.position, ribbonPoint.rotation);
    }

    private void AnimateLastRibbonPointTowardsBrushTipPosition()
    {
        if (model.brushStrokeFinalized) 
        {
            _mesh.skipLastRibbonPoint = true;
            return;
        }

        Vector3    brushTipPosition = model.brushTipPosition;
        Quaternion brushTipRotation = model.brushTipRotation;

        if (Vector3.Distance(_ribbonEndPosition, brushTipPosition) <= 0.0001f &&
            Quaternion.Angle(_ribbonEndRotation, brushTipRotation) <= 0.01f)
            return;

        _ribbonEndPosition =     Vector3.Lerp(_ribbonEndPosition, brushTipPosition, 25.0f * Time.deltaTime);
        _ribbonEndRotation = Quaternion.Slerp(_ribbonEndRotation, brushTipRotation, 25.0f * Time.deltaTime);

        _mesh.UpdateLastRibbonPoint(_ribbonEndPosition, _ribbonEndRotation);
    }
}
