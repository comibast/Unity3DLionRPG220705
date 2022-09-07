using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// ¦å¶q¨t²Î
    /// </summary>
    public class PlayerHealth : HealthSystem
    {
        private ThirdPersonController tpc;

        protected override void Awake()
        {
            base.Awake();

            tpc = GetComponent<ThirdPersonController>();
        }

        protected override void Dead()
        {
            base.Dead();
            tpc.enabled = false;
        }
    }

}

