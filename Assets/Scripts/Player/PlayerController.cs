using Combat;
using Player.Player;
using Player.States;
using Settings;

namespace Player
{
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [Header("Stats")]
        public PlayerStats Stats;

        [Header("Components")]
        public Rigidbody2D Rb;
        public PlayerInputReader Input;
        public Animator anim;

        [Header("Combat")]
        public GameObject AttackHitbox;

        [Header("State Machine")]
        public PlayerStateMachine StateMachine;

        [HideInInspector] public IdleState IdleState;
        [HideInInspector] public MoveState MoveState;
        [HideInInspector] public CombatState CombatState;
        [HideInInspector] public DashState DashState;
        
        [HideInInspector] public Vector2 LastMoveDirection = Vector2.right;
        public SpriteRenderer sr;
        void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Input = GetComponent<PlayerInputReader>();
            Stats = GetComponent<PlayerStats>();

            StateMachine = new PlayerStateMachine();
            
            sr = GetComponent<SpriteRenderer>();

            IdleState = new IdleState(this, StateMachine);
            MoveState = new MoveState(this, StateMachine);
            CombatState = new CombatState(this, StateMachine);
            DashState = new DashState(this, StateMachine);
        }

        void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        void Update()
        {
            if (GameState.Paused) return;
            StateMachine.Update();
            Input.ResetInput();
        }

        public void Move()
        {
            if (Stats.IsKnockedBack) return;

            if (Input.MoveInput != Vector2.zero)
                LastMoveDirection = Input.MoveInput.normalized;

            Rb.linearVelocity = Input.MoveInput.normalized * Stats.moveSpeed;
        }

        public void Stop()
        {
            Rb.linearVelocity = Vector2.zero;
        }
        public void EnableHitbox()
        {
            AttackHitbox.SetActive(true);
        }

        public void DisableHitbox()
        {
            AttackHitbox.SetActive(false);
        }
        public void UpdateAttackDirection()
        {
            bool facingLeft = sr.flipX;

            if (GameSettings.DirectionalAttack)
            {
                Vector2 dir = LastMoveDirection;

                if (dir == Vector2.zero)
                    dir = LastMoveDirection;

                if (Stats.currentCombo == Stats.maxCombo && dir != Vector2.down)
                    dir = Vector2.up;
                

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                AttackHitbox.transform.rotation = Quaternion.Euler(0, 0, angle);

                if (dir.x != 0)
                    sr.flipX = dir.x < 0;
            }
            else
            {
                if (Stats.currentCombo == Stats.maxCombo)
                {
                    AttackHitbox.transform.rotation = Quaternion.Euler(0, 0, 90f);
                }
                else
                {
                    float angle = facingLeft ? 180f : 0f;
                    AttackHitbox.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
        }
        public void ApplyKnockback(Vector2 sourcePosition, float force)
        {
            Vector2 direction = ((Vector2)transform.position - sourcePosition).normalized;

            Rb.linearVelocity = Vector2.zero;
            Rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }
}