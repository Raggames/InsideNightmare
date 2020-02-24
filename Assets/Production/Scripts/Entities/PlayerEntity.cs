using System.Collections;
using System.Collections.Generic;
using Production.Scripts;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{

    [SerializeField] private GameData _gameData;
    private OVRPlayerController _ovrPlayerController;

    private void Awake()
    {
        _ovrPlayerController = GetComponentInParent<OVRPlayerController>();
    }

    #region Data
    
    public void SavePlayerEntity(GameData gameData)
    {
        _gameData.playerPosition = _ovrPlayerController.gameObject.transform.position;
    }

    public void LoadPlayerEntity(GameData gameData)
    {
        _ovrPlayerController.transform.position = gameData.playerPosition;
    }
    
    #endregion

  
    
}
