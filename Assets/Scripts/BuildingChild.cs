using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BuildingChild : MonoBehaviour
{
    [ReadOnly] 
    [SerializeField] private Building _mainBuilding;
    
    public MeshRenderer Mockup;
    
    [Foldout("Additional")]
    [SerializeField] private float _buildTime = 0.7f;
    
    private Vector3 _defaultScale;

    private void Awake()
    {
        _defaultScale = transform.localScale;
        transform.localScale = Vector3.zero;
        UpdateAvailable(false);
    }

    private void OnValidate()
    {
        _mainBuilding = GetComponentInParent<Building>();
        if (Mockup == null)
        {
            GenerateMockup();
        }
    }

    private void OnEnable()
    {
        if (_mainBuilding == null) return;
        _mainBuilding.OnBuild += Build;
        _mainBuilding.OnAvailableUpdate += UpdateAvailable;
    }

    private void OnDisable()
    {
        if (_mainBuilding == null) return;
        _mainBuilding.OnBuild -= Build;
        _mainBuilding.OnAvailableUpdate -= UpdateAvailable;
    }

    private void UpdateAvailable(bool isAvailable)
    {
        Mockup.gameObject.SetActive(isAvailable);
    }
    
    [Button]
    private void Build()
    {
        transform.DOScale(_defaultScale, _buildTime);
        foreach (var mockupMaterial in Mockup.materials)
        {
            mockupMaterial.DOFade(0, _buildTime);
        }
        Mockup.transform.DOScale(Vector3.zero, _buildTime);
    }

    [Button]
    private void GenerateMockup()
    {
        var mockup = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
        mockup.name = $"[Mockup] {name}";
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () =>
        {
            DestroyImmediate(mockup.GetComponent<BuildingChild>());
        };
#endif

        var mesh = mockup.GetComponent<MeshRenderer>();
        mesh.material = Resources.Load<Material>("Mockup Material");;

        Mockup = mesh;
    }
}