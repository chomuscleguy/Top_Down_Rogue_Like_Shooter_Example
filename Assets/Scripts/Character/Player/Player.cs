using UnityEngine;

public class Player : MonoBehaviour, ICombatStatProvider, IMovementStatProvider, ISurvivalStatProvider, IProjectileStatProvider, IUtilityStatProvider
{
    public Health Health { get; private set; }
    public Experience Experience { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public WeaponController Weapon { get; private set; }
    public CircleCollider2D Collider { get; private set; }
    public RunData Run { get; private set; }

    private PlayerInputHandler inputHandler;

    [SerializeField]
    private PlayerUI playerUI;


    private void Awake()
    {
        Health = GetComponent<Health>();
        Movement = GetComponent<PlayerMovement>();
        Weapon = GetComponent<WeaponController>();
        Collider = GetComponent<CircleCollider2D>();
        Experience = GetComponent<Experience>();

        inputHandler = GetComponent<PlayerInputHandler>();

        BindInput();
    }

    public void Init(RunData run)
    {
        Run = run;

        BindSystems();

        RegisterTicks();

        InitUI();

        InitComponents();

        ApplyStats(run.FinalStats);

        Core.Instance.Orb.SetPlayer(transform);
    }

    private void InitUI()
    {
        playerUI.Init(Run, Run);
    }

    private void InitComponents()
    {
        Health.Init(Run.FinalStats.survival.maxHP);
        Movement.Init(Collider.radius);
        Weapon.Init(Run);
        Experience.Init(Run);
    }

    private void BindInput()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMove += Movement.SetInput;
        }
    }

    private void UnbindInput()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMove -= Movement.SetInput;
        }
    }

    private void BindSystems()
    {
        Run.OnStatsChanged += ApplyStats;
    }

    private void UnbindSystems()
    {
        if (Run == null)
            return;

        Run.OnStatsChanged -= ApplyStats;
    }

    private void RegisterTicks()
    {
        Core.Instance.Tick.Register(Movement);

        Core.Instance.Tick.Register(Run);
    }

    private void UnregisterTicks()
    {
        if (Core.Instance == null)
            return;

        Core.Instance.Tick.Unregister(Movement);

        Core.Instance.Tick.Unregister(Run);
    }

    private void ApplyStats(CharacterStats stats)
    {
        Movement.ApplyStats(stats.movement);

        Health.ApplyStats(stats.survival);

        Weapon.ApplyStatsToAll(Run, Run.Items);
    }

    public CombatStats GetCombatStats()
    {
        return Run.FinalStats.combat;
    }

    public MovementStats GetMovementStats()
    {
        return Run.FinalStats.movement;
    }

    public SurvivalStats GetSurvivalStats()
    {
        return Run.FinalStats.survival;
    }

    public ProjectileStats GetProjectileStats()
    {
        return Run.FinalStats.projectile;
    }

    public UtilityStats GetUtilityStats()
    {
        return Run.FinalStats.utility;
    }

    private void OnDestroy()
    {
        UnbindInput();

        UnbindSystems();

        UnregisterTicks();
    }
}