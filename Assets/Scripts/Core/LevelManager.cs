using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelKit _levelKit;
        public int curentLevel { get; private set; }        
        public int totalLevel { get; private set; }
        private List<GameObject> _levels = new List<GameObject>();

        public int StartLevelManager()
        {
            if(_levels.Count > 0)
            {
                foreach(GameObject level in _levels)Destroy(level);
                _levels.Clear();
            }

            totalLevel = _levelKit.levelsPrefabList.Count-1;
            curentLevel = 0;

            for(int i = 0; i < _levelKit.levelsPrefabList.Count; i++)
            {
                GameObject level = Instantiate(_levelKit.levelsPrefabList[i]);
                level.SetActive(false);
                _levels.Add(level);

            }

            _levels[curentLevel].SetActive(true);
            return _levelKit.stepsLevel[curentLevel];
        }

        public int NextLevel()
        {
            
            if (curentLevel < totalLevel)
            {
                _levels[curentLevel].SetActive(false);
                curentLevel++;
                _levels[curentLevel].SetActive(true);
            }
            else
            {
                StartLevelManager();
                
            }
            
            return _levelKit.stepsLevel[curentLevel];
        }
    }
}

