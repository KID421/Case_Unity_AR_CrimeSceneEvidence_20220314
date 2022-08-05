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
    }
}
