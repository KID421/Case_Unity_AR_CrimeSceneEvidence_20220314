using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

namespace KID
{
    /// <summary>
    /// 開始畫面管理器
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        private string sceneToLoad = "選取關卡";
        private Transform traLoadingCircle;
        private float angleToRotate = 90;
        private CanvasGroup groupLoading;
        private CanvasGroup groupBtnStart;
        private CanvasGroup groupLogin;
        private Button btnStart;
        private Button btnConfirm;
        private int countToRotate = 600;
        private WaitForSeconds secondToShowStart = new WaitForSeconds(0.5f);
        private WaitForSeconds secondToStart = new WaitForSeconds(1f);
        private TMP_InputField inputFieldID;

        private void Awake()
        {

            traLoadingCircle = GameObject.Find("載入遊戲圓圈").transform;
            groupLoading = GameObject.Find("載入遊戲群組").GetComponent<CanvasGroup>();
            groupBtnStart = GameObject.Find("按鈕開始遊戲").GetComponent<CanvasGroup>();
            groupLogin = GameObject.Find("登入群組").GetComponent<CanvasGroup>();
            btnStart = groupBtnStart.GetComponent<Button>();
            btnConfirm = GameObject.Find("按鈕確定").GetComponent<Button>();
            inputFieldID = GameObject.Find("輸入欄位 ID").GetComponent<TMP_InputField>();
            inputFieldID.onEndEdit.AddListener(input => PlayerPrefs.SetString("id", input));
        }

        private void Start()
        {
            StartCoroutine(Loading());
        }

        /// <summary>
        /// 載入
        /// </summary>
        private IEnumerator Loading()
        {
            for (int i = 0; i < countToRotate; i++)
            {
                traLoadingCircle.Rotate(0, 0, angleToRotate * Time.deltaTime);
                yield return null;
            }

            FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupLoading, false);
            yield return secondToShowStart;
            FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupBtnStart);

            btnStart.onClick.AddListener(() => StartCoroutine(ClickStart()));
        }

        /// <summary>
        /// 點擊開始
        /// </summary>
        private IEnumerator ClickStart()
        {
            FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupBtnStart, false);
            yield return secondToStart;
            FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupLogin);

            btnConfirm.onClick.AddListener(() => SceneManager.LoadScene(sceneToLoad));
        }
    }
}
