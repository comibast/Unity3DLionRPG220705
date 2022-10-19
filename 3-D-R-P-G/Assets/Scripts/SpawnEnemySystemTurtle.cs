using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// �ͦ��ĤH�t��
    /// </summary>
    [DefaultExecutionOrder(200)]
    public class SpawnEnemySystemTurtle : MonoBehaviour
    {
        [SerializeField, Header("���s�ͦ��ɶ��d��")]
        private Vector2 rangeRespawn = new Vector2(2, 5);

        private ObjectPoolTurtle objectPoolTurtle;
        private GameObject enemyObject;

        private void Awake()
        {
            objectPoolTurtle = GameObject.Find("����������t").GetComponent<ObjectPoolTurtle>();

            Spawn();
        }

        /// <summary>
        /// �ͦ�
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

