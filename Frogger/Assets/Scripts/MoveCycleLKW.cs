using UnityEngine;

public class MoveCycleLKW : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float speed = 0.8f;
    public int size = 1;

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update()
    {
        // Prüft, ob das Objekt über den rechten Rand des Bildschrirms weg ist und teleportiert es in diesem Fall nach links
        if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
        {
            transform.position = new Vector3(leftEdge.x - size, transform.position.y, transform.position.z);
        }
        // Prüft, ob das Objekt über den linken Rand des Bildschrirms weg ist und teleportiert es in diesem Fall nach rechts
        else if (direction.x < 0 && (transform.position.x + size) < leftEdge.x)
        {
            transform.position = new Vector3(rightEdge.x + size, transform.position.y, transform.position.z);
        }
        // Bewegt das Objekt, wenn es sich auf dem Bildschirm befindet
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public void setSpeed(float nsp)
    {
        speed = nsp;
    }

}