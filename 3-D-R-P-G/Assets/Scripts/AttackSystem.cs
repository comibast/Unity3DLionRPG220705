using UnityEngine;
using System.Collections;

namespace Comibast
{
    /// <summary>
    /// �����򩳨t��
    /// </summary>
    public class AttackSystem : MonoBehaviour
    {
        [SerializeField, Header("�������")]
        private DataAttack dataAttack;

        protected bool canAttack = true;

        //�ĤH�����t�ΡG�K�[�ߤ���ϥܨ��ױ���
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
        /// �}�l����
        /// </summary>
        public void StartAttack()
        {
            if (!canAttack) return;
            StartCoroutine(AttackFlow());
        }

        /// <summary>
        /// �����y�{
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
        /// ��������G�O�@�A�����G���\�l���O�Ƽg
        /// </summary>
        protected virtual void StopAttack()
        { }

        /// <summary>
        /// �ˬd�����ϰ�O�_�I����ؼйϼh
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

