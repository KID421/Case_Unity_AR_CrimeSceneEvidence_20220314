using UnityEngine;

namespace KID
{
    /// <summary>
    /// 需要被檢查的物件
    /// </summary>
    public class ObjectToCheck : MonoBehaviour
    {
        [Header("物件資料：關卡目標")]
        public DataObject dataGoal;
        [Header("物件資料：關卡原始")]
        public DataObject dataOriginal;
        [Header("要顯示的指紋")]
        public GameObject[] goFingerPrints;
        [Header("特效")]
        public ParticleSystem psEffect;
        [Header("比例尺")]
        public GameObject goScale;
    }
}
