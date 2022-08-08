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
 
        [SerializeField, Header("�T����")]
        private GameObject goTriangle;

        #endregion

        [SerializeField, Header("�H�J���j")]
        private float intervalFadeIn = 0.1f;
        [SerializeField, Header("���r���j")]
        private float intervalType = 0.05f;

        public DataNPC dataNpc;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            StartCoroutine(StartDialogue());
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
        /// �}�l���
        /// </summary>
        public IEnumerator StartDialogue()
        {
            textName.text = dataNpc.nameNPC;
            textContent.text = "";                     //���M�Ź��

            yield return StartCoroutine(Fade());

            for (int i = 0; i < dataNpc.dataDialogue.Length; i++)
            {
                yield return StartCoroutine(TypeEffect(i));

                //�p�G�٨S���A�N���򵥫�
                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }
            }
            StartCoroutine(Fade(false));
        }


        /// <summary>
        /// �H�J�βH�X�ĪG
        /// </summary>
        private IEnumerator Fade(bool fadeIn = true)
        {
            // �T���B��l
            // ���L�� ? ���L�Ȭ�true : ���L�Ȭ� false
            float increase = fadeIn ? 0.1f : -0.1f;
            
            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += increase;
                yield return new WaitForSeconds(intervalFadeIn);
            }  
        }

        /// <summary>
        /// ���r�ĪG�A�����ܭ��ĻP��ܤT����
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
