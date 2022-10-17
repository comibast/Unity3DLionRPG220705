using UnityEngine;
using System.Collections;

namespace Comibast
{
    /// <summary>
    /// 血量系統
    /// </summary>
    public class EnemyHealth : HealthSystem
    {
        private EnemySystem enemySystem;
        private Material matDissolve;
        private string nameDissolve = "DissolveValue";    //溶解值名稱"DissolveValue"：在溶解效果/溶解值的Reference
        private float maxDissolve = 2.5f;                 //要和溶解值的最高值相同
        private float minDisslve = -2.6f;

        private ObjectPoolGem objectPoolGem;

        protected override void Awake()
        {
            base.Awake();
            enemySystem = GetComponent<EnemySystem>();
            matDissolve = GetComponentsInChildren<Renderer>()[0].material;

            objectPoolGem = FindObjectOfType<ObjectPoolGem>();
        }

        protected override void Dead()
        {
            base.Dead();
            enemySystem.enabled = false;
            DropProp();
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()
        {
            while (maxDissolve > minDisslve)
            {
                maxDissolve -= 0.1f;
                matDissolve.SetFloat(nameDissolve, maxDissolve);
                yield return new WaitForSeconds(0.03f);
            }
        }


        /// <summary>
        /// 掉落道具
        /// </summary>
        private void DropProp()
        {
            float value = Random.value;
            if (value <= dataHealth.propProbability)
            {
                //Instantiate(
                //    dataHealth.goProp,
                //    transform.position + Vector3.up * 3,  //高度
                //    Quaternion.Euler(0, 0, 0));          //角度

                GameObject tempObject = objectPoolGem.GetPoolObject();
                tempObject.transform.position = transform.position + Vector3.up * 3;
            }
        }
    }

}

