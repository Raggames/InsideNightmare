using System.Collections.Generic;
using UnityEngine;

namespace Production.Scripts.Entities
{
    public class SequencerEntity : MonoBehaviour
    {
        
        
        [Header("Data")] public GameData GameData;
        
        public List<PatternEntity> PatternEntities = new List<PatternEntity>();
        [SerializeField] private int PatternIndex;
        public bool SequenceIsComplete;
        private void Start()
        {
            if (GameData.NewGame) PatternIndex = 0; 
            InitializePatterns();
        }

        #region Data

        public void SaveSequencerEntity(GameData gameData)
        {
            GameData.PatternIndex = PatternIndex;
        }

        public void LoadSequencerEntity(GameData gameData)
        {
           PatternIndex = gameData.PatternIndex;
        }
        
        #endregion
        
        
        void InitializePatterns()
        {
            for (int i = 0; i < PatternEntities.Count; i++)
            {
                if (i == PatternIndex)
                {
                    PatternEntities[i].ActivatePattern();
                }
                else
                {
                    PatternEntities[i].DesactivatePattern();
                }
            }
        }
        
        public void ComputePatterns()
        {
            if (!SequenceIsComplete)
            {
                Debug.Log("ComputePatterns");
                bool patternIsComplished = false;
                foreach (var LogObject in PatternEntities[PatternIndex].InteractableObjects)
                {
                    if (LogObject.interactableObject.InteractionIsDone)
                    {
                        patternIsComplished = true;
                        
                    }
                    if(LogObject.interactableObject.InteractionIsDone==false)
                    {
                        patternIsComplished = false;
                        break;
                    }
                }

                if (patternIsComplished)
                {
                    PatternEntities[PatternIndex].DesactivatePattern();
                    PatternEntities[PatternIndex].PatternIsDone = true;
                    
                    if(PatternIndex +1 == PatternEntities.Count) SequenceIsComplete = true;
                    else GameData.PatternIndex++;
                    
                    PatternIndex = GameData.PatternIndex;
                    PatternEntities[PatternIndex].ActivatePattern();
                }
              
            }
            
        }
      
    }
}