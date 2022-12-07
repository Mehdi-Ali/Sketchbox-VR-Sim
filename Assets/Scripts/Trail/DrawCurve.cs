using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class DrawCurve : RealtimeComponent<BrushStrokeModel>
{
    public Vector3 StartPoint;
    public Vector3 MidPoint; // TODO ...
    public Vector3 EndPoint;
    public LineRenderer lineRenderer;
    public int vertexCount = 12;
    [SerializeField] float _disappearTime = 5f;

    private TrailSync _trailSync;


    private void Awake()
    {
        _trailSync = GetComponent<TrailSync>();
    }


    private void Start()
    {
        if (realtimeView.isOwnedLocallySelf)
            gameObject.SetActive(false);
    }
    public void Initiate(Vector3 start, Vector3 end)
    {
        _trailSync.SetCurve(start, end);

        MidPoint = new( end.x, 5f, end.z);
        
        Invoke(nameof(DeleteTrail), _disappearTime);

    }

    private void DeleteTrail()
    {
        Realtime.Destroy(gameObject);
    }

    private void Update()
    {
        var pointList = new List<Vector3>();
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            var tangentLineVertex1 = Vector3.Lerp(StartPoint, MidPoint, ratio);
            var tangentLineVertex2 = Vector3.Lerp(MidPoint, EndPoint, ratio);
            var bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            pointList.Add(bezierPoint);
        }
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(StartPoint, MidPoint);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(MidPoint, EndPoint);

        Gizmos.color = Color.red;
        for (float ratio = 0.5f / vertexCount;ratio<1;ratio += 1.0f / vertexCount)
        {
            Gizmos.DrawLine(Vector3.Lerp(StartPoint, MidPoint, ratio), Vector3.Lerp(MidPoint, EndPoint, ratio));
        }
    }
}