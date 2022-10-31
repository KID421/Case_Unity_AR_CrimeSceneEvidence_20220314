using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

namespace KID
{
    /// <summary>
    /// �}�l�e���޲z��
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        private string sceneToLoad = "������d";
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

            traLoadingCircle = GameObject.Find("���J�C�����").transform;
            groupLoading = GameObject.Find("���J�C���s��").GetComponent<CanvasGroup>();
            groupBtnStart = GameObject.Find("���s�}�l�C��").GetComponent<CanvasGroup>();
            groupLogin = GameObject.Find("�n�J�s��").GetComponent<CanvasGroup>();
            btnStart = groupBtnStart.GetComponent<Button>();
            btnConfirm = GameObject.Find("���s�T�w").GetComponent<Button>();
            inputFieldID = GameObject.Find("��J��� ID").GetComponent<TMP_InputField>();
            inputFieldID.onEndEdit.AddListener(input => PlayerPrefs.SetString("id", input));
        }

        private void Start()
        {
            StartCoroutine(Loading());
        }

        /// <summary>
        /// ���J
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
        /// �I���}�l
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
