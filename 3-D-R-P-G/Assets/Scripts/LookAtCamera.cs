using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// 面向攝影機
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
        /// 面向攝影機
        /// </summary>
        private void LookAt()
        {
            transform.LookAt(traCamera);
        }
    }
}

