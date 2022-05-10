using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace KID
{
    /// <summary>
    /// �޲z���G����a��
    /// </summary>
    public class ManagerSelectionMap : MonoBehaviour
    {
        [SerializeField, Header("���d���s")]
        private Button[] btnMaps;
        private RectTransform[] rectMaps;
        /// <summary>
        /// ����ϥ�
        /// </summary>
        private RectTransform rectSelect;
        /// <summary>
        /// �������D
        /// </summary>
        private Text textMapSelect;
        private string[] nameMaps =
        {
            "���d 1 �ѵs��", "���d 2", "���d 3"
        };

        private void Awake()
        {
            rectSelect = GameObject.Find("����ϥ�").GetComponent<RectTransform>();
            textMapSelect = GameObject.Find("�������D").GetComponent<Text>();

            rectMaps = new RectTransform[btnMaps.Length];

            for (int i = 0; i < btnMaps.Length; i++)
            {
                rectMaps[i] = btnMaps[i].GetComponent<RectTransform>();
            }

            SelectMap();
        }

        /// <summary>
        /// ������d
        /// </summary>
        private void SelectMap()
        {
            for (int i = 0; i < btnMaps.Length; i++)
            {
                int index = i;
                btnMaps[i].onClick.AddListener(() =>
                {
                    rectSelect.anchoredPosition = rectMaps[index].anchoredPosition + Vector2.up * 80;
                    textMapSelect.text = nameMaps[index];
                    SceneManager.LoadScene(nameMaps[index]);
                });
            }
        }
    }
}
