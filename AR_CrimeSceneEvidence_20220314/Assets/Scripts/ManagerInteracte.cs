using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace KID
{
    /// <summary>
    /// 互動管理器
    /// </summary>
    public class ManagerInteracte : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("AR Camera")]
        private Camera cam;
        [SerializeField, Header("射線距離"), Range(0, 100)]
        private float camLength = 10;
        [SerializeField, Header("要偵測的物件圖層")]
        private LayerMask layerToCheck;
        [SerializeField, Header("圖示 DNA 棉花棒")]
        private Sprite spriteIconDNACottonSwab;
        [SerializeField, Header("圖示比例尺")]
        private Sprite spriteIconScale;
        [SerializeField, Header("圖示毛刷")]
        private Sprite spriteIconBrush;
        [SerializeField, Header("圖示相機")]
        private Sprite spriteIconCamera;
        [SerializeField, Header("圖示證物袋")]
        private Sprite spriteIconEvidenceBag;
        [SerializeField, Header("後期處理體積物件")]
        private GameObject goPPVolume;

        /// <summary>
        /// 工具圖示
        /// </summary>
        private Image imgIconTool;
        private RectTransform rectImgIconTool;
        /// <summary>
        /// 選中的物件
        /// </summary>
        private Text textChooseObject;
        /// <summary>
        /// 動作訊息
        /// </summary>
        private Text textActionMessage;
        /// <summary>
        /// 工具手電筒
        /// </summary>
        private Button btnFlashLight;
        /// <summary>
        /// 工具DNA
        /// </summary>
        private Button btnDNA;
        /// <summary>
        /// 工具證物袋
        /// </summary>
        private Button btnEvidenceBag;
        /// <summary>
        /// 工具比例尺
        /// </summary>
        private Button btnScale;
        /// <summary>
        /// 工具指紋
        /// </summary>
        private Button btnFingerPrint;
        /// <summary>
        /// 工具相機
        /// </summary>
        private Button btnCamera;
        /// <summary>
        /// 使用
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
            textChooseObject = GameObject.Find("選中的物件").GetComponent<Text>();
            textActionMessage = GameObject.Find("動作訊息").GetComponent<Text>();
            imgIconTool = GameObject.Find("工具圖示").GetComponent<Image>();
            rectImgIconTool = imgIconTool.GetComponent<RectTransform>();
            imgCameraEffectTop = GameObject.Find("拍照效果上方").GetComponent<Image>();
            imgCameraEffectBottom = GameObject.Find("拍照效果下方").GetComponent<Image>();

            ToolAndUseButtonsFind();
            ToolButtonsClick();
            btnUse.onClick.AddListener(() => UseTool());
        }

        private void Update()
        {
            CheckLookObject();
        }

        /// <summary>
        /// 工具與使用按鈕尋找
        /// </summary>
        private void ToolAndUseButtonsFind()
        {
            btnFlashLight = GameObject.Find("工具手電筒").GetComponent<Button>();
            btnDNA = GameObject.Find("工具DNA").GetComponent<Button>();
            btnEvidenceBag = GameObject.Find("工具證物袋").GetComponent<Button>();
            btnScale = GameObject.Find("工具比例尺").GetComponent<Button>();
            btnFingerPrint = GameObject.Find("工具指紋").GetComponent<Button>();
            btnCamera = GameObject.Find("工具相機").GetComponent<Button>();
            btnUse = GameObject.Find("使用").GetComponent<Button>();
        }

        /// <summary>
        /// 工具按鈕點擊
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
        /// 選擇工具
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
        /// 使用工具
        /// </summary>
        private void UseTool()
        {
            // 如果不是手電筒 並且 如果沒有目標資料就跳出
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
        /// 使用手電筒
        /// </summary>
        private bool UseFlashLight()
        {
            print("使用手電筒");

            
            if (dataTargetGoal.needFlashLight)
            {
                print("<color=green>使用手電筒成功</color>");
                textActionMessage.text = nameTarget + " 使用手電筒成功";
                dataTargetOriginal.needFlashLight = true;

                goPPVolume.SetActive(!goPPVolume.activeInHierarchy);
                return true;
            }
            else
            {
                print("<color=red>此物件不需要使用手電筒</color>");
                textActionMessage.text = nameTarget + " 此物件不需要使用手電筒";

                return false;
            }
        }

        /// <summary>
        /// 放入證物袋
        /// </summary>
        private bool UseEvidenceBag()
        {
            print("放入證物袋");

            if (dataTargetGoal.needFlashLight)
            {
                if (!dataTargetOriginal.needFlashLight)
                {
                    print("<color=red>放入證物袋失敗，尚未完成手電筒偵測</color>");

                    return false;
                }
                else
                {
                    print("<color=green>手電筒偵測已處理完成！</color>");
                }
            }

            if (dataTargetGoal.needCamera)
            {
                if (!dataTargetOriginal.needCamera)
                {
                    print("<color=red>放入證物袋失敗，尚未完成拍照</color>");

                    return false;
                }
                else
                {
                    print("<color=green>拍照已處理完成！</color>");
                }
            }

            if (dataTargetGoal.needEvidenceBag)
            {
                print("<color=green>放入證物袋成功</color>");
                textActionMessage.text = nameTarget + " 放入證物袋成功";
                dataTargetOriginal.needEvidenceBag = true;

                MissionObjectManager.instance.UpdateMission();

                return true;
            }
            else
            {
                print("<color=red>此物件不需要放入證物袋</color>");
                textActionMessage.text = nameTarget + " 此物件不需要放入證物袋";

                return false;
            }
        }

        /// <summary>
        /// 測量尺寸
        /// </summary>
        private bool UseScale()
        {
            print("測量尺寸");

            if (dataTargetGoal.needScale)
            {
                print("<color=green>測量尺寸成功</color>");
                textActionMessage.text = nameTarget + " 測量尺寸成功";
                dataTargetOriginal.needScale = true;
                objectToCheckCurrent.goScale.SetActive(true);
                imgIconTool.color = new Color(1, 1, 1, 0);

                return true;
            }
            else
            {
                print("<color=red>此物件不需要測量尺寸</color>");
                textActionMessage.text = nameTarget + " 此物件不需要測量尺寸";

                return false;
            }
        }

        /// <summary>
        /// 採集指紋
        /// </summary>
        private bool UseFingerPrint()
        {
            print("採集指紋");

            if (dataTargetGoal.needFingerPrint)
            {
                if (dataTargetOriginal.needFingerPrint)
                {
                    print("<color=green>已經採集過指紋</color>");

                    return false;
                }
                else
                {
                    print("<color=green>採集指紋成功</color>");
                    textActionMessage.text = nameTarget + " 採集指紋成功";
                    dataTargetOriginal.needFingerPrint = true;
                }

                return true;
            }
            else
            {
                print("<color=red>此物件不需採集指紋</color>");
                textActionMessage.text = nameTarget + " 此物件不需採集指紋";

                return false;
            }
        }

        /// <summary>
        /// 拍照
        /// </summary>
        private bool UseCamera()
        {
            print("拍照");

            bool fingerPrint = dataTargetGoal.needFingerPrint != dataTargetOriginal.needFingerPrint;
            bool scale = dataTargetGoal.needScale != dataTargetOriginal.needScale;
            bool dna = dataTargetGoal.needDNA != dataTargetOriginal.needDNA;

            if (dataTargetGoal.needFingerPrint)
            {
                if (!dataTargetOriginal.needFingerPrint)
                {
                    print("<color=red>拍照失敗，尚未完成指紋作業</color>");

                    return false;
                }
                else
                {
                    print("<color=green>指紋已處理完成！</color>");
                }
            }

            if (dataTargetGoal.needScale)
            {
                if (!dataTargetOriginal.needScale)
                {
                    print("<color=red>拍照失敗，尚未完成測量作業</color>");

                    return false;
                }
                else
                {
                    print("<color=green>測量已處理完成！</color>");
                }
            }

            if (dataTargetGoal.needDNA)
            {
                if (!dataTargetOriginal.needDNA)
                {
                    print("<color=red>拍照失敗，尚未完成 DNA作業</color>");

                    return false;
                }
                else
                {
                    print("<color=green>DNA 已處理完成！</color>");
                }
            }

            if (dataTargetGoal.needCamera)
            {
                if (!dataTargetOriginal.needCamera)
                {
                    print("<color=green>拍照成功</color>");
                    textActionMessage.text = nameTarget + " 拍照成功";
                    dataTargetOriginal.needCamera = true;

                    MissionObjectManager.instance.UpdateMission();
                    
                    return true;
                }
                else
                {
                    print("<color=green>拍照已處理完成！</color>");

                    return false;
                }
            }
            else
            {
                print("<color=red>此物件不需要拍照</color>");
                textActionMessage.text = nameTarget + " 此物件不需要拍照";

                return false;
            }
        }

        /// <summary>
        /// 檢查 DNA
        /// </summary>
        /// <returns></returns>
        private bool UseDNA()
        {
            print("檢查 DNA");

            if (dataTargetGoal.needDNA)
            {
                print("<color=green>檢查 DNA 成功</color>");
                textActionMessage.text = nameTarget + " 檢查 DNA 成功";
                dataTargetOriginal.needDNA = true;

                MissionObjectManager.instance.UpdateMission();

                return true;
            }
            else
            {
                print("<color=red>此物件不需要檢查 DNA</color>");
                textActionMessage.text = nameTarget + " 此物件不需要檢查 DNA";

                return false;
            }
        }

        /// <summary>
        /// 檢查看到的物件
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
        /// 變更圖示
        /// </summary>
        /// <param name="typeChooseTool">工具類型</param>
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
        /// 圖示移動效果：筆刷
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
        /// 攝影機效果：上方與下方模擬快門
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
        /// 放入證物袋
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
