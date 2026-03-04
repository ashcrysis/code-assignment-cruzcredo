using Combat;
using Player.Player;
using Player.States;

namespace Player
{
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        public PlayerStats Stats;
        public float moveSpeed = 5f;

        public Rigidbody2D Rb;
        public PlayerInputReader Input;

        public PlayerStateMachine StateMachine;

        public IdleState IdleState;
        public MoveState MoveState;
        public CombatState CombatState;
        public DashState DashState;
        public GameObject AttackHitbox;
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
            Rb.linearVelocity = Input.MoveInput.normalized * Stats.moveSpeed;
        }

        public void Stop()
        {
            Rb.linearVelocity = Vector2.zero;
        }
    }
}