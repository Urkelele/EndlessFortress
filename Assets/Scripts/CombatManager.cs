using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] PlayerCombatScript m_PlayerCharacter;
    public Transform m_CharacterPosition;
    public List<EnemyBaseScript> m_Enemies;
    [SerializeField] Transform[] m_EnemyPositions = new Transform[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
