using UnityEngine;

namespace KID
{
    /// <summary>
    /// 資料頁面內容
    /// </summary>
    [CreateAssetMenu(menuName = "KID/Data Page Content", fileName = "Data Page Content")]
    public class DataPageContent : ScriptableObject
    {
        [Header("資料頁內容")]
        public DataPageContentInformation[] dataPageContents;
    }

    [System.Serializable]
    public class DataPageContentInformation
    {
        [Header("資料頁內容圖片")]
        public Sprite sprPicture;
        [Header("資料頁內容文字描述"), TextArea(2, 5)]
        public string stringDescription;
    }
}

