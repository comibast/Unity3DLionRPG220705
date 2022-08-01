using UnityEngine;
using TMPro;
using System.Collections;    //引用 系統 集合 - 資料結構與協同程序

namespace Comibast
{
    /// <summary>
    /// 對話系統，淡入對話框，更新NPC資料名稱，內容，音效，淡出
    /// </summary>
    /// RequireComponent 要求元件，在添加腳本時觸發
    [RequireComponent(typeof(AudioSource))]
    public class DialogueSystem : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("畫布對話系統")]
        private CanvasGroup groupDialogue;
        [SerializeField, Header("說話者名字")]
        private TextMeshProUGUI textName;
        [SerializeField, Header("對話內容")]
        private TextMeshProUGUI textContent;

        private AudioSource aud;
        #endregion

        [SerializeField, Header("三角形")]
        private GameObject goTriangle;

        public DataNPC dataNPC;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();
            StartCoroutine(FadeIn());

            textName.text = dataNPC.nameNPC;
            textContent.text = "";                     //先清空對話
        }

        #region 協同程序教學
        //協同程序需要的
        //1. 引用System.Collections
        //2. 定義方法 並傳回 IEnumerator
        //3. 啟動協程 StartCoroutine

        private IEnumerator Test()
        {
            print("第一行文字");
            yield return new WaitForSeconds(2);
            print("第二行文字");
            yield return new WaitForSeconds(5);
            print("第三行文字");
        }
        #endregion

        /// <summary>
        /// 淡入效果
        /// </summary>
        private IEnumerator FadeIn()
        {
            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            StartCoroutine(TypeEffect());
        }

        /// <summary>
        /// 打字效果
        /// </summary>
        private IEnumerator TypeEffect()
        {
            aud.PlayOneShot(dataNPC.dataDialogue[0].sound);

            string content = dataNPC.dataDialogue[0].content;

            for (int i = 0; i < content.Length; i++)
            {
                textContent.text += content[i];
                yield return new WaitForSeconds(0.05f);
            }

            goTriangle.SetActive(true);
        }
    }

}
