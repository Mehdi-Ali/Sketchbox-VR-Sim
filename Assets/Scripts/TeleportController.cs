using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class TeleportController : MonoBehaviour // move stuff to a general control script.
{
    [SerializeField] private ActionBasedController _rightController;
    [SerializeField] private  ActionBasedController _leftController;
    [SerializeField] private  XRDeviceSimulator _controls;

    private XRInteractorLineVisual _leftRay ;
    private GameObject _leftReticle;

    private XRInteractorLineVisual _rightRay ; 
    private GameObject _rightReticle;

    private bool _isGripActive;
    private bool _isRightHandActive;
    private bool _isLeftHandActive;

    private enum _hand {Right, Left}; 


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

    private void ProcessesReticle( _hand hand)
    {
        if (!_isGripActive)
        {
            _rightReticle.SetActive(false);
            _leftReticle.SetActive(false);
        }
        if (hand == _hand.Right) 
        {
            _rightReticle.SetActive(_isRightHandActive);
            _rightRay.enabled = _isRightHandActive;
        }

        if (hand == _hand.Left) 
        {
            _leftReticle.SetActive(_isLeftHandActive);
            _leftRay.enabled = _isLeftHandActive;
        }

    }

    private void SubscriptingToInputManager()
    {
        // could be better if done in the editor by dropping and dragging scriptableObjects of the actions;
        var action = _controls.manipulateLeftAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _isLeftHandActive = true; ProcessesReticle(_hand.Left);};
        action.canceled += (InputAction.CallbackContext cntx) => { _isLeftHandActive = false; ProcessesReticle(_hand.Left);};

        action = _controls.toggleManipulateLeftAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _isLeftHandActive = ! _isLeftHandActive ; ProcessesReticle(_hand.Left);};

        action = _controls.manipulateRightAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _isRightHandActive = true; ProcessesReticle(_hand.Right);};
        action.canceled += (InputAction.CallbackContext cntx) => { _isRightHandActive = false; ProcessesReticle(_hand.Right);};

        action = _controls.toggleManipulateRightAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _isRightHandActive = ! _isRightHandActive ; ProcessesReticle(_hand.Right);};

        action = _controls.gripAction.action ;
        action.started += (InputAction.CallbackContext cntx) => { _isGripActive = true;};
        action.canceled += (InputAction.CallbackContext cntx) => { _isGripActive = false;};
    }
}
