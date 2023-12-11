using UnityEngine; 
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

[RequireComponent(typeof(SpriteRenderer))]
public class player : MonoBehaviour {

    public SpriteRenderer spriteRenderer {get; private set;}
    
    public Sprite idleSprite;
    public Sprite leapSprite;
    public Sprite deadSprite; 
                                          
    private Vector3 spawnPosition;
    private float farthestRow;
    private bool cooldown;

    public GameManager gm;
    public GameObject et;

    private Animator anim;
                                        
private void Awake (){
    //startet das Spiel u. spawnt den Frosch und speichert die Spawn Position
    spriteRenderer = GetComponent<SpriteRenderer>(); 
    spawnPosition = transform.position;
    gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    
}

private void Update (){
    //Movement
    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
        transform.rotation = Quaternion.Euler(0f,0f,0f);
        Move(Vector3.up); 
    }
    else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
        transform.rotation = Quaternion.Euler(0f,0f,90f);
        Move(Vector3.left); 
    }
    else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
        transform.rotation = Quaternion.Euler(0f,0f,-90f);
        Move(Vector3.right); 
    }
    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
        transform.rotation = Quaternion.Euler(0f,0f,180f);
        Move(Vector3.down); 
    }
    
}
public void Move (Vector3 direction)
    {   
        // Prüft, ob der Frosch bereits in der Luft ist
        if (cooldown) {
            return;
        }

        //berechnet die neue Position
        Vector3 destination = transform.position + direction;

        // Prüft, ob am Zielort eine Kollision mit einem anderem Gegenstand vorliegt
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));

        // Passiert eine Kollision mit einer Barriere, wird die Methode nicht ausgeführt
        if (barrier != null) {
            return;
        }

        //existiert eine Plattform, wird sie als als neues "Parent" gesetzt => der Frosch bewegt sich jetzt "auf"/mit der Platform 
        if (platform != null) {
            transform.SetParent(platform.transform);
        } else {
            transform.SetParent(null);
        }

        //Collidet der Frosch mit einem Hinderniss und befindet er sich nicht auf einer Plattform, stirbt er => Wasser ist auch Hinderniss 
        if (obstacle != null && platform == null)
        {
            transform.position = destination;
            Death();
        }
        // Collidet der Frosch mit nichts, bewegt sich der Frosch zum Zielort
        else
        {
            
            // Prüft ob vorgerückt wurde und updatet in diesem Fall die "farthest - Row" Variable
            if (destination.y > farthestRow)
            {  
                farthestRow = destination.y;
                
                gm.AdvancedRow();
            }  

            // Sprung zum Ziel wird durchgeführt 
            
            StopAllCoroutines();
            StartCoroutine(Leap(destination));
        }
    }

private IEnumerator Leap (Vector3 destination)
    {   
        //legt die Startposition fest 
        Vector3 startPosition = transform.position;

        float elapsed = 0f;
        float duration = 0.125f;

        // startet die Animation und setzt den Cooldown für den Sprung auf "true"
        cooldown = true;
        anim.SetBool("Leaping", true);
        //während die Bewegung nicht zuende ist, bewegt er sich auf das Ziel zu  
        while (elapsed < duration)
        { 
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Animation wird zu Idle zurückgesetzt, eine neue Position wird festgelegt und der Cooldown wird resetet
        transform.position = destination;
        anim.SetBool("Leaping", false);
        cooldown = false;
    }



 public void Respawn ()
 {
    //stoppt alle Vorgänge
    StopAllCoroutines();
    
    //Setzt Rotation zurück => Frosch schaut immer nach oben
    transform.rotation = Quaternion.identity;
    transform.position = spawnPosition; 
    farthestRow = spawnPosition.y; 
    
    //setzt die Animation zurück
    spriteRenderer.sprite = idleSprite; 
    
    //gibt die Kontrolle wieder frei
    gameObject.SetActive(true);
    enabled = true;
    cooldown = false;    
    }

public void Death ()
{
//stoppt alle Bewegungen
StopAllCoroutines();

//nimmt die Kontrolle
enabled = false; 

//Wechselt zur "tot" Animation und setzt die Rotation auf null => Frosch schaut nach oben
transform.rotation=Quaternion.identity;
anim.SetBool("Dead", true);

//ruft im GameManager eine Methode auf, um die "Game Over Phase" zu starten
gm.Died();
}

private void OnTriggerEnter2D (Collider2D other) 
{ 
  //sorgt dafür dass auch bei Collision nachdem der Frosch stillsteht der Tod eintreten kann
  bool hitObstacle = other.gameObject.layer == LayerMask.NameToLayer("Obstacle");
  bool onPlatform = transform.parent != null; 
  bool endlessTrigger = other.gameObject.layer == LayerMask.NameToLayer("TriggerEndless");

        if (enabled && hitObstacle && !onPlatform) {
        Death();
  }
  //wenn der Frosch den unsichtbaren Collider trifft, ruft er die Methode aus dem Game Manager auf
  if (endlessTrigger == true)
        {
            gm.Endless();
            gm.deleteTrigger(other);
        }
        

  }
}

