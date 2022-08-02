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
