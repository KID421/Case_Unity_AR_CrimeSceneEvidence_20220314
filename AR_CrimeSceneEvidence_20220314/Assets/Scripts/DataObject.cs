using UnityEngine;

namespace KID
{
    /// <summary>
    /// ������
    /// </summary>
    [CreateAssetMenu(menuName = "KID/Data Object", fileName = "Data Object")]
    public class DataObject : ScriptableObject
    {
        [Header("�Ҫ��W��")]
        public string nameEvidenvce;
        [Header("�Ҫ��Ϥ�")]
        public Sprite sprImage;
        [Header("�Ҫ������Ϥ�")]
        public Sprite sprImageFingerPrint;
        [Header("�O�_�ݭn���")]
        public bool needCamera;
        [Header("�O�_�ݭn���J�Ҫ��U")]
        public bool needEvidenceBag;
        [Header("�O�_�ݭn���q�ؤo")]
        public bool needScale;
        [Header("�O�_�ݭn�Ķ� DNA")]
        public bool needDNA;
        [Header("�O�_�ݭn�Ķ�����")]
        public bool needFingerPrint;
        [Header("�O�_�ݭn��q��")]
        public bool needFlashLight;

        [Header("�D�ػP�ﶵ")]
        public TypeQuestion typeQuestion;
        public string stringQuestion;
        public string textOption1;
        public string textOption2;
        public string textOption3;
        public Sprite imgOption1;
        public Sprite imgOption2;
        public Sprite imgOption3;
        [Header("���T����"), Range(1, 3)]
        public int indexAnswer;
        [Header("������勵�T����"), Range(1, 3)]
        public int indexAnswerFingerPrint;

        /// <summary>
        /// �O�_����
        /// </summary>
        public bool isCorrect;
        /// <summary>
        /// �O�_����������
        /// </summary>
        public bool isCorrectFingerPrint;
        /// <summary>
        /// �O�_��L���D��
        /// </summary>
        public bool isChoose;
        /// <summary>
        /// ������ת�����
        /// </summary>
        public int countChooseAnswer;
        /// <summary>
        /// �O�_�ϥθ�ƭ�
        /// </summary>
        public bool useDataPage;
        /// <summary>
        /// ������ת����� - �������
        /// </summary>
        public int countChooseAnswerFingerPrint;

        /// <summary>
        /// �`��
        /// </summary>
        private int scoreTotal = 10;
        /// <summary>
        /// �C�����~����
        /// </summary>
        private int scorePerWrong = 5;

        /// <summary>
        /// �o��
        /// </summary>
        public int score => countChooseAnswer == 0 ? 0 : scoreTotal - countChooseAnswer * scorePerWrong;
        /// <summary>
        /// ����������
        /// </summary>
        public int scoreFingerPrint => needFingerPrint ? scoreTotal - countChooseAnswerFingerPrint * scorePerWrong : 0;

        /// <summary>
        /// ���]���
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
        ///���s�w���W�����T���A�G�ؼи�ƥ�
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
