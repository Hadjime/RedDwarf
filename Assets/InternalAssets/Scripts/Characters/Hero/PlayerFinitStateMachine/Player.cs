using InternalAssets.Scripts.Characters.Hero.Data;
using InternalAssets.Scripts.Characters.Hero.Input;
using InternalAssets.Scripts.Characters.Hero.PlayerFinitStateMachine.PlayerStates;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Hero.PlayerFinitStateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private LayerMask maskForMove;
        [SerializeField] private LayerMask maskForAttack;
        [SerializeField] private LayerMask maskForDig;
        
        private Rigidbody2D _rb2D;
        
        public PlayerFSM playerFSM { get; private set; }
        public PlayerInputHandler inputHandler { get; private set; }
        public Animator animator { get; private set;}

        #region Player States

        public PlayerTestState testState { get; private set; }
        public PlayerIdleState idleState { get; private set; }
        public PlayerMoveState moveState { get; private set; }
        public PlayerDigState digState { get; private set; }
        
        #endregion

        public void Awake()
        {
            playerFSM = new PlayerFSM();
            testState = new PlayerTestState(this, playerFSM, playerData, Animator.StringToHash("isIdle") );
            idleState = new PlayerIdleState(this, playerFSM, playerData, Animator.StringToHash("isIdle") );
            moveState = new PlayerMoveState(this, playerFSM, playerData, Animator.StringToHash("isRun") );
            digState = new PlayerDigState(this, playerFSM, playerData, Animator.StringToHash("isDigging") );
        }

        public void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            inputHandler = GetComponent<PlayerInputHandler>();
            playerFSM.Initialize(idleState);
        }

        public void Update()
        {
            playerFSM.CurrentState.LogicUpdate();
        }

        public void FixedUpdate()
        {
            playerFSM.CurrentState.PhysicsUpdate();
        }

        public void Movement()
        {
            _rb2D.MovePosition(position: _rb2D.position + ( inputHandler.RawMovementInput * (Time.fixedDeltaTime * playerData.movementVelocity)) );
        }
        
        public bool CheckPointForMove(Vector2 point)
        {
            var position = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(position, point, 1, maskForMove);
            Debug.DrawRay(position, point, Color.green);
            return hit.collider == null;
        }
        
        public bool CheckPointForDig(Vector2 point)
        {
            var position = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(position, point, 1, maskForDig);
            Debug.DrawRay(position, point, Color.red);
            return hit.collider != null;
        }
        
        public void Rotation(Vector2 inputVector)
        {
            if (inputVector == Vector2.up) _rb2D.rotation = 90;
            if (inputVector == Vector2.down) _rb2D.rotation = 270;
            if (inputVector == Vector2.left) _rb2D.rotation = 180;
            if (inputVector == Vector2.right) _rb2D.rotation = 0;
        }
    }
}