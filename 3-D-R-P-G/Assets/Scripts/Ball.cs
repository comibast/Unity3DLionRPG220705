using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// ²yÅé
    /// </summary>
    public class Ball : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Contains("¦aªO"))
            {
                Destroy(gameObject);
            }
        }

    }
}


