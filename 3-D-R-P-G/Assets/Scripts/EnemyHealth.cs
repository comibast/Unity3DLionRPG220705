using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// 血量系統
    /// </summary>
    public class EnemyHealth : HealthSystem
    {
        private EnemySystem enemySystem;
        protected override void Awake()
        {
            base.Awake();
            enemySystem = GetComponent<EnemySystem>();
        }

        protected override void Dead()
        {
            base.Dead();
            enemySystem.enabled = false;
            DropProp();
        }

        /// <summary>
        /// 掉落道具
        /// </summary>
        private void DropProp()
        {
            float value = Random.value;
            if (value <= dataHealth.propProbability)
            {
                Instantiate(
                    dataHealth.goProp,
                    transform.position + Vector3.up * 3,  //高度
                    Quaternion.Euler(90, 0, 0));          //角度
            }
        }
    }

}

