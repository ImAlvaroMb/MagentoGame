using UnityEngine;

public abstract class AbstractAutoInitializableMonoBehaviour : MonoBehaviour
{

    [Tooltip("If true, Construct() will be called in Awake.")]
    [SerializeField] protected bool _autoConstructOnAwake = true;
    [Tooltip("If true, Initialize() will be called in Start once if not already initialized in Construct (Awake).")]
    [SerializeField] protected bool _autoInitializeOnStart = true;

    protected bool _isConstructed = false;
    protected bool _isInitialized = false;

    protected virtual void Awake()
    {
        HandleAutoConstructionAwake();
    }

    protected virtual void HandleAutoConstructionAwake()
    {
        if (_autoConstructOnAwake)
            PerformConstruct();
    }
    protected virtual void Start()
    {
        HandleAutoInitializationStart();
    }

    protected virtual void HandleAutoInitializationStart()
    {
        if (!_isConstructed && _autoConstructOnAwake)
        {
            PerformConstruct();
        }

        if (_autoInitializeOnStart && !_isInitialized)
            PerformInitialize();
    }

    public void PerformConstruct()
    {
        if (_isConstructed) return;
        _isConstructed = true;
        Construct();
    }

    public void PerformInitialize()
    {
        if (_isInitialized) return;
        if (!_isConstructed)
        {
            Debug.LogWarning($"Attempting to Initialize {gameObject.name} before it's Constructed. Forcing Construct.");
            PerformConstruct();
        }
        _isInitialized = true;
        Initialize();
    }

    /// <summary>
    /// Called during the Awake phase if autoConstructOnAwake is true, or when PerformConstruct is called.
    /// Use for setting up essential references and dependencies.
    /// </summary>
    protected abstract void Construct();

    /// <summary>
    /// Called during the Start phase if autoInitializeOnStart is true (and not already initialized), 
    /// or when PerformInitialize is called. Use for logic that may depend on other components having run their Construct/Awake.
    /// </summary>
    protected virtual void Initialize()
    {

    }


    private void OnDestroy()
    {
        Deconstruct();
    }

    protected virtual void Deconstruct()
    {

        _isInitialized = false;
        _isConstructed = false;
    }

}
