using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour{
    
    public SpriteRenderer spriteRenderer {get; private set;}
    public Sprite[] sprites = new Sprite[0];
    private float animationTime = 0.5f;
    public int animationFrame { get; private set; }
    public bool loop = true;

    private void Awake (){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start (){
        InvokeRepeating (nameof (Advance), animationTime, animationTime);
    }

    private void Advance(){
        //ist der Sprite Renderer deaktiviert, wird die Methode abgebrochen
        if (!spriteRenderer.enabled){
            return;
        }
        //animation Frame wird um eins erhöht
        animationFrame++;
        //ist das Animation Frame nicht mehr im array, wird es neu gestartet 
        if (animationFrame >= sprites.Length && loop){
            animationFrame = 0;
        }
        //ist das Animation Frame noch im Array, wird das Frame zur Animation aufgerufen
        if (animationFrame >=0 && animationFrame < sprites.Length){
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    public void Restart (){
        animationFrame = -1;
        Advance (); 
    }


}