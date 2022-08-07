using UnityEngine;

namespace KID
{
    /// <summary>
    /// ��ƭ������e
    /// </summary>
    [CreateAssetMenu(menuName = "KID/Data Page Content", fileName = "Data Page Content")]
    public class DataPageContent : ScriptableObject
    {
        [Header("��ƭ����e")]
        public DataPageContentInformation[] dataPageContents;
    }

    [System.Serializable]
    public class DataPageContentInformation
    {
        [Header("��ƭ����e�Ϥ�")]
        public Sprite sprPicture;
        [Header("��ƭ����e��r�y�z"), TextArea(2, 5)]
        public string stringDescription;
    }
}

