using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// ���ʺ޲z��
    /// </summary>
    public class ManagerInteracte : MonoBehaviour
    {
        [SerializeField, Header("AR Camera")]
        private Camera cam;
        [SerializeField, Header("�g�u�Z��"), Range(0, 100)]
        private float camLength = 10;
        [SerializeField, Header("�n����������ϼh")]
        private LayerMask layerToCheck;

        /// <summary>
        /// �襤������
        /// </summary>
        private Text textChooseObject;
        /// <summary>
        /// �ʧ@�T��
        /// </summary>
        private Text textActionMessage;
        /// <summary>
        /// �u���q��
        /// </summary>
        private Button btnFlashLight;
        /// <summary>
        /// �u���Ҫ��U
        /// </summary>
        private Button btnEvidenceBag;
        /// <summary>
        /// �u��DNA
        /// </summary>
        private Button btnDNA;
        /// <summary>
        /// �u���Ҥ�
        /// </summary>
        private Button btnScale;
        /// <summary>
        /// �u�����
        /// </summary>
        private Button btnFingerPrint;
        /// <summary>
        /// �u��۾�
        /// </summary>
        private Button btnCamera;
        /// <summary>
        /// �ϥ�
        /// </summary>
        private Button btnUse;

        private string nameTarget;
        private DataObject dataTargetGoal;
        private DataObject dataTargetOriginal;
        private TypeChooseTool typeChooseTool;
        private GameObject goTarget;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0);
            Gizmos.DrawRay(cam.transform.position, cam.transform.forward * camLength);
        }

        private void Awake()
        {
            cam = GameObject.Find("AR Camera").GetComponent<Camera>();
            textChooseObject = GameObject.Find("�襤������").GetComponent<Text>();
            textActionMessage = GameObject.Find("�ʧ@�T��").GetComponent<Text>();

            ToolAndUseButtonsFind();
            ToolButtonsClick();
            UseTool();
        }

        private void Update()
        {
            CheckLookObject();
        }

        /// <summary>
        /// �u��P�ϥΫ��s�M��
        /// </summary>
        private void ToolAndUseButtonsFind()
        {
            btnFlashLight = GameObject.Find("�u���q��").GetComponent<Button>();
            btnEvidenceBag = GameObject.Find("�u���Ҫ��U").GetComponent<Button>();
            btnDNA = GameObject.Find("�u��DNA").GetComponent<Button>();
            btnScale = GameObject.Find("�u���Ҥ�").GetComponent<Button>();
            btnFingerPrint = GameObject.Find("�u�����").GetComponent<Button>();
            btnCamera = GameObject.Find("�u��۾�").GetComponent<Button>();
            btnUse = GameObject.Find("�ϥ�").GetComponent<Button>();
        }

        /// <summary>
        /// �u����s�I��
        /// </summary>
        private void ToolButtonsClick()
        {
            btnFlashLight.onClick.AddListener(() => typeChooseTool = TypeChooseTool.FlashLight);
            btnEvidenceBag.onClick.AddListener(() => typeChooseTool = TypeChooseTool.EvidenceBag);
            btnDNA.onClick.AddListener(() => typeChooseTool = TypeChooseTool.DNA);
            btnScale.onClick.AddListener(() => typeChooseTool = TypeChooseTool.Scale);
            btnFingerPrint.onClick.AddListener(() => typeChooseTool = TypeChooseTool.FingerPrint);
            btnCamera.onClick.AddListener(() => typeChooseTool = TypeChooseTool.Camera);
        }

        /// <summary>
        /// �ϥΤu��
        /// </summary>
        private void UseTool()
        {
            btnUse.onClick.AddListener(() => 
            {
                if (!dataTargetGoal) return;

                switch (typeChooseTool)
                {
                    case TypeChooseTool.FlashLight:
                        UseFlashLight();
                        break;
                    case TypeChooseTool.EvidenceBag:
                        UseEvidenceBag();
                        break;
                    case TypeChooseTool.DNA:
                        UseDNA();
                        break;
                    case TypeChooseTool.Scale:
                        UseScale();
                        break;
                    case TypeChooseTool.FingerPrint:
                        UseFingerPrint();
                        break;
                    case TypeChooseTool.Camera:
                        UseCamera();
                        break;
                }

                typeChooseTool = TypeChooseTool.None;
            });
        }

        /// <summary>
        /// �ϥΤ�q��
        /// </summary>
        private void UseFlashLight()
        {
            print("�ϥΤ�q��");

            if (dataTargetGoal.needFlashLight)
            {
                print("<color=green>�ϥΤ�q�����\</color>");
                textActionMessage.text = nameTarget + " �ϥΤ�q�����\";
                dataTargetOriginal.needFlashLight = true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn�ϥΤ�q��</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn�ϥΤ�q��";
            }
        }

        /// <summary>
        /// ��J�Ҫ��U
        /// </summary>
        private void UseEvidenceBag()
        {
            print("��J�Ҫ��U");

            if (dataTargetGoal.needEvidenceBag)
            {
                print("<color=green>��J�Ҫ��U���\</color>");
                textActionMessage.text = nameTarget + " ��J�Ҫ��U���\";
                dataTargetOriginal.needEvidenceBag = true;
                goTarget.SetActive(false);
            }
            else
            {
                print("<color=red>�����󤣻ݭn��J�Ҫ��U���\</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn��J�Ҫ��U���\";
            }
        }

        /// <summary>
        /// �Ķ� DNA
        /// </summary>
        private void UseDNA()
        {
            print("�Ķ� DNA");

            if (dataTargetGoal.needDNA)
            {
                print("<color=green>�Ķ� DNA���\</color>");
                textActionMessage.text = nameTarget + " �Ķ� DNA���\";
                dataTargetOriginal.needDNA = true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn�Ķ� DNA</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn�Ķ� DNA";
            }
        }

        /// <summary>
        /// ���q�ؤo
        /// </summary>
        private void UseScale()
        {
            print("���q�ؤo");

            if (dataTargetGoal.needScale)
            {
                print("<color=green>���q�ؤo���\</color>");
                textActionMessage.text = nameTarget + " ���q�ؤo���\";
                dataTargetOriginal.needScale = true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn���q�ؤo</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn���q�ؤo";
            }
        }

        /// <summary>
        /// �Ķ�����
        /// </summary>
        private void UseFingerPrint()
        {
            print("�Ķ�����");

            if (dataTargetGoal.needFingerPrint)
            {
                print("<color=green>�Ķ��������\</color>");
                textActionMessage.text = nameTarget + " �Ķ��������\";
                dataTargetOriginal.needFingerPrint = true;

                ObjectToCheck objectToCheck = goTarget.GetComponent<ObjectToCheck>();

                for (int i = 0; i < objectToCheck.goFingerPrints.Length; i++)
                {
                    objectToCheck.goFingerPrints[i].SetActive(true);
                }
            }
            else
            {
                print("<color=red>�����󤣻ݱĶ�����</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݱĶ�����";
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        private void UseCamera()
        {
            print("���");

            if (dataTargetGoal.needCamera)
            {
                print("<color=green>��Ӧ��\</color>");
                textActionMessage.text = nameTarget + " ��Ӧ��\";
                dataTargetOriginal.needCamera = true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn���</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn���";
            }
        }

        /// <summary>
        /// �ˬd�ݨ쪺����
        /// </summary>
        private void CheckLookObject()
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100, layerToCheck))
            {
                goTarget = hit.collider.gameObject;
                textChooseObject.text = goTarget.name;

                dataTargetGoal = goTarget.GetComponent<ObjectToCheck>().dataGoal;
                dataTargetOriginal = goTarget.GetComponent<ObjectToCheck>().dataOriginal;
            }
        }
    }
}
