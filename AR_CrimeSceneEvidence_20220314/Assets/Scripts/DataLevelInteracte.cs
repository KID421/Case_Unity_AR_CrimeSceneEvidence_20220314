using UnityEngine;

namespace KID
{
    /// <summary>
    /// ���d���ʸ��
    /// </summary>
    public class DataLevelInteracte : MonoBehaviour
    {
        [Header("�n�Q�������Ҧ�����")]
        public ObjectToCheck[] objectsToCheck;

        /// <summary>
        /// ���d�n�������ؼ�
        /// </summary>
        public DataObject[] dataLevelGoal;
        /// <summary>
        /// ���d����l������
        /// </summary>
        public DataObject[] dataLevelOriginal;

        public static DataLevelInteracte instance;

        /// <summary>
        /// �`��
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
        /// �����`���p��
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
