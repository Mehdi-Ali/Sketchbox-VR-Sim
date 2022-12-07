using System.Xml.XPath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class TrailTrigger : MonoBehaviour
{
    [SerializeField] TeleportationTrail _tpTrail;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<XROrigin>(out XROrigin _xROrigin))
        {
            _tpTrail.DrawTrail(transform.position);
            Debug.Log("Triggered");
        }
    }
}
