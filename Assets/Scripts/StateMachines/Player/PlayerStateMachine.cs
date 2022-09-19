using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    //to access something from the state
   [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public  Animator Animator { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }

    [field: SerializeField] public GroundCheck groundCheck { get; private set; }


    [field: SerializeField] public float FreeLookMovementWalkingSpeed { get; private set; }
    [field: SerializeField] public float FreeLookMovementRunningSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }

    [field: SerializeField] public float HangingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSmoothValue { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLenght { get; private set; }
    

    [field: SerializeField] public float JumpForce { get; private set; }

    [field: SerializeField] public float minimumFallVelocity { get; private set; }

    [field: SerializeField] public float maximumFallVelocity { get; private set; }
    public float FallVelocity;


    [field: SerializeField] public float swordFallKnockbackForce { get; private set; }
    [field: SerializeField] public int swordFallDamage { get; private set; }

    [field: SerializeField] public float axeFallKnockbackForce { get; private set; }
    [field: SerializeField] public int axeFallDamage { get; private set; }

    [field: SerializeField] public float freeHangGrabPointOffset { get; private set; }
    [field: SerializeField] public float wallGrabPointOffset { get; private set; }
    [field: SerializeField] public Attack[] SwordAttacks { get; private set; }
    [field: SerializeField] public Attack HighSwordAttack { get; private set; }
    [field: SerializeField] public Attack LowSwordAttack { get; private set; }
    [field:SerializeField] public Attack WithdrawingSword { get; private set; }
    [field: SerializeField] public Attack WithdrawingAxe { get; private set; }
    [field: SerializeField] public Attack SheatSword { get; private set; }
    [field: SerializeField] public Attack SheatAxe { get; private set; }

    [field: SerializeField] public Attack[] AxeAttacks { get; private set; }
    [field: SerializeField] public Attack HighAxeAttack { get; private set; }
    [field: SerializeField] public Attack LowAxeAttack { get; private set; }

    [field: SerializeField] public WeaponDamage WeaponDamageSword { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamageAxe { get; private set; }

    [field: SerializeField] public Weapon Weapon1 { get; private set; }
    [field: SerializeField] public Weapon Weapon2 { get; private set; }



    [field: SerializeField] public bool WeaponActive { get; private set; }
    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    public AudioSource audioSource;
    public AudioClip battleSound;
   public AudioClip ambientSound;
    public AudioClip deathSound;
    public bool isAmbientSoundPlaying = false;
    public bool isBattleSoundPlaying = false;
    public Vector2 LookValue;
    public bool fallingFromJumping = false;
    public bool wasHanging = false;
    
    

    public bool SwordActive = false;
    public bool AxeActive = false;
    public Weapon currentWeapon;
    

    public Transform MainCameraTransform { get; private set; }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDeath;
    }
    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
        
    }

    private void Update()
    {
        
      currentState?.Tick(Time.deltaTime);
        if(SwordActive || AxeActive)
        {
            WeaponActive = true;
        }
        else
        {
            WeaponActive = false;
        }

        

        //if(CharacterController.velocity.y<minimumFallVelocity&&!fallingFromJumping )
        //{
        //    SwitchState(new PlayerFallingState(this));
        //}
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDeath()
    {
        SwitchState(new PlayerDeadState(this));
    }

    

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDeath;
    }

   



}
