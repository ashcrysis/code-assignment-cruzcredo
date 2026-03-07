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
        
        void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Input = GetComponent<PlayerInputReader>();
            Stats = GetComponent<PlayerStats>();

            StateMachine = new PlayerStateMachine();

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
            StateMachine.Update();
            Input.ResetInput();
        }

        public void Move()
        {
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
            // ATAQUE DIRECIONAL
            if (GameSettings.DirectionalAttack)
            {
                Vector2 dir = Input.MoveInput;

                if (dir == Vector2.zero)
                    dir = LastMoveDirection;

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                AttackHitbox.transform.rotation = Quaternion.Euler(0, 0, angle);

                if (dir.x != 0)
                    transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);
            }

            // SISTEMA ORIGINAL
            else
            {
                if (Stats.currentCombo == Stats.maxCombo)
                {
                    AttackHitbox.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    AttackHitbox.transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}