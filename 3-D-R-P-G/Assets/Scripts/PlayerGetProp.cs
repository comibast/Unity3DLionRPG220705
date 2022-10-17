using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// ���a���o�D��
    /// </summary>
    public class PlayerGetProp : MonoBehaviour
    {
        private ObjectPoolGem objectPoolGem;
        private string propGem = "Gem";

        private void Awake()
        {
            objectPoolGem = FindObjectOfType<ObjectPoolGem>();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.name.Contains(propGem))
            {
                objectPoolGem.ReleasePoolObject(hit.gameObject);
            }
        }

    }

}
