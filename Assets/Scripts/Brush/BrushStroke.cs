using System.Collections.Generic;
using UnityEngine;

public class BrushStroke : MonoBehaviour 
{
    [SerializeField]
    private BrushStrokeMesh _mesh = null;

    // Ribbon State
    struct RibbonPoint
    {
        public Vector3    position;
        public Quaternion rotation;
    }

    private List<RibbonPoint> _ribbonPoints = new List<RibbonPoint>();

    private Vector3    _brushTipPosition;
    private Quaternion _brushTipRotation;
    private bool       _brushStrokeFinalized;

    // Smoothing
    private Vector3    _ribbonEndPosition;
    private Quaternion _ribbonEndRotation = Quaternion.identity;

    // Mesh
    private Vector3    _previousRibbonPointPosition;
    private Quaternion _previousRibbonPointRotation = Quaternion.identity;


    private void Update() 
    {
        AnimateLastRibbonPointTowardsBrushTipPosition();
        AddRibbonPointIfNeeded();
    }

    public void BeginBrushStrokeWithBrushTipPoint(Vector3 position, Quaternion rotation)
    {
        _brushTipPosition = position;
        _brushTipRotation = rotation;

        _ribbonEndPosition = position;
        _ribbonEndRotation = rotation;
        _mesh.UpdateLastRibbonPoint(_ribbonEndPosition, _ribbonEndRotation);
    }

    public void MoveBrushTipToPoint(Vector3 position, Quaternion rotation) {
        _brushTipPosition = position;
        _brushTipRotation = rotation;
    }

    public void EndBrushStrokeWithBrushTipPoint(Vector3 position, Quaternion rotation) 
    {
        AddRibbonPoint(position, rotation);
        _brushStrokeFinalized = true;
    }


    private void AddRibbonPointIfNeeded() 
    {
        if (_brushStrokeFinalized)
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
        RibbonPoint ribbonPoint = new RibbonPoint();
        ribbonPoint.position = position;
        ribbonPoint.rotation = rotation;
        _ribbonPoints.Add(ribbonPoint);

        _mesh.InsertRibbonPoint(position, rotation);
    }

    private void AnimateLastRibbonPointTowardsBrushTipPosition()
    {
        if (_brushStrokeFinalized) 
        {
            _mesh.skipLastRibbonPoint = true;
            return;
        }

        Vector3    brushTipPosition = _brushTipPosition;
        Quaternion brushTipRotation = _brushTipRotation;

        if (Vector3.Distance(_ribbonEndPosition, brushTipPosition) <= 0.0001f &&
            Quaternion.Angle(_ribbonEndRotation, brushTipRotation) <= 0.01f)
            return;

        _ribbonEndPosition =     Vector3.Lerp(_ribbonEndPosition, brushTipPosition, 25.0f * Time.deltaTime);
        _ribbonEndRotation = Quaternion.Slerp(_ribbonEndRotation, brushTipRotation, 25.0f * Time.deltaTime);

        _mesh.UpdateLastRibbonPoint(_ribbonEndPosition, _ribbonEndRotation);
    }
}
