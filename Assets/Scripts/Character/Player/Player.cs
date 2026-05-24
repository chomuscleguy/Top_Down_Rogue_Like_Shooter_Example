using UnityEngine;

public class Player : MonoBehaviour, ICombatStatProvider, IMovementStatProvider, ISurvivalStatProvider, IProjectileStatProvider, IUtilityStatProvider, IWeaponStatProvider
{
    public Health Health { get; private set; }
    public Experience Experience { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public WeaponSystem WeaponSystem { get; private set; }
    public TargetScanner Scanner { get; private set; }
    public CircleCollider2D Collider { get; private set; }
    public HitFlash hitFlash { get; private set; }
    public RunData Run { get; private set; }

    private PlayerInputHandler inputHandler;

    [SerializeField]
    private PlayerUI playerUI;

    private void Awake()
    {
        Health = GetComponent<Health>();
        Experience = GetComponent<Experience>();
        Movement = GetComponent<PlayerMovement>();
        Scanner = GetComponent<TargetScanner>();
        Collider = GetComponent<CircleCollider2D>();
        inputHandler = GetComponent<PlayerInputHandler>();
        WeaponSystem = GetComponent<WeaponSystem>();
        hitFlash = GetComponent<HitFlash>();

        BindInput();
    }

    public void Init(RunData run)
    {
        Run = run;

        InitComponents();
        BindSystems();
        RegisterTicks();
        InitUI();
        ApplyStats(Run.FinalStats);
        run.BindWeaponSystem(WeaponSystem);

        Core.Instance.Grid.SetPlayer(transform);
        Core.Instance.Pickup.SetPlayer(this);
    }

    private void InitComponents()
    {
        Health.Init(Run.FinalStats.survival.maxHP);
        Movement.Init(Collider.radius);
        Scanner.Init();
        Experience.Init(Run);
        WeaponSystem.Init(transform, Run, Scanner, statProvider: this);
    }

    private void InitUI()
    {
        playerUI.Init(Health, Run);
    }

    private void BindInput()
    {
        if (inputHandler == null)
            return;

        inputHandler.OnMove += Movement.SetInput;
    }

    private void UnbindInput()
    {
        if (inputHandler == null)
            return;

        inputHandler.OnMove -= Movement.SetInput;
    }

    private void BindSystems()
    {
        if (Run == null)
            return;

        Run.OnStatsChanged -= ApplyStats;
        Run.OnStatsChanged += ApplyStats;

        Run.OnInventoryChanged -= RebuildWeapons;
        Run.OnInventoryChanged += RebuildWeapons;

        Health.OnDamageTaken -= handleHit;
        Health.OnDamageTaken += handleHit;
    }

    private void UnbindSystems()
    {
        if (Run == null)
            return;

        Run.OnStatsChanged -= ApplyStats;
        Run.OnInventoryChanged -= RebuildWeapons;
        Health.OnDamageTaken -= handleHit;
    }

    private void RegisterTicks()
    {
        Core.Instance.Tick.Register(Movement);
        Core.Instance.Tick.Register(Run);
        Core.Instance.Tick.Register(WeaponSystem);
    }

    private void UnregisterTicks()
    {
        if (Core.Instance == null)
            return;

        Core.Instance.Tick.Unregister(Movement);
        Core.Instance.Tick.Unregister(Run);
        Core.Instance.Tick.Unregister(WeaponSystem);
    }

    private void ApplyStats(CharacterStats stats)
    {
        Movement.ApplyStats(stats.movement);
        Health.ApplyStats(stats.survival);
        WeaponSystem.RebuildStats(this);
    }

    private void RebuildWeapons()
    {
        foreach (var (item, level) in Run.Items.GetAllItems())
        {
            if (item is not WeaponData weaponData)
                continue;

            WeaponSystem.AddOrLevelUpWeapon(weaponData, level);
        }

        WeaponSystem.RebuildStats(this);
    }

    private void handleHit(float damage)
    {
        hitFlash?.Play();
    }

    public CombatStats GetCombatStats() => Run.FinalStats.combat;
    public MovementStats GetMovementStats() => Run.FinalStats.movement;
    public SurvivalStats GetSurvivalStats() => Run.FinalStats.survival;
    public ProjectileStats GetProjectileStats() => Run.FinalStats.projectile;
    public UtilityStats GetUtilityStats() => Run.FinalStats.utility;

    private void OnDestroy()
    {
        UnbindInput();
        UnbindSystems();
        UnregisterTicks();
    }
}