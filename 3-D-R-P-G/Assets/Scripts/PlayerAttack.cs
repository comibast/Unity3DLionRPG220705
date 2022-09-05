using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// ���a�����G�z�L��J�覡����ʵe�P�����P�w
    /// </summary>
    public class PlayerAttack : AttackSystem
    {
        private Animator ani;
        private ThirdPersonController tpc;

        private string parAttack = "Ĳ�o����";

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
        /// ������J�P�w
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

