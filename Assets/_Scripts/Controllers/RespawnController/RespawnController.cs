using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField] private Transform[] m_SpawnPointsTeam1;
    [SerializeField] private Transform[] m_SpawnPointsTeam2;
    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_Bot;

    private LinkManager m_LinkManager;

    private void Start()
    {
        SortPoints(m_SpawnPointsTeam1, m_Bot);
        SortPoints(m_SpawnPointsTeam2, m_Player);
        
        m_LinkManager = LinkManager.Instance;
    }

    private void SortPoints(Transform[] points, GameObject unit)
    {
        for (int i = 0; i < points.Length; i++)
            SpawnUnit(points[i], unit);
    }
    
    private void SpawnUnit(Transform point, GameObject unitObj)
    {
        GameObject spawnedUnit = Instantiate(unitObj, point.position, Quaternion.identity) as GameObject;
        Unit unit = spawnedUnit.GetComponent<Unit>();

        unit.m_PointForSpawn = point;

        if (unit.isBot)
            unit.m_Nickname = GenerateBotNickname();

        LinkManager.Instance.m_UnitsHolder.AddUnitInHolder(unit);
    }
    
    public IEnumerator Respawn(Unit unit, float timeRespawn)
    {
        yield return new WaitForSeconds(timeRespawn);
        if (unit.isBot)
            SpawnUnit(unit.m_PointForSpawn, m_Bot);
        else
            SpawnUnit(unit.m_PointForSpawn, m_Player);
    }

    private string GenerateBotNickname()
    {
        TextAsset nicknamesBots = Resources.Load<TextAsset>("BotsName");
        string[] arrayNicknamesBots = nicknamesBots.ToString().Split('\n');
        var nickname = "Noname";
        
        for (int i = 0; i < arrayNicknamesBots.Length; i++)
        {
            if (!LinkManager.Instance.m_UnitsHolder.m_Units.Exists(unit => unit.m_Nickname == arrayNicknamesBots[i]))
            {
                nickname = arrayNicknamesBots[i];
                break;
            }
        }
        return nickname;
    }
}