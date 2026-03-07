using UnityEngine;
using Settings;
using Player;

namespace Util
{
    public class DirectionIndicator : MonoBehaviour
    {
        PlayerController player;
        SpriteRenderer sr;

        void Awake()
        {
            player = GetComponentInParent<PlayerController>();
            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            sr.enabled = GameSettings.DirectionalAttack;
        }

        void LateUpdate()
        {
            if (!GameSettings.DirectionalAttack || player == null)
                return;

            Vector2 dir = player.LastMoveDirection;

            if (dir == Vector2.zero)
                return;

            if (!GameSettings.DiagonalAttack)
            {
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    dir.y = 0;
                else
                    dir.x = 0;
            }

            if (dir == Vector2.zero)
                return;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector3 scale = transform.localScale;
            scale.y = player.transform.localScale.x < 0 ? -Mathf.Abs(scale.y) : Mathf.Abs(scale.y);
            transform.localScale = scale;
        }
    }
}