using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour //�� ������ ��� ������������ ��� �������� ��� ������������ ������ �� �����, ����� ��� ����� ����� ��������
{
    [SerializeField] private Unit _player;
    [SerializeField] private Unit enemy1;
    [SerializeField] private Unit enemy2;
    [SerializeField] private Unit enemy3;
    //[SerializeField] private Unit _enemie;
    [SerializeField] private Map _map;
    [SerializeField] private Vector2Int _startPos;

    private void Start()
    {
        _map.unitsLayer[2, 2].Content = _player;
        _map.unitsLayer[2, 5].Content = enemy1;
        _map.unitsLayer[5, 2].Content = enemy2;
        _map.unitsLayer[5, 5].Content = enemy3;
        //_map.unitsLayer[2, 1].Content = _enemie;
    }
}
