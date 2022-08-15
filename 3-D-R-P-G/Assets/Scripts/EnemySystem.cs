using UnityEngine;
using UnityEngine.AI;

namespace Comibast 
{
    /// <summary>
    /// �ĤH�t�ΡG�C���A�l�ܡA����
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("�ĤH���")]
        private DataEnemy dataEnemy;
        [SerializeField]
        private StateEnemy stateEnemy;

        private Animator ani;
        private NavMeshAgent nma;
        private Vector3 v3TargetPosition;
        private string parWalk = "�}������";

        private float timerIdle;
        #endregion

        #region �ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = dataEnemy.speedWalk;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeTrack);

            Gizmos.color = new Color(0, 0.2f, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeAttack);

            Gizmos.color = new Color(1, 0.2f, 0.3f, 1);
            Gizmos.DrawSphere(v3TargetPosition, 0.3f);
        }

        private void Update()
        {
            StateSwitcher();
            CheckerTargetInTrackRange();
        }
        #endregion

        #region ��k
        /// <summary>
        /// ���A�ഫ��
        /// </summary>
        private void StateSwitcher()
        {
            switch (stateEnemy)
            {
                case StateEnemy.Idle:
                    Idle();
                    break;
                case StateEnemy.Wander:
                    Wander();
                    break;
                case StateEnemy.Track:
                    Track();
                    break;
                case StateEnemy.Attack:
                    Attack();
                    break;
            }
        }

        /// <summary>
        /// �C��
        /// </summary>
        private void Wander()
        {
            //�p�G �Ѿl���Z�� ���� �s
            if (nma.remainingDistance == 0)
            {
                //�H���y�� = �H�� �ꤺ���I * �l�ܽd��
                v3TargetPosition = transform.position + Random.insideUnitSphere * dataEnemy.rangeTrack;
                v3TargetPosition.y = transform.position.y;
            }

            nma.SetDestination(v3TargetPosition);
            ani.SetBool(parWalk, nma.velocity.magnitude > 0.5f);
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Idle()
        {
            nma.velocity = Vector3.zero;
            ani.SetBool(parWalk, false);
            timerIdle += Time.deltaTime;
            print("���ݮɶ��G" + timerIdle);

            float r = Random.Range(dataEnemy.timeIdleRange.x, dataEnemy.timeIdleRange.y);
            if(timerIdle >= r)
            {
                timerIdle = 0;
                stateEnemy = StateEnemy.Wander;
            }

        }

        /// <summary>
        /// �l��
        /// </summary>
        private void Track()
        {
            nma.SetDestination(v3TargetPosition);
            ani.SetBool(parWalk, true);

            if (Vector3.Distance(transform.position, v3TargetPosition) <= dataEnemy.rangeAttack)
            {
                stateEnemy = StateEnemy.Attack;
                //print("�i�J�������A");
            }
        }

        private float timerAttack;
        private string parAttack = "Ĳ�o����";

        /// <summary>
        /// ����
        /// </summary>
        private void Attack()
        {
            ani.SetBool(parWalk, false);
            nma.velocity = Vector3.zero;

            if (timerAttack < dataEnemy.intervalAttack)
            {
                timerAttack += Time.deltaTime;
            }
            else
            {
                ani.SetTrigger(parAttack);
                timerAttack = 0;
            }
        }

        /// <summary>
        /// �ˬd�ؼЬO�_�b�d��
        /// </summary>
        private void CheckerTargetInTrackRange()
        {
            if (stateEnemy == StateEnemy.Attack) return;

            Collider[] hits = Physics.OverlapSphere(transform.position, dataEnemy.rangeTrack, dataEnemy.layerTarget);
            if (hits.Length > 0)
            {
                //print("�I�쪺����G" + hits[0].name);
                v3TargetPosition = hits[0].transform.position;
                stateEnemy = StateEnemy.Track;
            }
        }

        #endregion
    }

}

