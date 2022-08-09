using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// 遊戲管理器：暫停、返回
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField, Header("暫停黑幕")]
        private GameObject goPauseBlack;

        /// <summary>
        /// 暫停
        /// </summary>
        private Button btnPause;
        /// <summary>
        /// 返回
        /// </summary>
        private Button btnBack;

        private string nameBackScene = "選取關卡";

        private void Awake()
        {
            btnPause = GameObject.Find("暫停").GetComponent<Button>();
            btnBack = GameObject.Find("返回").GetComponent<Button>();

            btnPause.onClick.AddListener(Pause);
            btnBack.onClick.AddListener(BackToMenu);
        }

        /// <summary>
        /// 暫停
        /// </summary>
        private void Pause()
        {
            goPauseBlack.SetActive(!goPauseBlack.activeInHierarchy);
            ManagerTime.instance.isTimeStop = !ManagerTime.instance.isTimeStop;
        }

        /// <summary>
        /// 返回
        /// </summary>
        private void BackToMenu()
        {
            SceneManager.LoadScene(nameBackScene);
        }
    }
}
