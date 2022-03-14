using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// ���ʺ޲z��
    /// </summary>
    public class InteracteManager : MonoBehaviour
    {
        [SerializeField, Header("AR Camera")]
        private Camera cam;
        [SerializeField, Header("�g�u�Z��"), Range(0, 100)]
        private float camLength = 10;

        /// <summary>
        /// ���եΤ�r
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
            textTest = GameObject.Find("���եΤ�r").GetComponent<Text>();
        }

        private void Update()
        {
            CheckLookObject();
        }

        /// <summary>
        /// �ˬd�ݨ쪺����
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

