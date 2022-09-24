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
        [SerializeField, Header("�a�O����")]
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

                    HidePlaneObject();
                }
            }
        }

        /// <summary>
        /// ���æa�O����
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
