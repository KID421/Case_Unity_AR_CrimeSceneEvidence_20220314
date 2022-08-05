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
    }
}
