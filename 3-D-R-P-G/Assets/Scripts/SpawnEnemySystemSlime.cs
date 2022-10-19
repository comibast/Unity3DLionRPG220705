using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// �ͦ��ĤH�t��
    /// </summary>
    [DefaultExecutionOrder(200)]
    public class SpawnEnemySystemSlime : MonoBehaviour
    {
        [SerializeField, Header("���s�ͦ��ɶ��d��")]
        private Vector2 rangeRespawn = new Vector2(2, 5);

        private ObjectPoolSlime objectPoolSlime;
        private GameObject enemyObject;

        private void Awake()
        {
            objectPoolSlime = GameObject.Find("������v�ܩi").GetComponent<ObjectPoolSlime>();
            
            Spawn();
        }

        /// <summary>
        /// �ͦ�
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

