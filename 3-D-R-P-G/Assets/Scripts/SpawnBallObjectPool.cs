using UnityEngine;
using UnityEngine.Pool;

namespace Comibast
{
    /// <summary>
    /// �ͦ��y��G�ϥΪ����
    /// </summary>
    public class SpawnBallObjectPool : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabBall;

        /// <summary>
        /// �y�骫���
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
        /// �إߪ�����ɭn�B�z���欰
        /// </summary>
        /// <returns></returns>
        private GameObject CreatePool()
        {
            return Instantiate(prefabBall);
        }

        /// <summary>
        /// �򪫥��������
        /// </summary>
        private void GetBall(GameObject ball)
        {
            ball.SetActive(true);
        }

        /// <summary>
        /// �⪫���ٵ������
        /// </summary>
        private void ReleaseBall(GameObject ball)
        {
            ball.SetActive(false);
        }

        /// <summary>
        /// �ƶq�W�X������e�q�n�����B�z
        /// </summary>
        /// <param name="ball"></param>
        private void DestoryBall(GameObject ball)
        {
            Destroy(ball);
        }

        /// <summary>
        /// �ͦ��y��
        /// </summary>
        private void Spawn()
        {
            Vector3 pos;
            pos.x = Random.Range(-15f, 15f);
            pos.y = Random.Range(5f, 7f);
            pos.z = Random.Range(-15f, 15f);

            //�򪫥�����y��
            GameObject tempBall = poolBall.Get();
            tempBall.transform.position = pos;

            //�y�I�����ٵ������
            tempBall.GetComponent<BallObjectPool>().onHit = BallHitAndRelease;
        }

        /// <summary>
        /// �y��I����^��
        /// </summary>
        private void BallHitAndRelease(GameObject ball)
        {
            //��y�ٵ������
            poolBall.Release(ball);
        }


    }

}

