namespace VFX
{
    using System.Collections;
    using UnityEngine;

    public class FlashTexture : MonoBehaviour
    {
        public SpriteRenderer sprite;
        public Material flashMaterial;
        public float flashTime = 0.2f;

        Material originalMaterial;
        Coroutine flashRoutine;

        void Awake()
        {
            if (!sprite)
                sprite = GetComponent<SpriteRenderer>();

            originalMaterial = sprite.material;
        }

        public void Flash()
        {
            if (flashRoutine != null)
                StopCoroutine(flashRoutine);

            flashRoutine = StartCoroutine(FlashRoutine());
        }

        IEnumerator FlashRoutine()
        {
            sprite.material = flashMaterial;

            yield return new WaitForSeconds(flashTime);

            sprite.material = originalMaterial;
        }
    }
}