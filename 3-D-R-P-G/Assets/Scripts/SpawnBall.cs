using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// 生成球體：不使用物件池
    /// </summary>
    public class SpawnBall : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabBall;

        private void Awake()
        {
            InvokeRepeating("Spawn", 0, 0.1f);
        }

        /// <summary>
        /// 生成
        /// </summary>
        private void Spawn()
        {
            Vector3 pos;
            pos.x = Random.Range(-15f, 15f);
            pos.y = Random.Range(5f, 7f);
            pos.z = Random.Range(-15f, 15f);

            Instantiate(prefabBall, pos, Quaternion.identity);
        }


    }
}

