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
        [SerializeField, Header("地板材質")]
        private Material materialGround;

        private ARRaycastManager arRaycastManager;
        private List<ARRaycastHit> hits = new List<ARRaycastHit>();
        private Vector3 posMouse;
        private ARPlaneManager arPlaneManager;

        private bool isPlace;

        private void Awake()
        {
            materialGround.color = new Color(1, 1, 1, 1);
            arRaycastManager = GetComponent<ARRaycastManager>();
            arPlaneManager = GetComponent<ARPlaneManager>();
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

                    HidePlaneObject();
                }
            }
        }

        /// <summary>
        /// 隱藏地板物件
        /// </summary>
        private void HidePlaneObject()
        {
            materialGround.color = new Color(1, 1, 1, 0);

            foreach (var plane in arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
    }
}
