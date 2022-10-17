using UnityEngine;
using UnityEngine.Pool;

namespace Comibast
{
    /// <summary>
    /// 物件池：碎片
    /// </summary>
    public class ObjectPoolGem : MonoBehaviour
    {
        [SerializeField, Header("碎片")]
        private GameObject prefabGem;
        [SerializeField, Header("碎片最大數量")]
        private int countMaxGem = 30;

        /// <summary>
        /// 碎片物件池
        /// </summary>
        private ObjectPool<GameObject> poolGem;

        private int count;

        private void Awake()
        {
            poolGem = new ObjectPool<GameObject>(
                CreatePool, GetGem, ReleaseGem, DestroyGem, false, countMaxGem);
        }

        /// <summary>
        /// 建立物件池時做的處理
        /// </summary>
        private GameObject CreatePool()
        {
            count++;
            GameObject temp = Instantiate(prefabGem);
            temp.name = prefabGem.name + " " + count;
            return temp;
        }

        /// <summary>
        /// 取得物件池物件時處理的行為
        /// </summary>
        private void GetGem(GameObject gem)
        {
            gem.SetActive(true);
        }

        /// <summary>
        /// 將物件還給物件池時處理的行為
        /// </summary>
        private void ReleaseGem(GameObject gem)
        {
            gem.SetActive(false);
        }

        /// <summary>
        /// 數量超出物件池容量時做的行為
        /// </summary>
        private void DestroyGem(GameObject gem)
        {
            Destroy(gem);
        }

        /// <summary>
        /// 取得物件池內的物件
        /// </summary>
        public GameObject GetPoolObject()
        {
            return poolGem.Get();
        }

        /// <summary>
        /// 將物件還到物件池內
        /// </summary>
        /// <param name="gem"></param>
        public void ReleasePoolObject(GameObject gem)
        {
            poolGem.Release(gem);
        }


    }

}
