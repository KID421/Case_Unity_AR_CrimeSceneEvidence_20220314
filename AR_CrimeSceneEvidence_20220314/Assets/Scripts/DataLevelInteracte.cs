using UnityEngine;

namespace KID
{
    /// <summary>
    /// 關卡互動資料
    /// </summary>
    public class DataLevelInteracte : MonoBehaviour
    {
        [Header("要被偵測的所有物件")]
        public ObjectToCheck[] objectsToCheck;

        /// <summary>
        /// 關卡要完成的目標
        /// </summary>
        public DataObject[] dataLevelGoal;
        /// <summary>
        /// 關卡內原始的物件
        /// </summary>
        public DataObject[] dataLevelOriginal;

        public static DataLevelInteracte instance;

        private void Awake()
        {
            instance = this;

            for (int i = 0; i < dataLevelOriginal.Length; i++)
            {
                dataLevelOriginal[i].ResetData();
            }
        }

        [ContextMenu("Get All Data")]
        private void GetAllData()
        {
            dataLevelGoal = new DataObject[objectsToCheck.Length];
            dataLevelOriginal = new DataObject[objectsToCheck.Length];

            for (int i = 0; i < objectsToCheck.Length; i++)
            {
                dataLevelGoal[i] = objectsToCheck[i].dataGoal;
                dataLevelOriginal[i] = objectsToCheck[i].dataOriginal;
            }
        }
    }
}
