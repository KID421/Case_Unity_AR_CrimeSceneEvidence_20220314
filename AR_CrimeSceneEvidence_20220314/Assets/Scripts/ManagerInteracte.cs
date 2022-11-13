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
        [SerializeField, Header("�ϥ� DNA �֪��")]
        private Sprite spriteIconDNACottonSwab;
        [SerializeField, Header("�ϥܤ�Ҥ�")]
        private Sprite spriteIconScale;
        [SerializeField, Header("�ϥܤ��")]
        private Sprite spriteIconBrush;
        [SerializeField, Header("�ϥܬ۾�")]
        private Sprite spriteIconCamera;
        [SerializeField, Header("�ϥ��Ҫ��U")]
        private Sprite spriteIconEvidenceBag;
        [SerializeField, Header("����B�z��n����")]
        private GameObject goPPVolume;

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
        /// �u��DNA
        /// </summary>
        private Button btnDNA;
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
        private TypeEvidence typeChooseTool;
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
            btnUse.onClick.AddListener(() => UseTool());
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
            btnDNA = GameObject.Find("�u��DNA").GetComponent<Button>();
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
            btnFlashLight.onClick.AddListener(() => ChooseTool(TypeEvidence.FlashLight));
            btnDNA.onClick.AddListener(() => ChooseTool(TypeEvidence.DNA));
            btnEvidenceBag.onClick.AddListener(() => ChooseTool(TypeEvidence.EvidenceBag));
            btnScale.onClick.AddListener(() => ChooseTool(TypeEvidence.Scale));
            btnFingerPrint.onClick.AddListener(() => ChooseTool(TypeEvidence.FingerPrint));
            btnCamera.onClick.AddListener(() => ChooseTool(TypeEvidence.Camera));
        }

        /// <summary>
        /// ��ܤu��
        /// </summary>
        private void ChooseTool(TypeEvidence _typeChooseTool)
        {
            typeChooseTool = _typeChooseTool;

            Sprite spriteChoose = null;

            switch (typeChooseTool)
            {
                case TypeEvidence.FlashLight:
                    break;
                case TypeEvidence.EvidenceBag:
                    spriteChoose = spriteIconEvidenceBag;
                    break;
                case TypeEvidence.DNA:
                    spriteChoose = spriteIconDNACottonSwab;
                    break;
                case TypeEvidence.Scale:
                    spriteChoose = spriteIconScale;
                    break;
                case TypeEvidence.FingerPrint:
                    spriteChoose = spriteIconBrush;
                    break;
                case TypeEvidence.Camera:
                    spriteChoose = spriteIconCamera;
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
        }

        /// <summary>
        /// �ϥΤu��
        /// </summary>
        private void UseTool()
        {
            // �p�G���O��q�� �åB �p�G�S���ؼи�ƴN���X
            if (typeChooseTool != TypeEvidence.FlashLight && !dataTargetGoal ) return;

            bool result = false;

            switch (typeChooseTool)
            {
                case TypeEvidence.FlashLight:
                    result = UseFlashLight();
                    break;
                case TypeEvidence.EvidenceBag:
                    result = UseEvidenceBag();
                    break;
                case TypeEvidence.Scale:
                    result = UseScale();
                    break;
                case TypeEvidence.FingerPrint:
                    result = UseFingerPrint();
                    break;
                case TypeEvidence.Camera:
                    result = UseCamera();
                    break;
                case TypeEvidence.DNA:
                    result = UseDNA();
                    break;
            }

            if (result) ToolIconEffect(typeChooseTool);

            typeChooseTool = TypeEvidence.None;
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

                goPPVolume.SetActive(!goPPVolume.activeInHierarchy);
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

            if (dataTargetGoal.needFlashLight)
            {
                if (!dataTargetOriginal.needFlashLight)
                {
                    print("<color=red>��J�Ҫ��U���ѡA�|��������q������</color>");

                    return false;
                }
                else
                {
                    print("<color=green>��q�������w�B�z�����I</color>");
                }
            }

            if (dataTargetGoal.needCamera)
            {
                if (!dataTargetOriginal.needCamera)
                {
                    print("<color=red>��J�Ҫ��U���ѡA�|���������</color>");

                    return false;
                }
                else
                {
                    print("<color=green>��Ӥw�B�z�����I</color>");
                }
            }

            if (dataTargetGoal.needEvidenceBag)
            {
                print("<color=green>��J�Ҫ��U���\</color>");
                textActionMessage.text = nameTarget + " ��J�Ҫ��U���\";
                dataTargetOriginal.needEvidenceBag = true;

                MissionObjectManager.instance.UpdateMission();

                return true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn��J�Ҫ��U</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn��J�Ҫ��U";

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
                imgIconTool.color = new Color(1, 1, 1, 0);

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
                if (dataTargetOriginal.needFingerPrint)
                {
                    print("<color=green>�w�g�Ķ��L����</color>");

                    return false;
                }
                else
                {
                    print("<color=green>�Ķ��������\</color>");
                    textActionMessage.text = nameTarget + " �Ķ��������\";
                    dataTargetOriginal.needFingerPrint = true;
                }

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
            bool dna = dataTargetGoal.needDNA != dataTargetOriginal.needDNA;

            if (dataTargetGoal.needFingerPrint)
            {
                if (!dataTargetOriginal.needFingerPrint)
                {
                    print("<color=red>��ӥ��ѡA�|�����������@�~</color>");

                    return false;
                }
                else
                {
                    print("<color=green>�����w�B�z�����I</color>");
                }
            }

            if (dataTargetGoal.needScale)
            {
                if (!dataTargetOriginal.needScale)
                {
                    print("<color=red>��ӥ��ѡA�|���������q�@�~</color>");

                    return false;
                }
                else
                {
                    print("<color=green>���q�w�B�z�����I</color>");
                }
            }

            if (dataTargetGoal.needDNA)
            {
                if (!dataTargetOriginal.needDNA)
                {
                    print("<color=red>��ӥ��ѡA�|������ DNA�@�~</color>");

                    return false;
                }
                else
                {
                    print("<color=green>DNA �w�B�z�����I</color>");
                }
            }

            if (dataTargetGoal.needCamera)
            {
                if (!dataTargetOriginal.needCamera)
                {
                    print("<color=green>��Ӧ��\</color>");
                    textActionMessage.text = nameTarget + " ��Ӧ��\";
                    dataTargetOriginal.needCamera = true;

                    MissionObjectManager.instance.UpdateMission();
                    
                    return true;
                }
                else
                {
                    print("<color=green>��Ӥw�B�z�����I</color>");

                    return false;
                }
            }
            else
            {
                print("<color=red>�����󤣻ݭn���</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn���";

                return false;
            }
        }

        /// <summary>
        /// �ˬd DNA
        /// </summary>
        /// <returns></returns>
        private bool UseDNA()
        {
            print("�ˬd DNA");

            if (dataTargetGoal.needDNA)
            {
                print("<color=green>�ˬd DNA ���\</color>");
                textActionMessage.text = nameTarget + " �ˬd DNA ���\";
                dataTargetOriginal.needDNA = true;

                MissionObjectManager.instance.UpdateMission();

                return true;
            }
            else
            {
                print("<color=red>�����󤣻ݭn�ˬd DNA</color>");
                textActionMessage.text = nameTarget + " �����󤣻ݭn�ˬd DNA";

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
        public void ToolIconEffect(TypeEvidence typeChooseTool)
        {
            IEnumerator coroutine = null;

            switch (typeChooseTool)
            {
                case TypeEvidence.FlashLight:
                    break;
                case TypeEvidence.EvidenceBag:
                    coroutine = EvidenceBag();
                    break;
                case TypeEvidence.FingerPrint:
                case TypeEvidence.DNA:
                    coroutine = IconMove();
                    break;
                case TypeEvidence.Camera:
                    coroutine = CameraEffect();
                    break;
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
                objectToCheckCurrent.goFingerPrints[i].color = new Color(1, 1, 1, 1);
            }
        }

        /// <summary>
        /// ��v���ĪG�G�W��P�U������֪�
        /// </summary>
        private IEnumerator CameraEffect()
        {
            imgIconTool.color = new Color(1, 1, 1, 0);

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
