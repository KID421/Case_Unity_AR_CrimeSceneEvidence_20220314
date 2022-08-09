using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace KID
{
    /// <summary>
    /// 地板偵測並點擊放置物件
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class TapToPlaceObject : MonoBehaviour
    {
        [SerializeField, Header("要放置的物件")]
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
        /// 點擊放置物件
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
