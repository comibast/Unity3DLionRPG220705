using UnityEngine;
using UnityEngine.AI;

namespace Comibast 
{
    /// <summary>
    /// 敵人系統：遊走，追蹤，攻擊
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("敵人資料")]
        private DataEnemy dataEnemy;
        [SerializeField]
        private StateEnemy stateEnemy;

        private Animator ani;
        private NavMeshAgent nma;
        private Vector3 v3TargetPosition;
        private string parWalk = "開關走路";

        private float timerIdle;
        #endregion

        #region 事件
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

        #region 方法
        /// <summary>
        /// 狀態轉換器
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
        /// 遊走
        /// </summary>
        private void Wander()
        {
            //如果 剩餘的距離 等於 零
            if (nma.remainingDistance == 0)
            {
                //隨機座標 = 隨機 圓內的點 * 追蹤範圍
                v3TargetPosition = transform.position + Random.insideUnitSphere * dataEnemy.rangeTrack;
                v3TargetPosition.y = transform.position.y;
            }

            nma.SetDestination(v3TargetPosition);
            ani.SetBool(parWalk, nma.velocity.magnitude > 0.5f);
        }

        /// <summary>
        /// 等待
        /// </summary>
        private void Idle()
        {
            nma.velocity = Vector3.zero;
            ani.SetBool(parWalk, false);
            timerIdle += Time.deltaTime;
            print("等待時間：" + timerIdle);

            float r = Random.Range(dataEnemy.timeIdleRange.x, dataEnemy.timeIdleRange.y);
            if(timerIdle >= r)
            {
                timerIdle = 0;
                stateEnemy = StateEnemy.Wander;
            }

        }

        /// <summary>
        /// 追蹤
        /// </summary>
        private void Track()
        {
            nma.SetDestination(v3TargetPosition);
            ani.SetBool(parWalk, true);

            if (Vector3.Distance(transform.position, v3TargetPosition) <= dataEnemy.rangeAttack)
            {
                stateEnemy = StateEnemy.Attack;
                //print("進入攻擊狀態");
            }
        }

        private float timerAttack;
        private string parAttack = "觸發攻擊";

        /// <summary>
        /// 攻擊
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
        /// 檢查目標是否在範圍內
        /// </summary>
        private void CheckerTargetInTrackRange()
        {
            if (stateEnemy == StateEnemy.Attack) return;

            Collider[] hits = Physics.OverlapSphere(transform.position, dataEnemy.rangeTrack, dataEnemy.layerTarget);
            if (hits.Length > 0)
            {
                //print("碰到的物件：" + hits[0].name);
                v3TargetPosition = hits[0].transform.position;
                stateEnemy = StateEnemy.Track;
            }
        }

        #endregion
    }

}

