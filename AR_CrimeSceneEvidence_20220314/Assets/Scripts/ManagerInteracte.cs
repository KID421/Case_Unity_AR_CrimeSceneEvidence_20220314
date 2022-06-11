using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// 互動管理器
    /// </summary>
    public class ManagerInteracte : MonoBehaviour
    {
        [SerializeField, Header("AR Camera")]
        private Camera cam;
        [SerializeField, Header("射線距離"), Range(0, 100)]
        private float camLength = 10;
        [SerializeField, Header("要偵測的物件圖層")]
        private LayerMask layerToCheck;

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
        /// 工具證物袋
        /// </summary>
        private Button btnEvidenceBag;
        /// <summary>
        /// 工具DNA
        /// </summary>
        private Button btnDNA;
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
            textChooseObject = GameObject.Find("選中的物件").GetComponent<Text>();
            textActionMessage = GameObject.Find("動作訊息").GetComponent<Text>();

            ToolAndUseButtonsFind();
            ToolButtonsClick();
            UseTool();
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
            btnEvidenceBag = GameObject.Find("工具證物袋").GetComponent<Button>();
            btnDNA = GameObject.Find("工具DNA").GetComponent<Button>();
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
            btnFlashLight.onClick.AddListener(() => typeChooseTool = TypeChooseTool.FlashLight);
            btnEvidenceBag.onClick.AddListener(() => typeChooseTool = TypeChooseTool.EvidenceBag);
            btnDNA.onClick.AddListener(() => typeChooseTool = TypeChooseTool.DNA);
            btnScale.onClick.AddListener(() => typeChooseTool = TypeChooseTool.Scale);
            btnFingerPrint.onClick.AddListener(() => typeChooseTool = TypeChooseTool.FingerPrint);
            btnCamera.onClick.AddListener(() => typeChooseTool = TypeChooseTool.Camera);
        }

        /// <summary>
        /// 使用工具
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
        /// 使用手電筒
        /// </summary>
        private void UseFlashLight()
        {
            print("使用手電筒");

            if (dataTargetGoal.needFlashLight)
            {
                print("<color=green>使用手電筒成功</color>");
                textActionMessage.text = nameTarget + " 使用手電筒成功";
                dataTargetOriginal.needFlashLight = true;
            }
            else
            {
                print("<color=red>此物件不需要使用手電筒</color>");
                textActionMessage.text = nameTarget + " 此物件不需要使用手電筒";
            }
        }

        /// <summary>
        /// 放入證物袋
        /// </summary>
        private void UseEvidenceBag()
        {
            print("放入證物袋");

            if (dataTargetGoal.needEvidenceBag)
            {
                print("<color=green>放入證物袋成功</color>");
                textActionMessage.text = nameTarget + " 放入證物袋成功";
                dataTargetOriginal.needEvidenceBag = true;
                goTarget.SetActive(false);
            }
            else
            {
                print("<color=red>此物件不需要放入證物袋成功</color>");
                textActionMessage.text = nameTarget + " 此物件不需要放入證物袋成功";
            }
        }

        /// <summary>
        /// 採集 DNA
        /// </summary>
        private void UseDNA()
        {
            print("採集 DNA");

            if (dataTargetGoal.needDNA)
            {
                print("<color=green>採集 DNA成功</color>");
                textActionMessage.text = nameTarget + " 採集 DNA成功";
                dataTargetOriginal.needDNA = true;
            }
            else
            {
                print("<color=red>此物件不需要採集 DNA</color>");
                textActionMessage.text = nameTarget + " 此物件不需要採集 DNA";
            }
        }

        /// <summary>
        /// 測量尺寸
        /// </summary>
        private void UseScale()
        {
            print("測量尺寸");

            if (dataTargetGoal.needScale)
            {
                print("<color=green>測量尺寸成功</color>");
                textActionMessage.text = nameTarget + " 測量尺寸成功";
                dataTargetOriginal.needScale = true;
            }
            else
            {
                print("<color=red>此物件不需要測量尺寸</color>");
                textActionMessage.text = nameTarget + " 此物件不需要測量尺寸";
            }
        }

        /// <summary>
        /// 採集指紋
        /// </summary>
        private void UseFingerPrint()
        {
            print("採集指紋");

            if (dataTargetGoal.needFingerPrint)
            {
                print("<color=green>採集指紋成功</color>");
                textActionMessage.text = nameTarget + " 採集指紋成功";
                dataTargetOriginal.needFingerPrint = true;

                ObjectToCheck objectToCheck = goTarget.GetComponent<ObjectToCheck>();

                for (int i = 0; i < objectToCheck.goFingerPrints.Length; i++)
                {
                    objectToCheck.goFingerPrints[i].SetActive(true);
                }
            }
            else
            {
                print("<color=red>此物件不需採集指紋</color>");
                textActionMessage.text = nameTarget + " 此物件不需採集指紋";
            }
        }

        /// <summary>
        /// 拍照
        /// </summary>
        private void UseCamera()
        {
            print("拍照");

            if (dataTargetGoal.needCamera)
            {
                print("<color=green>拍照成功</color>");
                textActionMessage.text = nameTarget + " 拍照成功";
                dataTargetOriginal.needCamera = true;
            }
            else
            {
                print("<color=red>此物件不需要拍照</color>");
                textActionMessage.text = nameTarget + " 此物件不需要拍照";
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

                dataTargetGoal = goTarget.GetComponent<ObjectToCheck>().dataGoal;
                dataTargetOriginal = goTarget.GetComponent<ObjectToCheck>().dataOriginal;
            }
        }
    }
}
