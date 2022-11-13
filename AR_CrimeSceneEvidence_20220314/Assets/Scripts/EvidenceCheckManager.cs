using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// 證物檢查管理器
    /// </summary>
    [DefaultExecutionOrder(100)]
    public class EvidenceCheckManager : MonoBehaviour
    {
        #region 顯示資料
        [SerializeField, Header("資料頁指紋")]
        private DataPageContent dataPageContentFingerPrint;
        [SerializeField, Header("資料頁手套")]
        private DataPageContent dataPageContentGloves;
        [SerializeField, Header("資料頁檳榔渣")]
        private DataPageContent dataPageContentBetelNut;
        #endregion

        #region 證物資料
        private List<DataObject> typeCamera = new List<DataObject>();
        private List<DataObject> typeFingerPrint = new List<DataObject>();
        private List<DataObject> typeDNA = new List<DataObject>();
        private List<DataObject> typeEvidenceBag = new List<DataObject>();
        private List<DataObject> typeScale = new List<DataObject>();
        private List<DataObject> typeFlashLight = new List<DataObject>();

        private List<DataObject> allEvidence = new List<DataObject>();

        /// <summary>
        /// 目前選取的證物資料
        /// </summary>
        private List<DataObject> currentEvidence = new List<DataObject>();

        /// <summary>
        /// 證物框內證物編號
        /// </summary>
        private int indexEvidence;
        #endregion

        #region 介面資料
        /// <summary>
        /// 證物編號
        /// </summary>
        private Text textEvidenceIndex;
        /// <summary>
        /// 證物圖片
        /// </summary>
        private Image imgEvidencePicture;
        /// <summary>
        /// 證物轉換
        /// </summary>
        private Button btnChange;
        /// <summary>
        /// 證物放大
        /// </summary>
        private Button btnZoomIn;
        /// <summary>
        /// 按鈕採證拍照
        /// </summary>
        private Button btnCamera;
        /// <summary>
        /// 按鈕採證指紋
        /// </summary>
        private Button btnFingerPrint;
        /// <summary>
        /// 按鈕採證 DNA
        /// </summary>
        private Button btnDNA;
        /// <summary>
        /// 按鈕採證證物袋
        /// </summary>
        private Button btnEvidenceBag;
        /// <summary>
        /// 按鈕採證鞋印
        /// </summary>
        private Button btnShoes;
        /// <summary>
        /// 證物數量
        /// </summary>
        private Text textEvidenceCount;
        /// <summary>
        /// 上一個證物
        /// </summary>
        private Button btnEvidencePrev;
        /// <summary>
        /// 下一個證物
        /// </summary>
        private Button btnEvidenceNext;
        /// <summary>
        /// 指紋比對按鈕
        /// </summary>
        private Button btnFingerPrintDetach;
        private CanvasGroup groupBtnFingerPrintDetach;

        /// <summary>
        /// 問答題目
        /// </summary>
        private Text textQuestion;
        private Button btnTextOption1;
        private Button btnTextOption2;
        private Button btnTextOption3;
        private Text textBtnTextOption1;
        private Text textBtnTextOption2;
        private Text textBtnTextOption3;
        private Button btnImageOption1;
        private Button btnImageOption2;
        private Button btnImageOption3;
        private Image imgBtnImageOption1;
        private Image imgBtnImageOption2;
        private Image imgBtnImageOption3;
        private Text textResult;
        private CanvasGroup groupText;
        private CanvasGroup groupImage;

        // 資料頁
        private Button btnDataPagePrev;
        private Button btnDataPageNext;
        private Image imgDataPagePicture;
        private Text textDataPageContent;
        #endregion

        #region 資料頁資料
        private int indexDataPage;
        private DataPageContent dataPageContentCurrent;
        #endregion

        #region 未分類私人資料
        private bool isFingerPrint;
        private bool isEvidenceBag;
        private DataObject dataCurrent;
        /// <summary>
        /// 資料頁
        /// </summary>
        private Image imgDataPageRoot;
        /// <summary>
        /// 開啟與關閉
        /// </summary>
        private Button btnOpenAndClose;
        /// <summary>
        /// 資料頁無法看的顏色：灰色
        /// </summary>
        private Color colorImgDataPageRootCantLook = new Color(0.5f, 0.5f, 0.5f, 1);
        #endregion

        private void Awake()
        {
            ClassifyEvidence();
            FindUIObject();
            InitializeEvidenceInformation(typeCamera);
        }

        /// <summary>
        /// 證物分類
        /// </summary>
        private void ClassifyEvidence()
        {
            allEvidence = DataLevelInteracte.instance.dataLevelGoal.ToList();

            typeFlashLight = allEvidence.Where(x => x.needFlashLight).ToList();
            typeEvidenceBag = allEvidence.Where(x => x.needEvidenceBag).ToList();
            typeDNA = allEvidence.Where(x => x.needDNA).ToList();
            typeScale = allEvidence.Where(x => x.needScale).ToList();
            typeFingerPrint = allEvidence.Where(x => x.needFingerPrint).ToList();
            typeCamera = allEvidence.Where(x => x.needCamera).ToList();
        }

        /// <summary>
        /// 尋找介面物件
        /// </summary>
        private void FindUIObject()
        {
            #region 採證類型按鈕   
            btnCamera = GameObject.Find("按鈕採證拍照").GetComponent<Button>();
            btnFingerPrint = GameObject.Find("按鈕採證指紋").GetComponent<Button>();
            btnDNA = GameObject.Find("按鈕採證 DNA").GetComponent<Button>();
            btnEvidenceBag = GameObject.Find("按鈕採證證物袋").GetComponent<Button>();
            btnShoes = GameObject.Find("按鈕採證鞋印").GetComponent<Button>();

            btnCamera.onClick.AddListener(() => InitializeEvidenceInformation(typeCamera));
            btnFingerPrint.onClick.AddListener(() => InitializeEvidenceInformation(typeFingerPrint, true));
            btnDNA.onClick.AddListener(() => InitializeEvidenceInformation(typeDNA));
            btnEvidenceBag.onClick.AddListener(() => InitializeEvidenceInformation(typeEvidenceBag, _isEvidenceBag: true));
            btnShoes.onClick.AddListener(() => InitializeEvidenceInformation(typeScale));
            #endregion

            #region 證物框
            textEvidenceIndex = GameObject.Find("證物編號").GetComponent<Text>();
            imgEvidencePicture = GameObject.Find("證物圖片").GetComponent<Image>();
            textEvidenceCount = GameObject.Find("證物數量").GetComponent<Text>();
            btnEvidencePrev = GameObject.Find("上一個證物").GetComponent<Button>();
            btnEvidenceNext = GameObject.Find("下一個證物").GetComponent<Button>();
            btnChange = GameObject.Find("證物轉換").GetComponent<Button>();
            btnZoomIn = GameObject.Find("證物放大").GetComponent<Button>();

            btnEvidencePrev.onClick.AddListener(() => ChangeEvidenceInformation(-1));
            btnEvidenceNext.onClick.AddListener(() => ChangeEvidenceInformation(+1));
            #endregion

            #region 問答框
            textQuestion = GameObject.Find("問答題目").GetComponent<Text>();
            textResult = GameObject.Find("問答結果").GetComponent<Text>();
            btnTextOption1 = GameObject.Find("文字型選項一").GetComponent<Button>();
            btnTextOption2 = GameObject.Find("文字型選項二").GetComponent<Button>();
            btnTextOption3 = GameObject.Find("文字型選項三").GetComponent<Button>();
            textBtnTextOption1 = btnTextOption1.GetComponentInChildren<Text>();
            textBtnTextOption2 = btnTextOption2.GetComponentInChildren<Text>();
            textBtnTextOption3 = btnTextOption3.GetComponentInChildren<Text>();
            btnImageOption1 = GameObject.Find("圖片型選項一").GetComponent<Button>();
            btnImageOption2 = GameObject.Find("圖片型選項二").GetComponent<Button>();
            btnImageOption3 = GameObject.Find("圖片型選項三").GetComponent<Button>();
            imgBtnImageOption1 = btnImageOption1.GetComponent<Image>();
            imgBtnImageOption2 = btnImageOption2.GetComponent<Image>();
            imgBtnImageOption3 = btnImageOption3.GetComponent<Image>();

            btnTextOption1.onClick.AddListener(() => CheckAnswer(1));
            btnTextOption2.onClick.AddListener(() => CheckAnswer(2));
            btnTextOption3.onClick.AddListener(() => CheckAnswer(3));
            btnImageOption1.onClick.AddListener(() => CheckAnswer(1));
            btnImageOption2.onClick.AddListener(() => CheckAnswer(2));
            btnImageOption3.onClick.AddListener(() => CheckAnswer(3));

            groupText = GameObject.Find("文字型題目").GetComponent<CanvasGroup>();
            groupImage = GameObject.Find("圖片型題目").GetComponent<CanvasGroup>();

            btnFingerPrintDetach = GameObject.Find("指紋比對按鈕").GetComponent<Button>();
            groupBtnFingerPrintDetach = btnFingerPrintDetach.GetComponent<CanvasGroup>();
            btnFingerPrintDetach.onClick.AddListener(() =>
            {
                dataCurrent.typeQuestion = TypeQuestion.Image;
                UpdateEvidenceTextAndImage(typeFingerPrint);
                SwitchOptionGroup(false, true);
                SwitchFingerPrintDetachButton(false);
                textResult.text = "";
            });
            #endregion

            #region 資料頁
            btnDataPagePrev = GameObject.Find("資料頁上一筆").GetComponent<Button>();
            btnDataPageNext = GameObject.Find("資料頁下一筆").GetComponent<Button>();
            imgDataPagePicture = GameObject.Find("資料頁圖片").GetComponent<Image>();
            textDataPageContent = GameObject.Find("資料頁內容").GetComponent<Text>();

            btnDataPagePrev.onClick.AddListener(() => PrevAndNextEvidenceDataPage(-1));
            btnDataPageNext.onClick.AddListener(() => PrevAndNextEvidenceDataPage(+1));

            imgDataPageRoot = GameObject.Find("資料頁").GetComponent<Image>();
            btnOpenAndClose = GameObject.Find("開啟與關閉").GetComponent<Button>();
            btnOpenAndClose.onClick.AddListener(() => dataCurrent.useDataPage = true);
            #endregion
        }

        /// <summary>
        /// 初始化證物框內的資訊
        /// </summary>
        private void InitializeEvidenceInformation(List<DataObject> _dataObject, bool _isFingerPrint = false, bool _isEvidenceBag = false)
        {
            isFingerPrint = _isFingerPrint;
            isEvidenceBag = _isEvidenceBag;

            if (_dataObject.Count == 0)
            {
                textEvidenceIndex.text = "無證物";
                textEvidenceCount.text = "0 / 0";
                textQuestion.text = "";
                textResult.text = "";
                imgEvidencePicture.color = new Color(1, 1, 1, 0);
                currentEvidence = null;
                SwitchOptionGroup(false, false);
                UpdateEvidenceDataPage(null);
                return;
            }

            currentEvidence = _dataObject;
            indexEvidence = 0;

            UpdateEvidenceTextAndImage(currentEvidence);

            if (_isFingerPrint) UpdateEvidenceDataPage(dataPageContentFingerPrint);
            else if (_isEvidenceBag) UpdateEvidenceDataPage(new DataPageContent());
            else UpdateEvidenceDataPage(null);
        }

        /// <summary>
        /// 變更證物框內的資訊
        /// </summary>
        /// <param name="direction">-1 上一個或 1 下一個</param>
        private void ChangeEvidenceInformation(int direction)
        {
            if (currentEvidence == null) return;

            indexEvidence += direction;

            if (indexEvidence < 0) indexEvidence = currentEvidence.Count - 1;
            else if (indexEvidence == currentEvidence.Count) indexEvidence = 0;

            UpdateEvidenceTextAndImage(currentEvidence);

            if (isEvidenceBag) UpdateEvidenceDataPage(new DataPageContent()); 
        }

        /// <summary>
        /// 更新證物框內的文字與圖片
        /// </summary>
        /// <param name="_dataObject">要更新的證物清單</param>
        private void UpdateEvidenceTextAndImage(List<DataObject> _dataObject)
        {
            dataCurrent = _dataObject[indexEvidence];

            textEvidenceIndex.text = dataCurrent.nameEvidenvce;
            imgEvidencePicture.sprite = dataCurrent.sprImage;
            imgEvidencePicture.color = new Color(1, 1, 1, 1);
            textEvidenceCount.text = (indexEvidence + 1) + " / " + _dataObject.Count;

            textResult.text = "";

            if (dataCurrent.typeQuestion == TypeQuestion.None)
            {
                textQuestion.text = "";
                SwitchOptionGroup(false, false);
            }
            else if (dataCurrent.typeQuestion == TypeQuestion.Text || dataCurrent.typeQuestion == TypeQuestion.TextFirstThanImage)
            {
                textQuestion.text = dataCurrent.stringQuestion;
                textBtnTextOption1.text = dataCurrent.textOption1;
                textBtnTextOption2.text = dataCurrent.textOption2;
                textBtnTextOption3.text = dataCurrent.textOption3;
                SwitchOptionGroup(true, false, !dataCurrent.isCorrect);
            }
            else if (dataCurrent.typeQuestion == TypeQuestion.Image)
            {
                textQuestion.text = dataCurrent.stringQuestion;
                imgBtnImageOption1.sprite = dataCurrent.imgOption1;
                imgBtnImageOption2.sprite = dataCurrent.imgOption2;
                imgBtnImageOption3.sprite = dataCurrent.imgOption3;
                SwitchOptionGroup(false, true, !dataCurrent.isCorrect);
            }

            if (dataCurrent.isCorrect)
            {
                textResult.text = "已經答對";
            }

            CheckIsChooseAndUpdateDataPage();
            SwitchFingerPrintDetachButton(!dataCurrent.isCorrectFingerPrint && isFingerPrint && dataCurrent.isCorrect);
        }

        /// <summary>
        /// 切換題目群組
        /// </summary>
        /// <param name="optionText">文字型題目是否要顯示</param>
        /// <param name="optionImage">圖片型題目是否要顯示</param>
        /// <param name="cantInteractable">是否能互動，答對是可設定為 false 顯示但不能互動</param>
        private void SwitchOptionGroup(bool optionText = true, bool optionImage = false, bool cantInteractable = true)
        {
            groupText.alpha = optionText ? 1 : 0;
            groupText.interactable = optionText && cantInteractable;
            groupText.blocksRaycasts = optionText && cantInteractable;

            groupImage.alpha = optionImage ? 1 : 0;
            groupImage.interactable = optionImage && cantInteractable;
            groupImage.blocksRaycasts = optionImage && cantInteractable;
        }

        /// <summary>
        /// 檢查答案
        /// </summary>
        /// <param name="_option">選項</param>
        private void CheckAnswer(int _option)
        {
            int answer = currentEvidence[indexEvidence].indexAnswer;

            if (dataCurrent.isCorrect) answer = currentEvidence[indexEvidence].indexAnswerFingerPrint;

            bool result = _option == answer;
            textResult.text = result ? "正確答案" : "錯誤答案";

            if (!dataCurrent.isCorrect)
            {
                dataCurrent.countChooseAnswer++;
                dataCurrent.isChoose = true;
                CheckIsChooseAndUpdateDataPage();
            }
            else dataCurrent.countChooseAnswerFingerPrint++;

            if (result)
            {
                groupText.interactable = false;
                groupImage.interactable = false;
            }

            if (!dataCurrent.isCorrect)
            {
                SwitchFingerPrintDetachButton(isFingerPrint && result);     // 是指紋 並且 回答正確
                dataCurrent.isCorrect = result;
            }
            else
            {
                dataCurrent.isCorrectFingerPrint = result;
            }
        }

        /// <summary>
        /// 檢查是否選過並更新資料頁
        /// </summary>
        private void CheckIsChooseAndUpdateDataPage()
        {
            if (dataCurrent.isChoose)
            {
                btnOpenAndClose.interactable = true;
                imgDataPageRoot.color = Color.white;
            }
            else
            {
                btnOpenAndClose.interactable = false;
                imgDataPageRoot.color = colorImgDataPageRootCantLook;
            }
        }

        /// <summary>
        /// 顯示指紋比對題目
        /// </summary>
        private void SwitchFingerPrintDetachButton(bool show = true)
        {
            groupBtnFingerPrintDetach.alpha = show ? 1 : 0;
            groupBtnFingerPrintDetach.interactable = show;
            groupBtnFingerPrintDetach.blocksRaycasts = show;
        }

        /// <summary>
        /// 更新證物資料頁
        /// </summary>
        private void UpdateEvidenceDataPage(DataPageContent _dataPageContent)
        {
            if (dataCurrent.nameEvidenvce.Contains("手套")) _dataPageContent = dataPageContentGloves;
            else if (dataCurrent.nameEvidenvce.Contains("檳榔渣")) _dataPageContent = dataPageContentBetelNut;

            if (_dataPageContent == null)
            {
                imgDataPagePicture.color = new Color(1, 1, 1, 0);
                textDataPageContent.text = "";
                return;
            }

            dataPageContentCurrent = _dataPageContent;

            if (dataPageContentCurrent.dataPageContents != null)
            {
                DataPageContentInformation dataPageContentInformation = dataPageContentCurrent.dataPageContents[0];
                imgDataPagePicture.color = new Color(1, 1, 1, 1);
                imgDataPagePicture.sprite = dataPageContentInformation.sprPicture;
                textDataPageContent.text = dataPageContentInformation.stringDescription;
            }
        }

        /// <summary>
        /// 上一筆與下一筆資料頁內容
        /// </summary>
        /// <param name="direction">方向</param>
        private void PrevAndNextEvidenceDataPage(int direction = 1)
        {
            if (dataPageContentCurrent == null) return;

            indexDataPage += direction;

            DataPageContentInformation[] dataPageContentInformation = dataPageContentCurrent.dataPageContents;

            if (indexDataPage == dataPageContentInformation.Length) indexDataPage = 0;
            else if (indexDataPage == -1) indexDataPage = dataPageContentInformation.Length;

            imgDataPagePicture.sprite = dataPageContentInformation[indexDataPage].sprPicture;
            textDataPageContent.text = dataPageContentInformation[indexDataPage].stringDescription;
        }
    }
}

