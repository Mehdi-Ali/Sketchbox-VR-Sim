using System;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class DrawCurve : RealtimeComponent<BrushStrokeModel>
{
    private Vector3 _startPoint;
    private Vector3 _midPoint;
    private Vector3 _endPoint;
    public LineRenderer lineRenderer;
    public int vertexCount = 12;
    [SerializeField] float _disappearTime = 5f; 


    public void Initiate(Vector3 start, Vector3 end)
    {
        _startPoint = start;
        _endPoint = end;
        _midPoint = new( end.x, 5f, end.z);
        
        Invoke(nameof(DeleteTrail), _disappearTime);
    }

    private void DeleteTrail()
    {
        Realtime.Destroy(gameObject);
    }

    void Update()
    {
        var pointList = new List<Vector3>();
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            var tangentLineVertex1 = Vector3.Lerp(_startPoint, _midPoint, ratio);
            var tangentLineVertex2 = Vector3.Lerp(_midPoint, _endPoint, ratio);
            var bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            pointList.Add(bezierPoint);
        }
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_startPoint, _midPoint);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(_midPoint, _endPoint);

        Gizmos.color = Color.red;
        for (float ratio = 0.5f / vertexCount;ratio<1;ratio += 1.0f / vertexCount)
        {
            Gizmos.DrawLine(Vector3.Lerp(_startPoint, _midPoint, ratio), Vector3.Lerp(_midPoint, _endPoint, ratio));
        }
    }
}