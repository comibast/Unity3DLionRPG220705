using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// ���V��v��
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {

        private Transform traCamera;

        private void Awake()
        {
            traCamera = Camera.main.transform;
        }

        private void Update()
        {
            LookAt();
        }

        /// <summary>
        /// ���V��v��
        /// </summary>
        private void LookAt()
        {
            transform.LookAt(traCamera);
        }
    }
}

