using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// �C���޲z���G�Ȱ��B��^
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField, Header("�Ȱ��¹�")]
        private GameObject goPauseBlack;

        /// <summary>
        /// �Ȱ�
        /// </summary>
        private Button btnPause;
        /// <summary>
        /// ��^
        /// </summary>
        private Button btnBack;

        private string nameBackScene = "������d";

        private void Awake()
        {
            btnPause = GameObject.Find("�Ȱ�").GetComponent<Button>();
            btnBack = GameObject.Find("��^").GetComponent<Button>();

            btnPause.onClick.AddListener(Pause);
            btnBack.onClick.AddListener(BackToMenu);
        }

        /// <summary>
        /// �Ȱ�
        /// </summary>
        private void Pause()
        {
            goPauseBlack.SetActive(!goPauseBlack.activeInHierarchy);
            ManagerTime.instance.isTimeStop = !ManagerTime.instance.isTimeStop;
        }

        /// <summary>
        /// ��^
        /// </summary>
        private void BackToMenu()
        {
            SceneManager.LoadScene(nameBackScene);
        }
    }
}
