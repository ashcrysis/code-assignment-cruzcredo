namespace Util
{
    using UnityEngine;

    public class SyncRotation : MonoBehaviour
    {
        public Transform target;

        void LateUpdate()
        {
            if (target == null) return;

            transform.rotation = target.rotation;
        }
    }
}