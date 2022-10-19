using UnityEngine;
using System.Collections;

namespace Comibast
{
    /// <summary>
    /// ��q�t��
    /// </summary>
    public class EnemyHealth : HealthSystem
    {
        private EnemySystem enemySystem;
        private Material matDissolve;
        private string nameDissolve = "DissolveValue";    //���ѭȦW��"DissolveValue"�G�b���ѮĪG/���ѭȪ�Reference
        private float maxDissolve = 2.5f;                 //�n�M���ѭȪ��̰��ȬۦP
        private float minDisslve = -2.6f;

        private ObjectPoolGem objectPoolGem;

        public delegate void delegateDead();
        /// <summary>
        /// ���`�ƥ�
        /// </summary
        public delegateDead onDead;

        protected override void Awake()
        {
            base.Awake();
            enemySystem = GetComponent<EnemySystem>();

            //Renderer �� Skinned Mesh Render �P Mesh Renderer �������O
            //���o Renderer �i�H�A�Ω�ҫ��M�Τ��P���󪺪��p
            //GetComponentsInChildren ���o�l����̪�����A�Ǧ^�}�C
            matDissolve = GetComponentsInChildren<Renderer>()[0].material;

            //objectPoolGem = FindObjectOfType<ObjectPoolGem>();
            objectPoolGem = GameObject.Find("������H��").GetComponent<ObjectPoolGem>();
        }

        //�C������Q���îɰ���@��
        private void OnDisable()
        {
            
        }

        //�C������Q��ܮɰ���@��
        private void OnEnable()
        {
            hp = dataHealth.hp;
            imgHealth.fillAmount = 1;
            enemySystem.enabled = true;
            matDissolve.SetFloat(nameDissolve, 2.5f);    //�M���ѭȳ̤j�Ȥ@��
        }

        protected override void Dead()
        {
            base.Dead();
            enemySystem.enabled = false;
            DropProp();
            StartCoroutine(Dissolve());
        }

        /// <summary>
        /// ���ѮĪG
        /// </summary>
        private IEnumerator Dissolve()
        {
            while (maxDissolve > minDisslve)
            {
                maxDissolve -= 0.1f;
                matDissolve.SetFloat(nameDissolve, maxDissolve);
                yield return new WaitForSeconds(0.03f);
            }

            onDead();
        }


        /// <summary>
        /// �����D��
        /// </summary>
        private void DropProp()
        {
            float value = Random.value;
            if (value <= dataHealth.propProbability)
            {
                //Instantiate(
                //    dataHealth.goProp,
                //    transform.position + Vector3.up * 3,  //����
                //    Quaternion.Euler(0, 0, 0));          //����

                GameObject tempObject = objectPoolGem.GetPoolObject();
                tempObject.transform.position = transform.position + Vector3.up * 3;
            }
        }
    }

}

