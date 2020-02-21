using UnityEngine;

namespace Production.Scripts
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
    public class GameData : ScriptableObject
    {
        public bool NewGame = true;
        public int PatternIndex;
        
    }
}