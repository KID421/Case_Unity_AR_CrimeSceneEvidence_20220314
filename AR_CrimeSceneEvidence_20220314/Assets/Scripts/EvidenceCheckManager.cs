using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// �Ҫ��ˬd�޲z��
    /// </summary>
    [DefaultExecutionOrder(100)]
    public class EvidenceCheckManager : MonoBehaviour
    {
        #region ��ܸ��
        [SerializeField, Header("��ƭ�����")]
        private DataPageContent dataPageContentFingerPrint;
        [SerializeField, Header("��ƭ���M")]
        private DataPageContent dataPageContentGloves;
        [SerializeField, Header("��ƭ��b�}��")]
        private DataPageContent dataPageContentBetelNut;
        #endregion

        #region �Ҫ����
        private List<DataObject> typeCamera = new List<DataObject>();
        private List<DataObject> typeFingerPrint = new List<DataObject>();
        private List<DataObject> typeDNA = new List<DataObject>();
        private List<DataObject> typeEvidenceBag = new List<DataObject>();
        private List<DataObject> typeScale = new List<DataObject>();
        private List<DataObject> typeFlashLight = new List<DataObject>();

        private List<DataObject> allEvidence = new List<DataObject>();

        /// <summary>
        /// �ثe������Ҫ����
        /// </summary>
        private List<DataObject> currentEvidence = new List<DataObject>();

        /// <summary>
        /// �Ҫ��ؤ��Ҫ��s��
        /// </summary>
        private int indexEvidence;
        #endregion

        #region �������
        /// <summary>
        /// �Ҫ��s��
        /// </summary>
        private Text textEvidenceIndex;
        /// <summary>
        /// �Ҫ��Ϥ�
        /// </summary>
        private Image imgEvidencePicture;
        /// <summary>
        /// �Ҫ��ഫ
        /// </summary>
        private Button btnChange;
        /// <summary>
        /// �Ҫ���j
        /// </summary>
        private Button btnZoomIn;
        /// <summary>
        /// ���s���ҩ��
        /// </summary>
        private Button btnCamera;
        /// <summary>
        /// ���s���ҫ���
        /// </summary>
        private Button btnFingerPrint;
        /// <summary>
        /// ���s���� DNA
        /// </summary>
        private Button btnDNA;
        /// <summary>
        /// ���s�����Ҫ��U
        /// </summary>
        private Button btnEvidenceBag;
        /// <summary>
        /// ���s���Ҿc�L
        /// </summary>
        private Button btnShoes;
        /// <summary>
        /// �Ҫ��ƶq
        /// </summary>
        private Text textEvidenceCount;
        /// <summary>
        /// �W�@���Ҫ�
        /// </summary>
        private Button btnEvidencePrev;
        /// <summary>
        /// �U�@���Ҫ�
        /// </summary>
        private Button btnEvidenceNext;
        /// <summary>
        /// ���������s
        /// </summary>
        private Button btnFingerPrintDetach;
        private CanvasGroup groupBtnFingerPrintDetach;

        /// <summary>
        /// �ݵ��D��
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

        // ��ƭ�
        private Button btnDataPagePrev;
        private Button btnDataPageNext;
        private Image imgDataPagePicture;
        private Text textDataPageContent;
        #endregion

        #region ��ƭ����
        private int indexDataPage;
        private DataPageContent dataPageContentCurrent;
        #endregion

        #region �������p�H���
        private bool isFingerPrint;
        private bool isEvidenceBag;
        private DataObject dataCurrent;
        /// <summary>
        /// ��ƭ�
        /// </summary>
        private Image imgDataPageRoot;
        /// <summary>
        /// �}�һP����
        /// </summary>
        private Button btnOpenAndClose;
        /// <summary>
        /// ��ƭ��L�k�ݪ��C��G�Ǧ�
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
        /// �Ҫ�����
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
        /// �M�䤶������
        /// </summary>
        private void FindUIObject()
        {
            #region �����������s   
            btnCamera = GameObject.Find("���s���ҩ��").GetComponent<Button>();
            btnFingerPrint = GameObject.Find("���s���ҫ���").GetComponent<Button>();
            btnDNA = GameObject.Find("���s���� DNA").GetComponent<Button>();
            btnEvidenceBag = GameObject.Find("���s�����Ҫ��U").GetComponent<Button>();
            btnShoes = GameObject.Find("���s���Ҿc�L").GetComponent<Button>();

            btnCamera.onClick.AddListener(() => InitializeEvidenceInformation(typeCamera));
            btnFingerPrint.onClick.AddListener(() => InitializeEvidenceInformation(typeFingerPrint, true));
            btnDNA.onClick.AddListener(() => InitializeEvidenceInformation(typeDNA));
            btnEvidenceBag.onClick.AddListener(() => InitializeEvidenceInformation(typeEvidenceBag, _isEvidenceBag: true));
            btnShoes.onClick.AddListener(() => InitializeEvidenceInformation(typeScale));
            #endregion

            #region �Ҫ���
            textEvidenceIndex = GameObject.Find("�Ҫ��s��").GetComponent<Text>();
            imgEvidencePicture = GameObject.Find("�Ҫ��Ϥ�").GetComponent<Image>();
            textEvidenceCount = GameObject.Find("�Ҫ��ƶq").GetComponent<Text>();
            btnEvidencePrev = GameObject.Find("�W�@���Ҫ�").GetComponent<Button>();
            btnEvidenceNext = GameObject.Find("�U�@���Ҫ�").GetComponent<Button>();
            btnChange = GameObject.Find("�Ҫ��ഫ").GetComponent<Button>();
            btnZoomIn = GameObject.Find("�Ҫ���j").GetComponent<Button>();

            btnEvidencePrev.onClick.AddListener(() => ChangeEvidenceInformation(-1));
            btnEvidenceNext.onClick.AddListener(() => ChangeEvidenceInformation(+1));
            #endregion

            #region �ݵ���
            textQuestion = GameObject.Find("�ݵ��D��").GetComponent<Text>();
            textResult = GameObject.Find("�ݵ����G").GetComponent<Text>();
            btnTextOption1 = GameObject.Find("��r���ﶵ�@").GetComponent<Button>();
            btnTextOption2 = GameObject.Find("��r���ﶵ�G").GetComponent<Button>();
            btnTextOption3 = GameObject.Find("��r���ﶵ�T").GetComponent<Button>();
            textBtnTextOption1 = btnTextOption1.GetComponentInChildren<Text>();
            textBtnTextOption2 = btnTextOption2.GetComponentInChildren<Text>();
            textBtnTextOption3 = btnTextOption3.GetComponentInChildren<Text>();
            btnImageOption1 = GameObject.Find("�Ϥ����ﶵ�@").GetComponent<Button>();
            btnImageOption2 = GameObject.Find("�Ϥ����ﶵ�G").GetComponent<Button>();
            btnImageOption3 = GameObject.Find("�Ϥ����ﶵ�T").GetComponent<Button>();
            imgBtnImageOption1 = btnImageOption1.GetComponent<Image>();
            imgBtnImageOption2 = btnImageOption2.GetComponent<Image>();
            imgBtnImageOption3 = btnImageOption3.GetComponent<Image>();

            btnTextOption1.onClick.AddListener(() => CheckAnswer(1));
            btnTextOption2.onClick.AddListener(() => CheckAnswer(2));
            btnTextOption3.onClick.AddListener(() => CheckAnswer(3));
            btnImageOption1.onClick.AddListener(() => CheckAnswer(1));
            btnImageOption2.onClick.AddListener(() => CheckAnswer(2));
            btnImageOption3.onClick.AddListener(() => CheckAnswer(3));

            groupText = GameObject.Find("��r���D��").GetComponent<CanvasGroup>();
            groupImage = GameObject.Find("�Ϥ����D��").GetComponent<CanvasGroup>();

            btnFingerPrintDetach = GameObject.Find("���������s").GetComponent<Button>();
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

            #region ��ƭ�
            btnDataPagePrev = GameObject.Find("��ƭ��W�@��").GetComponent<Button>();
            btnDataPageNext = GameObject.Find("��ƭ��U�@��").GetComponent<Button>();
            imgDataPagePicture = GameObject.Find("��ƭ��Ϥ�").GetComponent<Image>();
            textDataPageContent = GameObject.Find("��ƭ����e").GetComponent<Text>();

            btnDataPagePrev.onClick.AddListener(() => PrevAndNextEvidenceDataPage(-1));
            btnDataPageNext.onClick.AddListener(() => PrevAndNextEvidenceDataPage(+1));

            imgDataPageRoot = GameObject.Find("��ƭ�").GetComponent<Image>();
            btnOpenAndClose = GameObject.Find("�}�һP����").GetComponent<Button>();
            btnOpenAndClose.onClick.AddListener(() => dataCurrent.useDataPage = true);
            #endregion
        }

        /// <summary>
        /// ��l���Ҫ��ؤ�����T
        /// </summary>
        private void InitializeEvidenceInformation(List<DataObject> _dataObject, bool _isFingerPrint = false, bool _isEvidenceBag = false)
        {
            isFingerPrint = _isFingerPrint;
            isEvidenceBag = _isEvidenceBag;

            if (_dataObject.Count == 0)
            {
                textEvidenceIndex.text = "�L�Ҫ�";
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
        /// �ܧ��Ҫ��ؤ�����T
        /// </summary>
        /// <param name="direction">-1 �W�@�ө� 1 �U�@��</param>
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
        /// ��s�Ҫ��ؤ�����r�P�Ϥ�
        /// </summary>
        /// <param name="_dataObject">�n��s���Ҫ��M��</param>
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
                textResult.text = "�w�g����";
            }

            CheckIsChooseAndUpdateDataPage();
            SwitchFingerPrintDetachButton(!dataCurrent.isCorrectFingerPrint && isFingerPrint && dataCurrent.isCorrect);
        }

        /// <summary>
        /// �����D�ظs��
        /// </summary>
        /// <param name="optionText">��r���D�جO�_�n���</param>
        /// <param name="optionImage">�Ϥ����D�جO�_�n���</param>
        /// <param name="cantInteractable">�O�_�ब�ʡA����O�i�]�w�� false ��ܦ����ब��</param>
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
        /// �ˬd����
        /// </summary>
        /// <param name="_option">�ﶵ</param>
        private void CheckAnswer(int _option)
        {
            int answer = currentEvidence[indexEvidence].indexAnswer;

            if (dataCurrent.isCorrect) answer = currentEvidence[indexEvidence].indexAnswerFingerPrint;

            bool result = _option == answer;
            textResult.text = result ? "���T����" : "���~����";

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
                SwitchFingerPrintDetachButton(isFingerPrint && result);     // �O���� �åB �^�����T
                dataCurrent.isCorrect = result;
            }
            else
            {
                dataCurrent.isCorrectFingerPrint = result;
            }
        }

        /// <summary>
        /// �ˬd�O�_��L�ç�s��ƭ�
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
        /// ��ܫ�������D��
        /// </summary>
        private void SwitchFingerPrintDetachButton(bool show = true)
        {
            groupBtnFingerPrintDetach.alpha = show ? 1 : 0;
            groupBtnFingerPrintDetach.interactable = show;
            groupBtnFingerPrintDetach.blocksRaycasts = show;
        }

        /// <summary>
        /// ��s�Ҫ���ƭ�
        /// </summary>
        private void UpdateEvidenceDataPage(DataPageContent _dataPageContent)
        {
            if (dataCurrent.nameEvidenvce.Contains("��M")) _dataPageContent = dataPageContentGloves;
            else if (dataCurrent.nameEvidenvce.Contains("�b�}��")) _dataPageContent = dataPageContentBetelNut;

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
        /// �W�@���P�U�@����ƭ����e
        /// </summary>
        /// <param name="direction">��V</param>
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

