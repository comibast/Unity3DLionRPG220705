using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// ���a�����G�z�L��J�覡����ʵe�P�����P�w
    /// </summary>
    public class PlayerAttack : AttackSystem
    {
        
        private ThirdPersonController tpc;

        private string parAttack = "Ĳ�o����";

        protected override void Awake()
        {
            base.Awake();
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

