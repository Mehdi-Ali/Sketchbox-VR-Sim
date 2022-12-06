using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class Brush : MonoBehaviour 
{
    [SerializeField] private BrushStroke _brushStrokePrefab = null;

    [SerializeField] private ActionBasedController _hand = null;

    private Vector3     _handPosition;
    private Quaternion  _handRotation;
    private BrushStroke _activeBrushStroke;

    public bool triggerPressed = false;

    [SerializeField] private  XRDeviceSimulator _controls;


    private void Awake()
    {
        SubscriptingToInputManager();
    }

    private void SubscriptingToInputManager()
    {

        var action = _controls.triggerAction.action ;
        action.started += (InputAction.CallbackContext cntx) => {triggerPressed = true;};
        action.canceled += (InputAction.CallbackContext cntx) => {triggerPressed = false;};
    }

    private void Update() 
    {

        UpdatePose(_hand, ref _handPosition, ref _handRotation);

        if (triggerPressed && _activeBrushStroke == null)
        {
            _activeBrushStroke = Instantiate(_brushStrokePrefab);
            _activeBrushStroke.BeginBrushStrokeWithBrushTipPoint(_handPosition, _handRotation);
        }

        if (triggerPressed)
            _activeBrushStroke.MoveBrushTipToPoint(_handPosition, _handRotation);

        if (!triggerPressed && _activeBrushStroke != null) {
            _activeBrushStroke.EndBrushStrokeWithBrushTipPoint(_handPosition, _handRotation);
            _activeBrushStroke = null;
        }
    }

    private void UpdatePose(ActionBasedController hand, ref Vector3 position, ref Quaternion rotation)
    {
        position = hand.transform.position;
        rotation = hand.transform.rotation;
    }
}
