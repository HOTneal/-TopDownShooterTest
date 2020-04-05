using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers.RespawnController
{
    public class RespawnController : MonoBehaviour
    {
        [SerializeField] private Transform[] m_SpawnPointsTeam1;
        [SerializeField] private Transform[] m_SpawnPointsTeam2;
        [SerializeField] private GameObject m_Player;
        [SerializeField] private GameObject m_Bot;

        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
            SortPoints(m_SpawnPointsTeam1, m_Bot);
            SortPoints(m_SpawnPointsTeam2, m_Player);
        }

        private void SortPoints(Transform[] points, GameObject unit)
        {
            for (int i = 0; i < points.Length; i++)
                SpawnUnit(points[i], unit);
        }
    
        private void SpawnUnit(Transform point, GameObject unitObj)
        {
            GameObject spawnedUnit = Instantiate(unitObj, point.position, Quaternion.identity) as GameObject;
            Unit.Unit unit = spawnedUnit.GetComponent<Unit.Unit>();

            unit.pointForSpawn = point;

            if (unit.isBot)
                unit.nickname = GenerateBotNickname();

            m_LinkManager.unitsHolder.AddUnitInHolder(unit);
        }
    
        public IEnumerator Respawn(Unit.Unit unit, float timeRespawn)
        {
            yield return new WaitForSeconds(timeRespawn);
            if (unit.isBot)
                SpawnUnit(unit.pointForSpawn, m_Bot);
            else
                SpawnUnit(unit.pointForSpawn, m_Player);
        }

        private string GenerateBotNickname()
        {
            TextAsset nicknamesBots = Resources.Load<TextAsset>("BotsName");
            string[] arrayNicknamesBots = nicknamesBots.ToString().Split('\n');
            var nickname = "Noname";
        
            for (int i = 0; i < arrayNicknamesBots.Length; i++)
            {
                if (m_LinkManager.unitsHolder.Units.Exists(unit => unit.nickname == arrayNicknamesBots[i])) continue;
                nickname = arrayNicknamesBots[i];
                break;
            }
            return nickname;
        }
    }
}