using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

namespace KID
{
    [DefaultExecutionOrder(200)]
    /// <summary>
    /// Google App Script �޲z��
    /// �x�s��Ʃ� GAS ��
    /// </summary>
    public class GoogleAppsScriptManager : MonoBehaviour
    {
        private string gasURL = "https://script.google.com/macros/s/AKfycbzW2miVL3nlZHRC9L3lgVyYK8tX3ldsmyLJDrG-ssXwWE4czqQwpgOx1prxI8dOS2a2/exec";
        private string stringMethod = "method";
        private string stringSave = "�x�s�Ǹo�{�����Ҹ��";
        private DataLevelInteracte dataLevel;

        public static GoogleAppsScriptManager instance;

        private void Awake()
        {
            instance = this;
            dataLevel = DataLevelInteracte.instance;
        }
        
        /// <summary>
        /// �}�l�]�w GAS ���
        /// </summary>
        public void StartSetGAS()
        {
            StartCoroutine(StartSetSheetData());
        }

        /// <summary>
        /// �}�l�]�w�x�s���
        /// </summary>
        private IEnumerator StartSetSheetData()
        {
            WWWForm form = new WWWForm();
            form.AddField(stringMethod, stringSave);
            form.AddField("scene", SceneManager.GetActiveScene().name);
            form.AddField("id", PlayerPrefs.GetString("id"));
            form.AddField("date", DateTime.Now.ToString());
            form.AddField("time", ManagerTime.instance.timerCount.ToString("F0"));
            form.AddField("score", DataLevelInteracte.instance.scoreTotal);

            for (int i = 0; i < dataLevel.dataLevelGoal.Length; i++)
            {
                string nameTitle = dataLevel.dataLevelGoal[i].name;
                string nameScore = dataLevel.dataLevelGoal[i].score.ToString();
                string nameUseDataPage = dataLevel.dataLevelGoal[i].useDataPage ? "���ϥ�" : "�S�ϥ�";
            
                form.AddField("title" + i, nameTitle);
                form.AddField("score" + i, nameScore);
                form.AddField("use" + i, nameUseDataPage);
            }

            using (UnityWebRequest www = UnityWebRequest.Post(gasURL, form))
            {
                yield return www.SendWebRequest();

                print(www.downloadHandler.text);
            }
        }
    }
}
