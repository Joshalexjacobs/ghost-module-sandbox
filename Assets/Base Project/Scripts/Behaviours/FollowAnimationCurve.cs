using System.Collections;
using CandyCoded;
using UnityEngine;
using UnityEngine.Events;

public class FollowAnimationCurve : MonoBehaviour {

  public enum ScaleTypes {
    SCALE_ON_Z_ONLY,
    SCALE_ON_X_AND_Y
  }
  
  public float spawnDelay;
  public bool triggerOnCall = false;
  
  [Tooltip("Note, you currently cannot check destination with useLocalPosition.")]
  public bool useLocalPosition = false;
  
  [Tooltip("If left blank, this will be set to the current GameObject.")]
  public GameObject gameObjectToManipulate;
  
  [Header("Animation Curve")]
  public bool useAnimationCurve = true;
  public CandyCoded.Vector3AnimationCurve animationCurve;

  [Header("Rotation Curve")]
  public bool useRotationCurve = false;
  
  public CandyCoded.Vector3AnimationCurve rotationCurve;
  
  [Header("Scale Curve")]
  public bool useScaleCurve = false;

  public CandyCoded.Vector3AnimationCurve scaleCurve;
  public FollowAnimationCurve.ScaleTypes scaleTypes = ScaleTypes.SCALE_ON_Z_ONLY;
  
  [Tooltip("A UBHShot that is fired on start if populated.")]
  public UbhShotCtrl ubhShotCtrl;

  [Header("Events On Destination Reached")]
  public bool hasReachDestination = false;
  
  public UnityEvent events;

  private Vector3 destination;
  private float _initializationTime;
  
  private void Awake() {
    if (gameObjectToManipulate == null) {
      gameObjectToManipulate = this.gameObject;
    }

    CalculateDestination();
  }
  
  private void CalculateDestination() {
    if (useLocalPosition) {
      animationCurve.EditKeyframeValue(0, new Vector3(gameObjectToManipulate.transform.localPosition.x, gameObjectToManipulate.transform.localPosition.y, 0));      
    } else {
      animationCurve.EditKeyframeValue(0, new Vector3(gameObjectToManipulate.transform.position.x, gameObjectToManipulate.transform.position.y, 0));      
    }
    
    destination = animationCurve.Evaluate(animationCurve.MaxTime()); 
  }
  
  private IEnumerator Start() {
    if (triggerOnCall) {
      yield break;
    }
    
    yield return new WaitForSeconds(spawnDelay);

    if (ubhShotCtrl) {
      ubhShotCtrl.enabled = true;
    }

    _initializationTime = Time.timeSinceLevelLoad;

    if (useAnimationCurve) {
      StartCoroutine(Animate());
    }

    if (useRotationCurve) {
      StartCoroutine(AnimateRotationCurve());
    }
    
    if (useScaleCurve) {
      StartCoroutine(AnimateScaleCurve());
    }
  }

  public void ResetInitializationTime() {
    CalculateDestination();
    _initializationTime = Time.timeSinceLevelLoad;
  }

  private void EvaluatePosition(Vector3 newPosition) {
    if (useLocalPosition) {
      gameObjectToManipulate.transform.localPosition = newPosition;      
    } else {
      gameObjectToManipulate.transform.position = newPosition;
    }
  }

  public IEnumerator Animate() {
    EvaluatePosition(animationCurve.Evaluate(Time.timeSinceLevelLoad - _initializationTime));

    if (!useLocalPosition && gameObjectToManipulate.transform.position.Equals(destination) 
                          && events.GetPersistentEventCount() > 0) {
      events.Invoke();
    } else {
      yield return null;
      StartCoroutine(Animate()); 
    }
  }

  public IEnumerator AnimateRotationCurve() {
    Vector3 rotation = rotationCurve.Evaluate(Time.timeSinceLevelLoad - _initializationTime);
    gameObjectToManipulate.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    
    yield return null;
    StartCoroutine(AnimateRotationCurve());
  }

  public IEnumerator AnimateScaleCurve() {
    if (scaleTypes == ScaleTypes.SCALE_ON_Z_ONLY) {
      gameObjectToManipulate.transform.SetLocalScaleX(scaleCurve.Evaluate(Time.timeSinceLevelLoad - _initializationTime).z);
      gameObjectToManipulate.transform.SetLocalScaleY(scaleCurve.Evaluate(Time.timeSinceLevelLoad - _initializationTime).z);      
    } else {
      gameObjectToManipulate.transform.SetLocalScaleX(scaleCurve.Evaluate(Time.timeSinceLevelLoad - _initializationTime).x);
      gameObjectToManipulate.transform.SetLocalScaleY(scaleCurve.Evaluate(Time.timeSinceLevelLoad - _initializationTime).y);
    }

    yield return null;
    StartCoroutine(AnimateScaleCurve());
  }

  public void UpdateHasReachedDestination(bool hasReachedDestination) {
    this.hasReachDestination = hasReachedDestination;
  }

  public void DestroyGameObjectToManipulate() {
    Destroy(gameObjectToManipulate);
  }
  
  public void DestroyGameObject() {
    Destroy(gameObject);
  }
  
  public void DestroySelf() {
    Destroy(this);
  }
  
}
