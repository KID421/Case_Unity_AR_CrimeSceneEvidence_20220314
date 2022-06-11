using UnityEngine;

namespace KID
{
    /// <summary>
    /// 物件資料
    /// </summary>
    [CreateAssetMenu(menuName = "KID/Data Object", fileName = "Data Object")]
    public class DataObject : ScriptableObject
    {
        //[Header("物件類型"), EnumFlagsAttribute]
        //public TypeObjectToCheck type;

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
