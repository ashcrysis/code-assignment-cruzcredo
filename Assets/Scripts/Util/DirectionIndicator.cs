using UnityEngine;
using Settings;
using Player;

namespace Util
{
    public class DirectionIndicator : MonoBehaviour
    {
        PlayerController player;

        void Awake()
        {
            player = GetComponentInParent<PlayerController>();
        }

        void Start()
        {
            gameObject.SetActive(GameSettings.DirectionalAttack);
        }

        void LateUpdate()
        {
            if (!GameSettings.DirectionalAttack || player == null)
                return;

            Vector2 dir = player.LastMoveDirection;

            if (dir == Vector2.zero)
                return;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector3 scale = transform.localScale;

            if (player.transform.localScale.x < 0)
                scale.y = -Mathf.Abs(scale.y);
            else
                scale.y = Mathf.Abs(scale.y);

            transform.localScale = scale;
        }
    }
}