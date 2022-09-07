using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// ��q�t��
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
        /// �����D��
        /// </summary>
        private void DropProp()
        {
            float value = Random.value;
            if (value <= dataHealth.propProbability)
            {
                Instantiate(
                    dataHealth.goProp,
                    transform.position + Vector3.up * 3,  //����
                    Quaternion.Euler(90, 0, 0));          //����
            }
        }
    }

}

