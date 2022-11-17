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

        /// <summary>
        /// 總分
        /// </summary>
        public int scoreTotal => ScoreTotal();

        private void Awake()
        {
            instance = this;

            for (int i = 0; i < dataLevelOriginal.Length; i++)
            {
                dataLevelOriginal[i].ResetData();
            }

            for (int i = 0; i < dataLevelGoal.Length; i++)
            {
                dataLevelGoal[i].ResetToNotCorrect();

                if (dataLevelGoal[i].needFingerPrint) dataLevelGoal[i].typeQuestion = TypeQuestion.TextFirstThanImage;
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

        /// <summary>
        /// 分數總分計算
        /// </summary>
        /// <returns></returns>
        private int ScoreTotal()
        {
            int scoreTotalResult = 0;

            for (int i = 0; i < dataLevelGoal.Length; i++)
            {
                scoreTotalResult += dataLevelGoal[i].score;
                scoreTotalResult += dataLevelGoal[i].scoreFingerPrint;
            }

            return scoreTotalResult;
        }
    }
}
