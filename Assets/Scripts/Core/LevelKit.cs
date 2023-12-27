using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "New LevelKit", menuName = "LevelKit", order = 51)]
    public class LevelKit : ScriptableObject
    {
        [field: SerializeField] public List<GameObject> levelsPrefabList { get; private set; }//префабы уровней
        [field: SerializeField] public List<int> stepsLevel { get; private set; }//количество шагов на уровень
    }
}

