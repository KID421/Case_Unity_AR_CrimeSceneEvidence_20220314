using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace KID
{
    /// <summary>
    /// �a�O�������I����m����
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class TapToPlaceObject : MonoBehaviour
    {
        [SerializeField, Header("�n��m������")]
        private GameObject goPlaceObject;

        private ARRaycastManager arRaycastManager;
        private List<ARRaycastHit> hits = new List<ARRaycastHit>();
        private Vector3 posMouse;

        private bool isPlace;

        private void Awake()
        {
            arRaycastManager = GetComponent<ARRaycastManager>();
        }

        private void Update()
        {
            TapToPlace();
        }

        /// <summary>
        /// �I����m����
        /// </summary>
        private void TapToPlace()
        {
            if (!isPlace && Input.GetKeyDown(KeyCode.Mouse0))
            {
                posMouse = Input.mousePosition;

                if (arRaycastManager.Raycast(posMouse, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    Instantiate(goPlaceObject, hitPose.position, Quaternion.identity);
                    
                    isPlace = true;
                }
            }
        }
    }
}
