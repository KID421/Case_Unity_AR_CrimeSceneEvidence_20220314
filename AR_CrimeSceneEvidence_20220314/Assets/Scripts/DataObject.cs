using UnityEngine;

namespace KID
{
    /// <summary>
    /// ������
    /// </summary>
    [CreateAssetMenu(menuName = "KID/Data Object", fileName = "Data Object")]
    public class DataObject : ScriptableObject
    {
        //[Header("��������"), EnumFlagsAttribute]
        //public TypeObjectToCheck type;

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
