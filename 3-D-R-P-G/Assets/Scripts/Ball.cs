using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// �y��
    /// </summary>
    public class Ball : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Contains("�a�O"))
            {
                Destroy(gameObject);
            }
        }

    }
}


