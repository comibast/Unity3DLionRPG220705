using UnityEngine;
using UnityEngine.UI;

namespace Comibast
{
    /// <summary>
    /// ��q�t��
    /// </summary>
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField, Header("��q���")]
        protected DataHealth dataHealth;
        [SerializeField, Header("���")]
        private Image imgHealth;

        private float hp;
        private Animator ani;
        private string parHurt = "Ĳ�o����";
        private string parDead = "�}�����`";
        private AttackSystem attackSystem;

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            attackSystem = GetComponent<AttackSystem>();
            hp = dataHealth.hp;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="damage">���쪺�ˮ`��</param>
        public void Hurt(float damage)
        {
            hp -= damage;
            ani.SetTrigger(parHurt);
            
            if (hp <= 0) Dead();

            imgHealth.fillAmount = hp / dataHealth.hpMax;
        }

        /// <summary>
        /// ���`
        /// </summary>
        protected virtual void Dead()
        {
            hp = 0;
            ani.SetBool(parDead, true);
            attackSystem.enabled = false;
        }
    }
}
