using UnityEngine;

namespace UI
{
    public class UIIdleRotate : MonoBehaviour
    {
        public float angle = 5f;
        public float speed = 2f;

        private Quaternion startRotation;

        void Start()
        {
            startRotation = transform.rotation;
        }

        void Update()
        {
            float z = Mathf.Sin(Time.time * speed) * angle;
            transform.rotation = Quaternion.Euler(0f, 0f, z);
        }

    }
}