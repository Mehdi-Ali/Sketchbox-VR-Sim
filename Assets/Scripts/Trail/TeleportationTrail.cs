using Normal.Realtime;
using UnityEngine;

public class TeleportationTrail : MonoBehaviour
{
    [SerializeField] private Realtime _realtime;
    [SerializeField] private DrawCurve _curve;
    [SerializeField] private TeleportController _tpController;
    private LineRenderer _line;
    private Vector3 _startingPoint;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
        _startingPoint = _tpController.transform.position;
    }
    public void DrawTrail(Vector3 endingPoint)
    {
        GameObject curveInstance = Realtime.Instantiate(_curve.name, ownedByClient: true, useInstance: _realtime);
        curveInstance.GetComponent<DrawCurve>().Initiate(_startingPoint, endingPoint);

        _startingPoint = endingPoint;
    }




    
}
