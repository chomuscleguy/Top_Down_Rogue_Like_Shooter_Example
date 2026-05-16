using UnityEngine;

public class Core : MonoBehaviour
{
    public static Core Instance { get; private set; }

    [Header("Manager")]
    public GameManager Game { get; private set; }
    public AudioManager Audio { get; private set; }
    public UIManager UI { get; private set; }
    public SaveManager Save { get; private set; }
    public DataManager Data { get; private set; }
    public DropManager Drop { get; private set; }
    public OrbManager Orb { get; private set; }

    [Header("System")]
    public TickSystem Tick { get; private set; }
    public CombatSystem Combat { get; private set; }

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        // Managers
        Game = GetComponentInChildren<GameManager>();
        Audio = GetComponentInChildren<AudioManager>();
        UI = GetComponentInChildren<UIManager>();
        Save = GetComponentInChildren<SaveManager>();
        Data = GetComponentInChildren<DataManager>();
        Drop = GetComponentInChildren<DropManager>();
        Orb = GetComponentInChildren<OrbManager>();

        // Systems
        Tick = GetComponentInChildren<TickSystem>();
        Combat = GetComponentInChildren<CombatSystem>();
    }
}