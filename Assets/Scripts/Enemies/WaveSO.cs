using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveSO : ScriptableObject
{
    public Enemy[] enemies;
    public int count;
    public float timeBetweenSpawns;
}
