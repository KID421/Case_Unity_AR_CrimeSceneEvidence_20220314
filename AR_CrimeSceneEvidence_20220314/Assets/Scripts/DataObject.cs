using UnityEngine;

namespace KID
{
    /// <summary>
    /// 物件資料
    /// </summary>
    [CreateAssetMenu(menuName = "KID/Data Object", fileName = "Data Object")]
    public class DataObject : ScriptableObject
    {
        [Header("證物名稱")]
        public string nameEvidenvce;
        [Header("證物圖片")]
        public Sprite sprImage;
        [Header("證物指紋圖片")]
        public Sprite sprImageFingerPrint;
        [Header("是否需要拍照")]
        public bool needCamera;
        [Header("是否需要收入證物袋")]
        public bool needEvidenceBag;
        [Header("是否需要測量尺寸")]
        public bool needScale;
        [Header("是否需要採集 DNA")]
        public bool needDNA;
        [Header("是否需要採集指紋")]
        public bool needFingerPrint;
        [Header("是否需要手電筒")]
        public bool needFlashLight;

        [Header("題目與選項")]
        public TypeQuestion typeQuestion;
        public string stringQuestion;
        public string textOption1;
        public string textOption2;
        public string textOption3;
        public Sprite imgOption1;
        public Sprite imgOption2;
        public Sprite imgOption3;
        [Header("正確答案"), Range(1, 3)]
        public int indexAnswer;
        [Header("指紋比對正確答案"), Range(1, 3)]
        public int indexAnswerFingerPrint;

        /// <summary>
        /// 是否答對
        /// </summary>
        public bool isCorrect;
        /// <summary>
        /// 是否答對指紋比對
        /// </summary>
        public bool isCorrectFingerPrint;
        /// <summary>
        /// 是否選過此題目
        /// </summary>
        public bool isChoose;
        /// <summary>
        /// 選取答案的次數
        /// </summary>
        public int countChooseAnswer;
        /// <summary>
        /// 是否使用資料頁
        /// </summary>
        public bool useDataPage;
        /// <summary>
        /// 選取答案的次數 - 指紋比對
        /// </summary>
        public int countChooseAnswerFingerPrint;

        /// <summary>
        /// 總分
        /// </summary>
        private int scoreTotal = 10;
        /// <summary>
        /// 每次錯誤扣分
        /// </summary>
        private int scorePerWrong = 5;

        /// <summary>
        /// 得分
        /// </summary>
        public int score => countChooseAnswer == 0 ? 0 : scoreTotal - countChooseAnswer * scorePerWrong;
        /// <summary>
        /// 指紋比對分數
        /// </summary>
        public int scoreFingerPrint => needFingerPrint ? scoreTotal - countChooseAnswerFingerPrint * scorePerWrong : 0;

        /// <summary>
        /// 重設資料
        /// </summary>
        public void ResetData()
        {
            needCamera = false;
            needEvidenceBag = false;
            needScale = false;
            needDNA = false;
            needFingerPrint = false;
            needFlashLight = false;
        }

        /// <summary>
        ///重新定為上未正確狀態：目標資料用
        /// </summary>
        public void ResetToNotCorrect()
        {
            isChoose = false;
            isCorrect = false;
            isCorrectFingerPrint = false;
            countChooseAnswer = 0;
            useDataPage = false;
            countChooseAnswerFingerPrint = 0;
        }
    }
}
