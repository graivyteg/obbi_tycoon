using System;
using System.Collections.Generic;
using Audio;
using DG.Tweening;
using NaughtyAttributes;
using Triggers;
using UnityEngine;
using YG;
using Zenject;

public class Building : MonoYandex
{
    [Inject] private SoundController _soundController;
    [Inject] private GameSaver _saver;
    [Inject] private MoneyGenerator _generator;
    [Inject] private Player _player;

    [ReadOnly]
    public string UUID = "";
    public string Name = "Дом";
    public int Price = 0;
    public float Bonus = 1;
    
    [Space]
    public Transform Object;
    public MeshRenderer Mockup;
    public BuildingTrigger Trigger;
    
    [Space]
    public Action OnBuild;
    public Action<bool> OnAvailableUpdate;
    [ReadOnly] public bool IsBuilt = false;
    [ReadOnly] public bool IsAvailable = false;

    [SerializeField] private List<Building> _dependencies;
    [SerializeField] private List<GameObject> _citizens;
    [SerializeField] private List<MeshRenderer> _children = new();

    [SerializeField] private Material _mockupMaterial;
    [Foldout("Advanced")]
    [SerializeField] private float _buildTime = 0.7f;

    private int _dependenciesBuilt = 0;
    private Vector3 _defaultScale;

    private float _dependencyTimer = 0;
    private const float DependencyUpdateTime = 1f; 

    private void OnValidate()
    {
        if (UUID == "")
        {
            UUID = Guid.NewGuid().ToString();
        }
    }

    private void Awake()
    {
        Trigger = GetComponentInChildren<BuildingTrigger>();
        Trigger.Initialize(this);
        
        _defaultScale = Object.localScale;
        Object.localScale = Vector3.zero;
        _citizens.ForEach(c => c.SetActive(false));
    }

    protected override void OnSDK()
    {
        if (YandexGame.savesData.buildings.Contains(UUID))
        {
            Build(false);
        }
        UpdateAvailable();
    }

    private void Update()
    {
        _dependencyTimer -= Time.deltaTime;
        if (_dependencyTimer <= 0)
        {
            _dependencyTimer = DependencyUpdateTime;
            UpdateAvailable();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _dependencies.ForEach(dep => dep.OnBuild += UpdateAvailable);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _dependencies.ForEach(dep => dep.OnBuild -= UpdateAvailable);
    }
    private void UpdateAvailable()
    {
        _dependenciesBuilt = 0;
        foreach (var dependency in _dependencies)
        {
            if (dependency.IsBuilt) _dependenciesBuilt++;
        }
        
        IsAvailable = !IsBuilt && _dependenciesBuilt == _dependencies.Count;

        Mockup.gameObject.SetActive(IsAvailable);
        OnAvailableUpdate?.Invoke(IsAvailable);
        if (Trigger != null) Trigger.gameObject.SetActive(IsAvailable);
    }

    public bool IsMoneyEnough()
    {
        return _player.Wallet.Money >= Price;
    }
    
    public bool TryBuild()
    {
        if (IsBuilt || !IsMoneyEnough()) return false;
        Build();
        return true;
    }
    
    [Button]
    private void Build(bool save = true)
    {
        _citizens.ForEach(c => c.SetActive(true));
        _player.Wallet.TryRemoveMoney(Price);
        _generator.AddMoneyPerSecond(Bonus);
        Object.transform.DOScale(_defaultScale, _buildTime);

        foreach (var mockupMaterial in Mockup.materials)
        {
            mockupMaterial.DOFade(0, _buildTime);
        }
        Mockup.transform.DOScale(Vector3.zero, _buildTime);

        if (save && !YandexGame.savesData.buildings.Contains(UUID))
        {
            YandexGame.savesData.buildings.Add(UUID);
            _saver.TrySave();   
            _soundController.PlaySound(SoundController.SoundType.Build);
        }

        IsBuilt = true;
        OnBuild?.Invoke();
    }

    [Button]
    private void CreateFromMesh()
    {
        var parent = new GameObject(name);
        parent.transform.parent = transform.parent;
        transform.parent = parent.transform;
    
        var trigger = GetComponentInChildren<BuildingTrigger>();
        trigger.transform.parent = parent.transform;
        var building = parent.AddComponent<Building>();
        building.Object = transform;
        building.Trigger = trigger;

        var mockup = Instantiate(gameObject, parent.transform);
        var mockupMaterial = Resources.Load<Material>("Mockup Material");
        building.Mockup = mockup.GetComponent<MeshRenderer>();
        building.Mockup.material = mockupMaterial;

        if (_children.Count > 0)
        {
            var childrenParent = new GameObject("Children");
            childrenParent.transform.parent = parent.transform;
            foreach (var child in _children)
            {
                child.transform.parent = childrenParent.transform;
                child.gameObject.AddComponent<BuildingChild>();
            }
        }

        name = "Object";
        mockup.name = "Mockup";
        trigger.name = "Trigger";
    
        DestroyImmediate(this);
    }

    [Button]
    private void InitTrigger()
    {
        Trigger = GetComponentInChildren<BuildingTrigger>();
    }
}