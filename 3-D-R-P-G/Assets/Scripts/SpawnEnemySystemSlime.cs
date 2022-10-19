using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// 生成敵人系統
    /// </summary>
    [DefaultExecutionOrder(200)]
    public class SpawnEnemySystemSlime : MonoBehaviour
    {
        [SerializeField, Header("重新生成時間範圍")]
        private Vector2 rangeRespawn = new Vector2(2, 5);

        private ObjectPoolSlime objectPoolSlime;
        private GameObject enemyObject;

        private void Awake()
        {
            objectPoolSlime = GameObject.Find("物件池史萊姆").GetComponent<ObjectPoolSlime>();
            
            Spawn();
        }

        /// <summary>
        /// 生成
        /// </summary>
        private void Spawn()
        {
            enemyObject = objectPoolSlime.GetPoolObject();
            enemyObject.transform.position = transform.position;

            enemyObject.GetComponent<EnemyHealth>().onDead = EnemyDead;
        }

        private void EnemyDead()
        {
            objectPoolSlime.ReleasePoolObject(enemyObject);

            float randomTime = Random.Range(rangeRespawn.x, rangeRespawn.y);
            Invoke("Spawn", randomTime);
        }
    }
}

