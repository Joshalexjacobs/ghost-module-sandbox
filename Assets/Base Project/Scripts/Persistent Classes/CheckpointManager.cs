using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

  public EnemyWave currentWave;
  public int currentCheckpoint = 0;

  private void Awake() {
    if (FindObjectsOfType<CheckpointManager>().Length > 1) {
      Destroy(gameObject);
    }
    
    DontDestroyOnLoad(gameObject);
  }

  public void NewCheckpointReached() {
    currentCheckpoint++;
  }

  public void UpdateCurrentWave(EnemyWave newWave) {
    currentWave = newWave;
  }

}
