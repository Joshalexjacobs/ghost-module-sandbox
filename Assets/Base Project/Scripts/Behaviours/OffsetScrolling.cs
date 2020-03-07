using System.Collections;
using System.Collections.Generic;
using CandyCoded;
using UnityEngine;

public class OffsetScrolling : MonoBehaviour {
    
    public float scrollSpeed;

    public bool isAnimated = false;
    public bool unscaled = false;

    public List<Texture> animatedTextures;
    public float frameLength = 0.2f;

    public bool useRandomScrollSpeed = false;
    
    [SerializeField]
    [RangedSlider(-1, 1)]
    public RangedFloat randomScrollSpeed;
    
    public bool useStaticPosition = false;

    private Renderer renderer;
    private Vector2 savedOffset;

    void Start () {
        renderer = GetComponent<Renderer> ();

        if (isAnimated) {
            StartCoroutine(AnimatePlane());
        }
        
        if (useRandomScrollSpeed) {
            scrollSpeed = randomScrollSpeed.Random();
        }
    }

    private IEnumerator AnimatePlane() {
        foreach (Texture texture in animatedTextures) {
            renderer.material.mainTexture = texture;
            yield return new WaitForSeconds(frameLength); 
        }

        StartCoroutine(AnimatePlane());
    }

    private float Scroll() {
        if (!unscaled) {
            return Mathf.Repeat (Time.time * scrollSpeed, 1);
        } else {
            return Mathf.Repeat (Time.unscaledTime * scrollSpeed, 1);
        }
    }
    
    void Update () {
        float x = Scroll();
        
        if (useStaticPosition) {
            x = Mathf.Repeat (scrollSpeed, 1);            
        }
        
        Vector2 offset = new Vector2 (x, 0);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
