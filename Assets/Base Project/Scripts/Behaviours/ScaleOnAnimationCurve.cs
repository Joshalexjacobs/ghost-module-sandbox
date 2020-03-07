using System;
using System.Collections;
using UnityEngine;

public class ScaleOnAnimationCurve : MonoBehaviour {
  
  public CandyCoded.Vector3AnimationCurve scaleCurve;
  public FollowAnimationCurve.ScaleTypes scaleTypes = FollowAnimationCurve.ScaleTypes.SCALE_ON_Z_ONLY;

  public bool unscaled = false;
  
  private float _timeAwake = 0f;
  private float _initializationTime = 0f;

  private PauseUtil _pauseUtil;

  private void Awake() {
    _pauseUtil = FindObjectOfType<PauseUtil>();
  }

  private void Start() {
    _timeAwake = Time.timeSinceLevelLoad;
    _initializationTime = Time.timeSinceLevelLoad;
    
    StartCoroutine(AnimateScaleCurve());
  }
  
  public IEnumerator AnimateScaleCurve() {
    if (scaleTypes == FollowAnimationCurve.ScaleTypes.SCALE_ON_Z_ONLY) {
      float z = scaleCurve.Evaluate(_initializationTime - _timeAwake).z;
      transform.SetLocalScaleX(z);
      transform.SetLocalScaleY(z);      
    } else {
      Vector2 vector2 = scaleCurve.Evaluate(_initializationTime - _timeAwake);
      transform.SetLocalScaleX(vector2.x);
      transform.SetLocalScaleY(vector2.y);
    }

    yield return null;
    StartCoroutine(AnimateScaleCurve());
  }

  private void Update() {
    if (!_pauseUtil.IsPaused) {
      if (unscaled) {
        _initializationTime += Time.unscaledDeltaTime;        
      } else {
        _initializationTime += Time.deltaTime;
      }
    }
  }
  
}
