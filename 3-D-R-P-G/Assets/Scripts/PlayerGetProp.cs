using TMPro;
using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public class PlayerGetProp : MonoBehaviour
    {
        private ObjectPoolGem objectPoolGem;
        private string propGem = "Gem";
        private int countGem = 0;
        private int countGemMax = 3;
        private TextMeshProUGUI textCount;

        private NPCSystem npcSystem;
        [SerializeField, Header("完成任務的對話")]
        private DataNPC dataNPC;

        private void Awake()
        {
            //objectPoolGem = FindObjectOfType<ObjectPoolGem>();
            objectPoolGem = GameObject.Find("物件池碎片").GetComponent<ObjectPoolGem>();

            textCount = GameObject.Find("碎片數量介面").GetComponent<TextMeshProUGUI>();
            npcSystem = GameObject.Find("機器人1019").GetComponent<NPCSystem>();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.name.Contains(propGem))
            {
                objectPoolGem.ReleasePoolObject(hit.gameObject);
                UpdateUI();
            }
        }

        /// <summary>
        /// 更新介面
        /// </summary>
        private void UpdateUI()
        {
            textCount.text = "魔晶 " + (++countGem) + " / " + countGemMax;

            if (countGem >= countGemMax) npcSystem.dataNPC = dataNPC;
        }
    }

}
