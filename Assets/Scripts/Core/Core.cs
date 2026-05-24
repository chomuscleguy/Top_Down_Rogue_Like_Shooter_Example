using System.Collections.Generic;
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
    public PickupManager Pickup { get; private set; }
    public EnemyGridManager Grid { get; private set; }

    [Header("System")]
    public TickSystem Tick { get; private set; }

    public GameObject ProjectileRoot;

    private List<IManager> managers;

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
        Game = GetComponentInChildren<GameManager>();
        Audio = GetComponentInChildren<AudioManager>();
        UI = GetComponentInChildren<UIManager>();
        Save = GetComponentInChildren<SaveManager>();
        Data = GetComponentInChildren<DataManager>();
        Drop = GetComponentInChildren<DropManager>();
        Pickup = GetComponentInChildren<PickupManager>();
        Grid = GetComponentInChildren<EnemyGridManager>();
        Tick = GetComponentInChildren<TickSystem>();

        managers = new List<IManager>
        {
            Save,
            Game,
            Audio,
            UI,
            Data,
            Drop,
            Pickup,
            Grid,
            Tick,
        };

        foreach (var m in managers)
        {
            if (m == null)
                continue;
            m.Init();
        }
    }
}