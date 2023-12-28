using System;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class PathWalker : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private float _speed = 3;
    [SerializeField] private bool _cyclicPath = false;

    [Foldout("Adnvanced")]
    [SerializeField] private float _spawnTime = 0.3f;
    [Foldout("Adnvanced")]
    [SerializeField] private const float _reachDistance = 0.1f;

    private int _pointId = 0;
    private Vector3 _defaultScale;
    
    private void Start()
    {
        _defaultScale = transform.localScale;
        Spawn();
    }

    private void Update()
    {
        if (_points.Count == 0) return;
        
        transform.position = Vector3.MoveTowards(
                transform.position, 
                _points[_pointId].position, 
            _speed * Time.deltaTime);
        
        transform.LookAt(_points[_pointId].position);
        
        
        if (Vector3.Distance(transform.position, _points[_pointId].position) < _reachDistance)
        {
            _pointId++;

            if (_pointId >= _points.Count)
            {
                Respawn();
            }
        }
    }

    private void Respawn()
    {
        _pointId = 0;
        
        if (_cyclicPath) return;
        transform.DOScale(Vector3.zero, _spawnTime).OnComplete(Spawn);
    }

    private void Spawn()
    {
        if (_points.Count > 0)
        {
            transform.position = _points[0].position;   
        }
        transform.localScale = Vector3.zero;
        transform.DOScale(_defaultScale, _spawnTime);
    }
}