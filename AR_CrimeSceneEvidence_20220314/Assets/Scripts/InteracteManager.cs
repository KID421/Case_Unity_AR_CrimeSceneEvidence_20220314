using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// 互動管理器
    /// </summary>
    public class InteracteManager : MonoBehaviour
    {
        [SerializeField, Header("AR Camera")]
        private Camera cam;
        [SerializeField, Header("射線距離"), Range(0, 100)]
        private float camLength = 10;

        /// <summary>
        /// 測試用文字
        /// </summary>
        private Text textTest;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0);
            Gizmos.DrawRay(cam.transform.position, cam.transform.forward * camLength);
        }

        private void Awake()
        {
            cam = GameObject.Find("AR Camera").GetComponent<Camera>();
            textTest = GameObject.Find("測試用文字").GetComponent<Text>();
        }

        private void Update()
        {
            CheckLookObject();
        }

        /// <summary>
        /// 檢查看到的物件
        /// </summary>
        private void CheckLookObject()
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100))
            {
                textTest.text = hit.collider.gameObject.name;
            }
        }
    }
}

