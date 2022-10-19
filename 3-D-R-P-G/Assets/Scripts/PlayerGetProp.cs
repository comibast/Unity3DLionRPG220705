using TMPro;
using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// ���a���o�D��
    /// </summary>
    public class PlayerGetProp : MonoBehaviour
    {
        private ObjectPoolGem objectPoolGem;
        private string propGem = "Gem";
        private int countGem = 0;
        private int countGemMax = 3;
        private TextMeshProUGUI textCount;

        private NPCSystem npcSystem;
        [SerializeField, Header("�������Ȫ����")]
        private DataNPC dataNPC;

        private void Awake()
        {
            //objectPoolGem = FindObjectOfType<ObjectPoolGem>();
            objectPoolGem = GameObject.Find("������H��").GetComponent<ObjectPoolGem>();

            textCount = GameObject.Find("�H���ƶq����").GetComponent<TextMeshProUGUI>();
            npcSystem = GameObject.Find("�����H1019").GetComponent<NPCSystem>();
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
        /// ��s����
        /// </summary>
        private void UpdateUI()
        {
            textCount.text = "�]�� " + (++countGem) + " / " + countGemMax;

            if (countGem >= countGemMax) npcSystem.dataNPC = dataNPC;
        }
    }

}
