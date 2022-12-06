using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private ActionBasedController _rightController;
    [SerializeField] private  ActionBasedController _leftController;
    [SerializeField] private  XRDeviceSimulator _controls;

    private XRInteractorLineVisual _leftRay ;
    private GameObject _leftReticle;

    private XRInteractorLineVisual _rightRay ; 
    private GameObject _rightReticle;
    private bool _isGripActive;


    private void Awake()
    {
        SubscriptingToInputManager();
        CachingInstances();
    }

    private void CachingInstances()
    {
        _leftRay = _rightController.gameObject.GetComponent<XRInteractorLineVisual>();
        _leftReticle = _leftRay.reticle;

        _rightRay = _leftController.gameObject.GetComponent<XRInteractorLineVisual>();
        _rightReticle = _rightRay.reticle;
    }

    private void Start()
    {
        _isGripActive = false ;
        _leftRay.enabled = false ;
        _rightRay.enabled = false ;
    }

    private void ProcessesReticle(bool isHandActive, int index)
    {
        if (!_isGripActive)
        {
            _rightReticle.SetActive(false);
            _leftReticle.SetActive(false);
        }
        else if (isHandActive && index == 1) 
        {
            _rightReticle.SetActive(true);
            _leftReticle.SetActive(false);
        }

         else if (index == 2) 
        {
            _leftReticle.SetActive(true);
            _rightReticle.SetActive(false);
        }
    }

    private void SubscriptingToInputManager()
    {

        var action = _controls.manipulateLeftAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _leftRay.enabled = true; ProcessesReticle(true, 2);};
        action.canceled += (InputAction.CallbackContext cntx) => { _leftRay.enabled = false; ProcessesReticle(false, 2);};

        action = _controls.toggleManipulateLeftAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _leftRay.enabled = ! _leftRay.enabled ; ProcessesReticle(_leftRay.enabled, 2);};

        action = _controls.manipulateRightAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _rightRay.enabled = true; ProcessesReticle(true, 1);};
        action.canceled += (InputAction.CallbackContext cntx) => { _rightRay.enabled = false; ProcessesReticle(false, 1);};

        action = _controls.toggleManipulateRightAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _rightRay.enabled = ! _rightRay.enabled ; ProcessesReticle(_rightRay.enabled, 2);};

        action = _controls.gripAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _isGripActive = true;};
        action.canceled += (InputAction.CallbackContext cntx) => { _isGripActive = false;};
    }
}
