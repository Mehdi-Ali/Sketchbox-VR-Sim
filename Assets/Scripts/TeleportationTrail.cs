using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationTrail : MonoBehaviour
{
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
        _line.enabled = true;

        _line.SetPosition(0, _startingPoint);
        _line.SetPosition(1, endingPoint);

        _startingPoint = endingPoint;

        Invoke(nameof(DeleteTrail), 5f);
    }

    private void DeleteTrail()
    {
        _line.enabled = false;
    }


    
}
