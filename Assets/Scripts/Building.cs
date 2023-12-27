using System;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using Triggers;
using UnityEditor;
using UnityEngine;
using Zenject;

[CanEditMultipleObjects]
public class Building : MonoBehaviour
{
    [Inject] private MoneyGenerator _generator;
    [Inject] private Player _player;
    
    public string Name = "Дом";
    public int Price = 0;
    public float Bonus = 1;
    
    public Transform Object;
    public MeshRenderer Mockup;
    private BuildingTrigger _trigger;

    public Action OnBuild;

    [SerializeField] private List<Building> _dependencies;

    [Foldout("Advanced")]
    [SerializeField] private float _buildTime = 0.7f;

    private int _dependenciesBuilt = 0;
    private Vector3 _defaultScale;
    
    private void Awake()
    {
        _trigger = GetComponentInChildren<BuildingTrigger>();
        _trigger.Initialize(this);
        
        _defaultScale = Object.localScale;
        Object.localScale = Vector3.zero;
        UpdateAvailable();
    }

    private void OnEnable()
    {
        _dependencies.ForEach(dep => dep.OnBuild += OnDependencyBuild);
    }

    private void OnDisable()
    {
        _dependencies.ForEach(dep => dep.OnBuild -= OnDependencyBuild);
    }

    private void OnDependencyBuild()
    {
        _dependenciesBuilt++;
        UpdateAvailable();
    }

    private void UpdateAvailable()
    {
        bool isAvailable = _dependenciesBuilt == _dependencies.Count;
        
        Mockup.gameObject.SetActive(isAvailable);
        if (_trigger != null) _trigger.gameObject.SetActive(isAvailable);
    }

    public bool IsMoneyEnough()
    {
        return _player.Wallet.Money >= Price;
    }
    
    public bool TryBuild()
    {
        if (!IsMoneyEnough()) return false;
        Build();
        return true;
    }
    
    [Button]
    private void Build()
    {
        _player.Wallet.TryRemoveMoney(Price);
        _generator.AddMoneyPerSecond(Bonus);
        Object.transform.DOScale(_defaultScale, _buildTime);
        Mockup.material.DOFade(0, _buildTime);
        
        OnBuild?.Invoke();
    }

    // [Button]
    // private void CreateBuildingFromMesh()
    // {
    //     var parent = new GameObject(name);
    //     parent.transform.parent = transform.parent;
    //     transform.parent = parent.transform;
    //
    //     var trigger = GetComponentInChildren<BuildingTrigger>().gameObject;
    //     trigger.transform.parent = parent.transform;
    //
    //     var building = parent.AddComponent<Building>();
    //     building.Object = transform;
    //
    //     var mockup = Instantiate(gameObject, parent.transform);
    //     building.Mockup = mockup.GetComponent<MeshRenderer>();
    //     building.Mockup.material = _mockupMaterial;
    //
    //     name = "Object";
    //     mockup.name = "Mockup";
    //     trigger.name = "Trigger";
    //
    //     DestroyImmediate(this);
    // }
}