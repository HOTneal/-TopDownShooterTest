using System.Collections;
using Managers;
using Unit;
using UnityEngine;

namespace Controllers.RespawnController
{
    public class RespawnController : MonoBehaviour
    {
        [SerializeField] private Transform[] m_SpawnPointsTeam1;
        [SerializeField] private Transform[] m_SpawnPointsTeam2;

        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
            SortPoints(m_SpawnPointsTeam1, "Bot");
            SortPoints(m_SpawnPointsTeam2, "Player");
        }

        private void SortPoints(Transform[] points, string typeUnit)
        {
            for (int i = 0; i < points.Length; i++)
                SpawnUnit(points[i], TypeUnit(typeUnit));
        }

        private UnitController TypeUnit(string type)
        {
            UnitController unitFromPool = null;
            switch (type)
            {
                case "Player":
                    m_LinkManager.playerPool.CheckBulletsInPool();
                    unitFromPool = m_LinkManager.playerPool.objInPool.Dequeue();
                    break;

                case "Bot":
                    m_LinkManager.botsPool.CheckBulletsInPool();
                    unitFromPool = m_LinkManager.botsPool.objInPool.Dequeue();
                    break;
            }

            return unitFromPool;
        }

        private void SpawnUnit(Transform point, UnitController unit)
        {
            unit.transform.position = point.position;
            unit.pointForSpawn = point;

            if (unit.isBot)
                unit.nickname = GenerateBotNickname();

            m_LinkManager.unitsHolder.AddUnitInHolder(unit);
            unit.gameObject.SetActive(true);
        }
    
        public IEnumerator Respawn(Unit.UnitController unit, float timeRespawn)
        {
            yield return new WaitForSeconds(timeRespawn);
            if (unit.isBot)
                SpawnUnit(unit.pointForSpawn, unit);
            else
                SpawnUnit(unit.pointForSpawn, unit);
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