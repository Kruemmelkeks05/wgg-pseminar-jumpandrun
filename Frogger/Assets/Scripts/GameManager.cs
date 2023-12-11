using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

  private player player;

  public GameObject gameOverMenu;
  public Text scoreText;
    private GameObject[] tilemaps;

  private int score;
  private int counter;

  public GameObject Tilemap;

  private float respawnTime;

  public MoveCycleCar1 mcCar1;
  public MoveCycleCar2 mcCar2;
  public MoveCycleCar3 mcCar3;
  public MoveCycleLKW mcLKW;
  public MoveCycleTurtle mcTurtle;
  public MoveCycleLogs mcLogs;

    private void Awake(){
    Application.targetFrameRate = 60;

    player = FindObjectOfType<player>();
        mcCar1 = FindObjectOfType<MoveCycleCar1>();
        mcCar2 = FindObjectOfType<MoveCycleCar2>();
        mcCar3 = FindObjectOfType<MoveCycleCar3>();
        mcLKW = FindObjectOfType <MoveCycleLKW> ();
        mcTurtle = FindObjectOfType<MoveCycleTurtle>();
        mcLogs = FindObjectOfType<MoveCycleLogs>();  

  }

  //starten des Spiels
  private void Start(){
     tilemaps = new GameObject[100];
     NewGame();     
    //InvokeRepeating("checkScore", 0, 1.0f);
  }
  
  //checkt, ob der Score über den jeweiligen Werten liegt und ruft dann eine entsprechende Methode auf
   /*private void checkScore()
    {
        if (score == 100)
        {
            score100();
        }
        if (score == 200)
        {
            score200();
        }
        if (score == 400)
        {
            score400();
        }
        if (score == 800)
        {
            score800();
        }
        if (score == 1000)
        {
            score1000();
        }
    } */

  //aktivieren des neuen Spiels
  public void NewGame(){
        for (int i = 3; i <= counter; i++)
        {
            Destroy(tilemaps[i]);
            Debug.Log("Tilemap deleted");
        }
    gameOverMenu.SetActive(false);
    SetScore(0);
    counter = 1;
    player.Respawn();
     
      
  }
    
  
  //respawnen des Frosches
  private void Respawn()
    {
        player.Respawn();

        StopAllCoroutines();

       

    }

 

  //sterben des Frosches
  public void Died()
    { 
       Invoke(nameof(GameOver), 1f);        
    }
  
   //GamerOver Bild
   public void GameOver()
    {
        player.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(CheckForPlayAgain());
    }

   
   //neues Game starten?
   private IEnumerator CheckForPlayAgain()
    {
        bool playAgain = false;

        while (!playAgain)
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                playAgain = true;
            }

            yield return null;
        }

        NewGame();
    }
  

  //Score erhöhen
  public void AdvancedRow()
    {
        SetScore(score + 10);
    }

  //Score anzeigen
  private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    public void Endless()
    {
        GameObject oberLay = Instantiate(Tilemap, new Vector3(-15, 17 + 14 * counter, 0), Quaternion.identity);
        counter++;
        oberLay.transform.SetParent(Tilemap.transform.parent);
        tilemaps[counter] = oberLay;
        Debug.Log("addedTilemap to array");
        if (counter > 2)
        {
            Destroy(tilemaps[counter - 3]);
        }

    }   
        
  public void deleteTrigger (Collider2D go) 
  {
      if (counter > 2)       
            {
                Destroy(go);
            }
  }

    

    
    
    //Methoden, die entsprechend des Scores schnellere Geschwinigkeiten für die einzelnen Autos setzen
    //=> Problem: Autos sind Prefabs die die Werte ihrer Variablen aus dem Prefab ziehen, was immer dort am Anfang drinsteht kann nicht so einfach verändert werden (hab auf jeden Fall keine Lösung gefunden)
    /* 
    private void score100 ()
    {
        mcCar1.setSpeed(2f);
        mcCar2.setSpeed(1.75f);
        mcCar3.setSpeed(1.9f);
        mcLKW.setSpeed(1.5f);
        mcLogs.setSpeed(1.75f);
        mcTurtle.setSpeed(2.5f);
        Debug.Log("Changend Values for 100");
    }

    private void score200()
    {
        mcCar1.setSpeed(2.5f);
        mcCar2.setSpeed(2.25f);
        mcCar3.setSpeed(2.3f);
        mcLKW.setSpeed(2f);
        mcLogs.setSpeed(2.25f);
        mcTurtle.setSpeed(3f);
    }

    private void score400()
    {
        mcCar1.setSpeed(3f);
        mcCar2.setSpeed(2.75f);
        mcCar3.setSpeed(2.8f);
        mcLKW.setSpeed(2.5f);
        mcLogs.setSpeed(2.75f);
        mcTurtle.setSpeed(3.5f);
    }

    private void score800()
    {
        mcCar1.setSpeed(3.5f);
        mcCar2.setSpeed(3.25f);
        mcCar3.setSpeed(3.2f);
        mcLKW.setSpeed(3f);
        mcLogs.setSpeed(3.25f);
        mcTurtle.setSpeed(4f);
    }

    private void score1000()
    {
        mcCar1.setSpeed(4f);
        mcCar2.setSpeed(3.75f);
        mcCar3.setSpeed(3.7f);
        mcLKW.setSpeed(3.5f);
        mcLogs.setSpeed(3.75f);
        mcTurtle.setSpeed(4.25f);
    } 
    */
     
}


  

