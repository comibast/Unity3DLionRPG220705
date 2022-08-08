using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// 偵測玩家是否進到區域內，顯示提示畫面，按鍵偵測並啟動對話系統
    /// </summary>
    public class NPCSystem : MonoBehaviour
    {
        [SerializeField, Header("NPC 對話資料")]
        private DataNPC dataNPC;
        [SerializeField, Header("NPC 攝影機")]
        private GameObject goCamera;

        /// <summary>
        /// 提示底圖
        /// </summary>
        private Animator aniTip;
        private string parTipFade = "觸發淡入淡出";
        private bool isInTrigger;
        private ThirdPersonController thirdPersonController;
        private DialogueSystem dialogueSystem;
        private Animator ani;
        private string parDialogue = "開關對話";

        private void Awake()
        {
            aniTip = GameObject.Find("提示底圖").GetComponent<Animator>();

            //FindObjectOfType 搜尋元件 - 僅限只有一個元件時使用
            thirdPersonController = FindObjectOfType<ThirdPersonController>();
            dialogueSystem = FindObjectOfType<DialogueSystem>();
            ani = GetComponent<Animator>();
        }

        //碰撞事件
        //1. 兩個物件至少其中一個有 Rigidbody
        //2. 有勾選 Trigger 使用 OnTrigger 事件 Enter, Exit, Stay
        private void OnTriggerEnter(Collider other)
        {
            CheckPlayerAndAnimation(other.name, true);
        }

        private void OnTriggerExit(Collider other)
        {
            CheckPlayerAndAnimation(other.name, false);
        }

        private void Update()
        {
            InputKeyAndStartDialogue();
        }

        /// <summary>
        /// 檢查玩家是否進入或離開，並更新動畫
        /// </summary>
        /// <param name="nameHit">碰撞物件的名稱</param>
        private void CheckPlayerAndAnimation(string nameHit, bool _isInTrigger)
        {
            if (nameHit == "狗騎士")
            {
                isInTrigger = _isInTrigger;
                aniTip.SetTrigger(parTipFade);
            }
        }

        /// <summary>
        /// 檢查是否輸入指定按鍵E，並開啟對話。
        /// </summary>
        private void InputKeyAndStartDialogue()
        {
            if (dialogueSystem.isDialogue) return;

            if (isInTrigger && Input.GetKeyDown(KeyCode.E))
            {
                goCamera.SetActive(true);
                aniTip.SetTrigger(parTipFade);
                thirdPersonController.enabled = false;
                
                try
                {
                    ani.SetBool(parDialogue, true);
                }
                catch (System.Exception)
                {
                    print("<color=#993311>缺少元件錯誤，NPC沒有Animator</color>");
                    //throw;
                }
                
                StartCoroutine(dialogueSystem.StartDialogue(dataNPC, ResetControllerAndCloseCamera));
            }
        }

        /// <summary>
        /// 重新設定控制器與關閉攝影機
        /// </summary>
        private void ResetControllerAndCloseCamera()
        {
            goCamera.SetActive(false);
            thirdPersonController.enabled = true;
            aniTip.SetTrigger(parTipFade);

            try
            {
                ani.SetBool(parDialogue, false);
            }
            catch (System.Exception)
            {
                print("<color=#993311>缺少元件錯誤，NPC沒有Animator</color>");
                // throw;
            }

        }

    }

}

