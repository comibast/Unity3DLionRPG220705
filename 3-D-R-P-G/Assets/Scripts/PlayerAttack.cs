using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// 玩家攻擊：透過輸入方式控制動畫與攻擊判定
    /// </summary>
    public class PlayerAttack : AttackSystem
    {
        private Animator ani;
        private ThirdPersonController tpc;

        private string parAttack = "觸發攻擊";

        private void Awake()
        {
            ani = GetComponent<Animator>();
            tpc = GetComponent<ThirdPersonController>();
        }

        private void Update()
        {
            AttackInput();
        }

        /// <summary>
        /// 攻擊輸入判定
        /// </summary>
        private void AttackInput()
        {
            if (!canAttack) return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                tpc.enabled = false;
                ani.SetTrigger(parAttack);
                StartAttack();
            }
        }

        protected override void StopAttack()
        {
            tpc.enabled = true;
        }
    }
}

