namespace VFX
{
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    public class YSortSprite : MonoBehaviour
    {
        SpriteRenderer sr;

        [Header("Settings")]
        public int offset = 0;
        public float multiplier = 100f;

        void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        void LateUpdate()
        {
            sr.sortingOrder = (int)(-transform.position.y * multiplier) + offset;
        }
    }
}