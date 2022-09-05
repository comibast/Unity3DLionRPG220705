using UnityEngine;
using System.Collections;

namespace Comibast
{
    /// <summary>
    /// 攻擊基底系統
    /// </summary>
    public class AttackSystem : MonoBehaviour
    {
        [SerializeField, Header("攻擊資料")]
        private DataAttack dataAttack;

        protected bool canAttack = true;

        //敵人攻擊系統：添加立方體圖示角度旋轉
        private void OnDrawGizmos()
        {
            Gizmos.color = dataAttack.attackAreaColor;

            Gizmos.matrix = Matrix4x4.TRS(
                transform.position + 
                transform.TransformDirection(dataAttack.attackAreaOffset),
                transform.rotation, transform.localScale);

            Gizmos.DrawCube(
                Vector3.zero,
                dataAttack.attackAreaSize);
        }

        /// <summary>
        /// 開始攻擊
        /// </summary>
        public void StartAttack()
        {
            if (!canAttack) return;
            StartCoroutine(AttackFlow());
        }

        /// <summary>
        /// 攻擊流程
        /// </summary>
        private IEnumerator AttackFlow()
        {
            canAttack = false;
            yield return new WaitForSeconds(dataAttack.delayAttack);
            CheckAttackArea();

            yield return new WaitForSeconds(dataAttack.waitAttackEnd);
            canAttack = true;
            StopAttack();
        }

        /// <summary>
        /// 停止攻擊：保護，虛擬：允許子類別複寫
        /// </summary>
        protected virtual void StopAttack()
        { }

        /// <summary>
        /// 檢查攻擊區域是否碰撞到目標圖層
        /// </summary>
        private void CheckAttackArea()
        {
            Collider[] hits = Physics.OverlapBox(
                transform.position +
                transform.TransformDirection(dataAttack.attackAreaOffset),
                dataAttack.attackAreaSize / 2,
                transform.rotation, dataAttack.layerTarget);

            if (hits.Length > 0)
            {
                print(hits[0].name);
            }
        }

    }

}

