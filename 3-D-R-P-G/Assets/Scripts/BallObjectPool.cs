using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// �y��G���������
    /// </summary>
    public class BallObjectPool : MonoBehaviour
    {
        public delegate void delegateHit(GameObject ball);
        public delegateHit onHit;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Contains("�a�O"))
            {
                onHit(gameObject);
            }
        }
    }
}

