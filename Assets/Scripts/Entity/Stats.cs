using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
[System.Serializable]
public class Stats : ScriptableObject {

    public int maxHp;

    public bool isImmortal;

    public float moveSpeed;

    public float jumpStr;
}
