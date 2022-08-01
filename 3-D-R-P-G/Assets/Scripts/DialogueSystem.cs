using UnityEngine;
using TMPro;
using System.Collections;    //�ޥ� �t�� ���X - ��Ƶ��c�P��P�{��

namespace Comibast
{
    /// <summary>
    /// ��ܨt�ΡA�H�J��ܮءA��sNPC��ƦW�١A���e�A���ġA�H�X
    /// </summary>
    /// RequireComponent �n�D����A�b�K�[�}����Ĳ�o
    [RequireComponent(typeof(AudioSource))]
    public class DialogueSystem : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("�e����ܨt��")]
        private CanvasGroup groupDialogue;
        [SerializeField, Header("���ܪ̦W�r")]
        private TextMeshProUGUI textName;
        [SerializeField, Header("��ܤ��e")]
        private TextMeshProUGUI textContent;

        private AudioSource aud;
        #endregion

        [SerializeField, Header("�T����")]
        private GameObject goTriangle;

        public DataNPC dataNPC;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();
            StartCoroutine(FadeIn());

            textName.text = dataNPC.nameNPC;
            textContent.text = "";                     //���M�Ź��
        }

        #region ��P�{�Ǳо�
        //��P�{�ǻݭn��
        //1. �ޥ�System.Collections
        //2. �w�q��k �öǦ^ IEnumerator
        //3. �Ұʨ�{ StartCoroutine

        private IEnumerator Test()
        {
            print("�Ĥ@���r");
            yield return new WaitForSeconds(2);
            print("�ĤG���r");
            yield return new WaitForSeconds(5);
            print("�ĤT���r");
        }
        #endregion

        /// <summary>
        /// �H�J�ĪG
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
        /// ���r�ĪG
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
