using UnityEngine;
using UnityEngine.Pool;

namespace Comibast
{
    /// <summary>
    /// 生成球體：使用物件池
    /// </summary>
    public class SpawnBallObjectPool : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabBall;

        /// <summary>
        /// 球體物件池
        /// </summary>
        private ObjectPool<GameObject> poolBall;

        private void Awake()
        {
            //
            poolBall = new ObjectPool<GameObject>(
                CreatePool, GetBall, ReleaseBall, DestoryBall, false, 100);
            InvokeRepeating("Spawn", 0, 0.1f);
        }

        /// <summary>
        /// 建立物件池時要處理的行為
        /// </summary>
        /// <returns></returns>
        private GameObject CreatePool()
        {
            return Instantiate(prefabBall);
        }

        /// <summary>
        /// 跟物件池拿物件
        /// </summary>
        private void GetBall(GameObject ball)
        {
            ball.SetActive(true);
        }

        /// <summary>
        /// 把物件還給物件池
        /// </summary>
        private void ReleaseBall(GameObject ball)
        {
            ball.SetActive(false);
        }

        /// <summary>
        /// 數量超出物件池容量要做的處理
        /// </summary>
        /// <param name="ball"></param>
        private void DestoryBall(GameObject ball)
        {
            Destroy(ball);
        }

        /// <summary>
        /// 生成球體
        /// </summary>
        private void Spawn()
        {
            Vector3 pos;
            pos.x = Random.Range(-15f, 15f);
            pos.y = Random.Range(5f, 7f);
            pos.z = Random.Range(-15f, 15f);

            //跟物件池拿球體
            GameObject tempBall = poolBall.Get();
            tempBall.transform.position = pos;

            //球碰撞時還給物件池
            tempBall.GetComponent<BallObjectPool>().onHit = BallHitAndRelease;
        }

        /// <summary>
        /// 球體碰撞後回收
        /// </summary>
        private void BallHitAndRelease(GameObject ball)
        {
            //把球還給物件池
            poolBall.Release(ball);
        }


    }

}

