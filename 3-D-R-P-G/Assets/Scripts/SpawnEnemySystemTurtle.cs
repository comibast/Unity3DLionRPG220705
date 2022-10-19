using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// 生成敵人系統
    /// </summary>
    [DefaultExecutionOrder(200)]
    public class SpawnEnemySystemTurtle : MonoBehaviour
    {
        [SerializeField, Header("重新生成時間範圍")]
        private Vector2 rangeRespawn = new Vector2(2, 5);

        private ObjectPoolTurtle objectPoolTurtle;
        private GameObject enemyObject;

        private void Awake()
        {
            objectPoolTurtle = GameObject.Find("物件池盔甲龜").GetComponent<ObjectPoolTurtle>();

            Spawn();
        }

        /// <summary>
        /// 生成
        /// </summary>
        private void Spawn()
        {
            enemyObject = objectPoolTurtle.GetPoolObject();
            enemyObject.transform.position = transform.position;

            enemyObject.GetComponent<EnemyHealth>().onDead = EnemyDead;
        }

        private void EnemyDead()
        {
            objectPoolTurtle.ReleasePoolObject(enemyObject);

            float randomTime = Random.Range(rangeRespawn.x, rangeRespawn.y);
            Invoke("Spawn", randomTime);
        }
    }
}

