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
 
        [SerializeField, Header("三角形")]
        private GameObject goTriangle;

        #endregion

        [SerializeField, Header("淡入間隔")]
        private float intervalFadeIn = 0.1f;
        [SerializeField, Header("打字間隔")]
        private float intervalType = 0.05f;

        public DataNPC dataNpc;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            StartCoroutine(StartDialogue());
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
        /// 開始對話
        /// </summary>
        public IEnumerator StartDialogue()
        {
            textName.text = dataNpc.nameNPC;
            textContent.text = "";                     //先清空對話

            yield return StartCoroutine(Fade());

            for (int i = 0; i < dataNpc.dataDialogue.Length; i++)
            {
                yield return StartCoroutine(TypeEffect(i));

                //如果還沒按，就持續等待
                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }
            }
            StartCoroutine(Fade(false));
        }


        /// <summary>
        /// 淡入或淡出效果
        /// </summary>
        private IEnumerator Fade(bool fadeIn = true)
        {
            // 三元運算子
            // 布林值 ? 布林值為true : 布林值為 false
            float increase = fadeIn ? 0.1f : -0.1f;
            
            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += increase;
                yield return new WaitForSeconds(intervalFadeIn);
            }  
        }

        /// <summary>
        /// 打字效果，播放對話音效與顯示三角形
        /// </summary>
        private IEnumerator TypeEffect(int indexDialogue)
        {
            textContent.text = "";
            aud.PlayOneShot(dataNpc.dataDialogue[indexDialogue].sound);

            string content = dataNpc.dataDialogue[indexDialogue].content;

            for (int i = 0; i < content.Length; i++)
            {
                textContent.text += content[i];
                yield return new WaitForSeconds(intervalType);
            }

            goTriangle.SetActive(true);
        }
    }

}
