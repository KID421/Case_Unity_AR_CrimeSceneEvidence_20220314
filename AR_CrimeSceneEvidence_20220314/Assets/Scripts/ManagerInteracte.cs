using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace KID
{
    /// <summary>
    /// ���ʺ޲z��
    /// </summary>
    public class ManagerInteracte : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("AR Camera")]
        private Camera cam;
        [SerializeField, Header("�g�u�Z��"), Range(0, 100)]
        private float camLength = 10;
        [SerializeField, Header("�n����������ϼh")]
        private LayerMask layerToCheck;
        [SerializeField, Header("�ϥܴ֪��")]
        private Sprite spriteIconCottonSwab;
        [SerializeField, Header("�ϥܤ�Ҥ�")]
        private Sprite spriteIconScale;
        [SerializeField, Header("�ϥܤ��")]
        private Sprite spriteIconBrush;
        [SerializeField, Header("�ϥܬ۾�")]
        private Sprite spriteIconCamera;
        [SerializeField, Header("�ϥ��Ҫ��U")]
        private Sprite spriteIconEvidenceBag;

        /// <summary>
        /// �u��ϥ�
        /// </summary>
        private Image imgIconTool;
        private RectTransform rectImgIconTool;
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
        private ObjectToCheck objectToCheckCurrent;
        private Image imgCameraEffectTop;
        private Image imgCameraEffectBottom;
        #endregion

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
            imgIconTool = GameObject.Find("�u��ϥ�").GetComponent<Image>();
            rectImgIconTool = imgIconTool.GetComponent<RectTransform>();
            imgCameraEffectTop = GameObject.Find("��ӮĪG�W��").GetComponent<Image>();
            imgCameraEffectBottom = GameObject.Find("��ӮĪG�U��").GetComponent<Image>();

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

                bool result = false;

                switch (typeChooseTool)
                {
                    case TypeChooseTool.FlashLight:
                        result = UseFlashLight();
                        break;
                    case TypeChooseTool.EvidenceBag:
                        result = UseEvidenceBag();
                        break;
                    case TypeChooseTool.Scale:
                        result = UseScale();
                        break;
                    case TypeChooseTool.FingerPrint:
                        result = UseFingerPrint();
                        break;
                    case TypeChooseTool.Camera:
                        result = UseCamera();
                        break;
                }

                if (result) ChangeIcon(typeChooseTool);

                typeChooseTool = TypeChooseTool.None;
            });
        }

        /// <summary>
        /// �ϥΤ�q��
        /// </summary>
        private bool UseFlashLight()
        {
            print("�ϥΤ�q��");

            if (dataTargetGoal.needFlashLight)
            {
                print("<color=green>�ϥΤ�q�����\</color>");
                textActionMessage.text = nameTarget + " �ϥΤ�q�����\";
                dataTargetOriginal.needFlashLight = true;

                return true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn�ϥΤ�q��</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn�ϥΤ�q��";

                return false;
            }
        }

        /// <summary>
        /// ��J�Ҫ��U
        /// </summary>
        private bool UseEvidenceBag()
        {
            print("��J�Ҫ��U");

            if (dataTargetGoal.needEvidenceBag)
            {
                print("<color=green>��J�Ҫ��U���\</color>");
                textActionMessage.text = nameTarget + " ��J�Ҫ��U���\";
                dataTargetOriginal.needEvidenceBag = true;
                

                return true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn��J�Ҫ��U���\</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn��J�Ҫ��U���\";

                return false;
            }
        }

        /// <summary>
        /// ���q�ؤo
        /// </summary>
        private bool UseScale()
        {
            print("���q�ؤo");

            if (dataTargetGoal.needScale)
            {
                print("<color=green>���q�ؤo���\</color>");
                textActionMessage.text = nameTarget + " ���q�ؤo���\";
                dataTargetOriginal.needScale = true;
                objectToCheckCurrent.goScale.SetActive(true);

                return true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn���q�ؤo</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn���q�ؤo";

                return false;
            }
        }

        /// <summary>
        /// �Ķ�����
        /// </summary>
        private bool UseFingerPrint()
        {
            print("�Ķ�����");

            if (dataTargetGoal.needFingerPrint)
            {
                print("<color=green>�Ķ��������\</color>");
                textActionMessage.text = nameTarget + " �Ķ��������\";
                dataTargetOriginal.needFingerPrint = true;

                return true;
            }
            else
            {
                print("<color=red>�����󤣻ݱĶ�����</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݱĶ�����";

                return false;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        private bool UseCamera()
        {
            print("���");

            bool fingerPrint = dataTargetGoal.needFingerPrint != dataTargetOriginal.needFingerPrint;
            bool scale = dataTargetGoal.needScale != dataTargetOriginal.needScale;

            if (fingerPrint || scale)
            {
                print("<color=green>��ӥ��ѡA�|�������@�~</color>");
                textActionMessage.text = nameTarget + " ��ӥ��ѡA�|�������@�~";

                return false;
            }
            else if (dataTargetGoal.needCamera)
            {
                print("<color=green>��Ӧ��\</color>");
                textActionMessage.text = nameTarget + " ��Ӧ��\";
                dataTargetOriginal.needCamera = true;

                return true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn���</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn���";

                return false;
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

                objectToCheckCurrent = goTarget.GetComponent<ObjectToCheck>();

                dataTargetGoal = objectToCheckCurrent.dataGoal;
                dataTargetOriginal = objectToCheckCurrent.dataOriginal;
            }
        }

        /// <summary>
        /// �ܧ�ϥ�
        /// </summary>
        /// <param name="typeChooseTool">�u������</param>
        public void ChangeIcon(TypeChooseTool typeChooseTool)
        {
            Sprite spriteChoose = null;
            IEnumerator coroutine = null;

            switch (typeChooseTool)
            {
                case TypeChooseTool.FlashLight:
                    break;
                case TypeChooseTool.EvidenceBag:
                    spriteChoose = spriteIconEvidenceBag;
                    coroutine = EvidenceBag();
                    break;
                case TypeChooseTool.FingerPrint:
                    spriteChoose = spriteIconBrush;
                    coroutine = IconMove();
                    break;
                case TypeChooseTool.Camera:
                    spriteChoose = spriteIconCamera;
                    coroutine = CameraEffect();
                    break;
            }

            if (spriteChoose)
            {
                imgIconTool.color = new Color(1, 1, 1, 1);
                imgIconTool.sprite = spriteChoose;
            }
            else
            {
                imgIconTool.color = new Color(1, 1, 1, 0);
            }

            if (coroutine != null) StartCoroutine(coroutine);
        }

        /// <summary>
        /// �ϥܲ��ʮĪG�G����
        /// </summary>
        private IEnumerator IconMove()
        {
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(0.35f);
                float move = i % 2 == 0 ? 30 : -30;
                rectImgIconTool.anchoredPosition += Vector2.one * move;
            }

            imgIconTool.color = new Color(1, 1, 1, 0);

            if (objectToCheckCurrent.psEffect) objectToCheckCurrent.psEffect.Play();

            yield return new WaitForSeconds(2f);

            for (int i = 0; i < objectToCheckCurrent.goFingerPrints.Length; i++)
            {
                objectToCheckCurrent.goFingerPrints[i].SetActive(true);
            }
        }

        /// <summary>
        /// ��v���ĪG�G�W��P�U������֪�
        /// </summary>
        private IEnumerator CameraEffect()
        {
            float increase = 0.1f;

            for (int i = 0; i < 10; i++)
            {
                imgCameraEffectTop.fillAmount += increase;
                imgCameraEffectBottom.fillAmount += increase;
                yield return new WaitForSeconds(0.02f);
            }

            for (int i = 0; i < 10; i++)
            {
                imgCameraEffectTop.fillAmount -= increase;
                imgCameraEffectBottom.fillAmount -= increase;
                yield return new WaitForSeconds(0.02f);
            }
        }

        /// <summary>
        /// ��J�Ҫ��U
        /// </summary>
        private IEnumerator EvidenceBag()
        {

            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(0.25f);

                float size = i % 2 == 0 ? -10 : 10;
                rectImgIconTool.sizeDelta += Vector2.one * size;
            }

            goTarget.SetActive(false);

            imgIconTool.color = new Color(1, 1, 1, 0);
        }
    }
}
