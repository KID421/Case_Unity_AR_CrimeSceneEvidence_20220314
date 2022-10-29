using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace KID
{
    /// <summary>
    /// �޲z���G����a��
    /// </summary>
    public class ManagerSelectionMap : MonoBehaviour
    {
        [SerializeField, Header("���d���s")]
        private Button[] btnMaps;
        [SerializeField, Header("���d�y������")]
        private AudioClip[] soundDescriptions;

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
            "��v�ѵs��", "�����ƥ�", "�S���ѵs��"
        };
        private string[] descriptionMaps =
        {
            "�����ͩ~��b�p���J�A�U�Z��a��A�o�{��a�æ��D�H�I�J�A���]���l���A��ĵ�B�z�C",
            "���p�j�~��b���J�p�M�СA���@��S���ӤW�Z�A�P�Ƥ]�s�����W�A��O��J�٧�o�A�o�{�˪צa�W�A�w�g�S���߸�...",
            "�Y�j��1�ӵo�ͤ@�_��v�ѵs�סA�ڳQ�`�ΥD���z�@�������Ʊ��B�]�_�@�嵥���D�ѡA�]���l���ʦ��F�W�ʸU��..."
        };
        private AudioSource aud;
        private CanvasGroup groupLevelDescription;
        private Button btnSelectConfirm;
        private Button btnSelectCancel;
        private TextMeshProUGUI textDescription;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            rectSelect = GameObject.Find("����ϥ�").GetComponent<RectTransform>();
            textMapSelect = GameObject.Find("�������D").GetComponent<Text>();

            groupLevelDescription = GameObject.Find("���d���иs��").GetComponent<CanvasGroup>();
            btnSelectConfirm = GameObject.Find("���s���d�T�w").GetComponent<Button>();
            btnSelectCancel = GameObject.Find("���s���d����").GetComponent<Button>();
            textDescription = GameObject.Find("���d���Ф�r").GetComponent<TextMeshProUGUI>();

            btnSelectCancel.onClick.AddListener(() =>
            {
                btnSelectConfirm.onClick.RemoveAllListeners();
                aud.Stop();
                FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupLevelDescription, false);
            });

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
                    FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupLevelDescription);
                    aud.PlayOneShot(soundDescriptions[index]);
                    textDescription.text = descriptionMaps[index];

                    btnSelectConfirm.onClick.AddListener(() =>
                    {
                        btnSelectConfirm.interactable = false;
                        SceneManager.LoadScene(nameMaps[index]);
                    });
                });
            }
        }
    }
}
