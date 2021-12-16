using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level", order = 2)]
public class LevelInfo : ScriptableObject
{
    [SerializeField] int _numberOfCols;
    [SerializeField] int _numberOfRows;

    public int NumberOfCols => _numberOfCols;
    public int NumberOfRows => _numberOfRows;
}
