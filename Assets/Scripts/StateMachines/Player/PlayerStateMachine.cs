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

    [field: SerializeField] public float FreeLookMovementWalkingSpeed { get; private set; }
    [field: SerializeField] public float FreeLookMovementRunningSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSmoothValue { get; private set; }

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

    public AudioSource audioSource;
    public AudioClip battleSound;
   public AudioClip ambientSound;
    public bool isAmbientSoundPlaying = false;
    public bool isBattleSoundPlaying = false;
    public Vector2 LookValue;

    public bool SwordActive = false;
    public bool AxeActive = false;
    public Weapon currentWeapon;
    

    public Transform MainCameraTransform { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player state Machine initialized");
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
        
    }

    private void Update()
    {
        //Debug.Log(currentState);
        currentState?.Tick(Time.deltaTime);
        if(SwordActive || AxeActive)
        {
            WeaponActive = true;
        }
        else
        {
            WeaponActive = false;
        }
    }






}
