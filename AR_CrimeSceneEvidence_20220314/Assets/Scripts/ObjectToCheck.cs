using UnityEngine;

namespace KID
{
    /// <summary>
    /// �ݭn�Q�ˬd������
    /// </summary>
    public class ObjectToCheck : MonoBehaviour
    {
        [Header("�����ơG���d�ؼ�")]
        public DataObject dataGoal;
        [Header("�����ơG���d��l")]
        public DataObject dataOriginal;
        [Header("�n��ܪ�����")]
        public GameObject[] goFingerPrints;
        [Header("�S��")]
        public ParticleSystem psEffect;
        [Header("��Ҥ�")]
        public GameObject goScale;
    }
}
