using UnityEngine;
using UnityEngine.UI;

namespace Comibast
{
    /// <summary>
    /// 血量系統
    /// </summary>
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField, Header("血量資料")]
        protected DataHealth dataHealth;
        [SerializeField, Header("血條")]
        private Image imgHealth;

        private float hp;
        private Animator ani;
        private string parHurt = "觸發受傷";
        private string parDead = "開關死亡";
        private AttackSystem attackSystem;

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            attackSystem = GetComponent<AttackSystem>();
            hp = dataHealth.hp;
        }

        /// <summary>
        /// 受傷
        /// </summary>
        /// <param name="damage">收到的傷害值</param>
        public void Hurt(float damage)
        {
            hp -= damage;
            ani.SetTrigger(parHurt);
            
            if (hp <= 0) Dead();

            imgHealth.fillAmount = hp / dataHealth.hpMax;
        }

        /// <summary>
        /// 死亡
        /// </summary>
        protected virtual void Dead()
        {
            hp = 0;
            ani.SetBool(parDead, true);
            attackSystem.enabled = false;
        }
    }
}
