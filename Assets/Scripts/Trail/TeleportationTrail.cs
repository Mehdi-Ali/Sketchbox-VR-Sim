using Normal.Realtime;
using UnityEngine;

public class TeleportationTrail : MonoBehaviour
{
    [SerializeField] private Realtime _realtime;
    [SerializeField] private DrawCurve _curve;

    private LineRenderer _line;
    private Vector3 _startingPoint = Vector3.zero;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
    }
    public void DrawTrail(Vector3 endingPoint)
    {
        GameObject curveInstance = Realtime.Instantiate(_curve.name, ownedByClient: true, useInstance: _realtime);
        curveInstance.GetComponent<DrawCurve>().Initiate(_startingPoint, endingPoint);

        _startingPoint = endingPoint;
    }




    
}
