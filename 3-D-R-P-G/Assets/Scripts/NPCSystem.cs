using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// �������a�O�_�i��ϰ줺�A��ܴ��ܵe���A���䰻���ñҰʹ�ܨt��
    /// </summary>
    public class NPCSystem : MonoBehaviour
    {
        [SerializeField, Header("NPC ��ܸ��")]
        private DataNPC dataNPC;
        [SerializeField, Header("NPC ��v��")]
        private GameObject goCamera;

        /// <summary>
        /// ���ܩ���
        /// </summary>
        private Animator aniTip;
        private string parTipFade = "Ĳ�o�H�J�H�X";
        private bool isInTrigger;
        private ThirdPersonController thirdPersonController;
        private DialogueSystem dialogueSystem;
        private Animator ani;
        private string parDialogue = "�}�����";

        private void Awake()
        {
            aniTip = GameObject.Find("���ܩ���").GetComponent<Animator>();

            //FindObjectOfType �j�M���� - �ȭ��u���@�Ӥ���ɨϥ�
            thirdPersonController = FindObjectOfType<ThirdPersonController>();
            dialogueSystem = FindObjectOfType<DialogueSystem>();
            ani = GetComponent<Animator>();
        }

        //�I���ƥ�
        //1. ��Ӫ���ܤ֨䤤�@�Ӧ� Rigidbody
        //2. ���Ŀ� Trigger �ϥ� OnTrigger �ƥ� Enter, Exit, Stay
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
        /// �ˬd���a�O�_�i�J�����}�A�ç�s�ʵe
        /// </summary>
        /// <param name="nameHit">�I�����󪺦W��</param>
        private void CheckPlayerAndAnimation(string nameHit, bool _isInTrigger)
        {
            if (nameHit == "���M�h")
            {
                isInTrigger = _isInTrigger;
                aniTip.SetTrigger(parTipFade);
            }
        }

        /// <summary>
        /// �ˬd�O�_��J���w����E�A�ö}�ҹ�ܡC
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
                    print("<color=#993311>�ʤ֤�����~�ANPC�S��Animator</color>");
                    //throw;
                }
                
                StartCoroutine(dialogueSystem.StartDialogue(dataNPC, ResetControllerAndCloseCamera));
            }
        }

        /// <summary>
        /// ���s�]�w����P������v��
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
                print("<color=#993311>�ʤ֤�����~�ANPC�S��Animator</color>");
                // throw;
            }

        }

    }

}

