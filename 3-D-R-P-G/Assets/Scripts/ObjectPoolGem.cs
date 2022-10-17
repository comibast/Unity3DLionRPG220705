using UnityEngine;
using UnityEngine.Pool;

namespace Comibast
{
    /// <summary>
    /// ������G�H��
    /// </summary>
    public class ObjectPoolGem : MonoBehaviour
    {
        [SerializeField, Header("�H��")]
        private GameObject prefabGem;
        [SerializeField, Header("�H���̤j�ƶq")]
        private int countMaxGem = 30;

        /// <summary>
        /// �H�������
        /// </summary>
        private ObjectPool<GameObject> poolGem;

        private int count;

        private void Awake()
        {
            poolGem = new ObjectPool<GameObject>(
                CreatePool, GetGem, ReleaseGem, DestroyGem, false, countMaxGem);
        }

        /// <summary>
        /// �إߪ�����ɰ����B�z
        /// </summary>
        private GameObject CreatePool()
        {
            count++;
            GameObject temp = Instantiate(prefabGem);
            temp.name = prefabGem.name + " " + count;
            return temp;
        }

        /// <summary>
        /// ���o���������ɳB�z���欰
        /// </summary>
        private void GetGem(GameObject gem)
        {
            gem.SetActive(true);
        }

        /// <summary>
        /// �N�����ٵ�������ɳB�z���欰
        /// </summary>
        private void ReleaseGem(GameObject gem)
        {
            gem.SetActive(false);
        }

        /// <summary>
        /// �ƶq�W�X������e�q�ɰ����欰
        /// </summary>
        private void DestroyGem(GameObject gem)
        {
            Destroy(gem);
        }

        /// <summary>
        /// ���o�������������
        /// </summary>
        public GameObject GetPoolObject()
        {
            return poolGem.Get();
        }

        /// <summary>
        /// �N�����٨쪫�����
        /// </summary>
        /// <param name="gem"></param>
        public void ReleasePoolObject(GameObject gem)
        {
            poolGem.Release(gem);
        }


    }

}
