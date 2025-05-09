using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    [SerializeField] CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    [SerializeField] float additivePos = 2;
    enum Direction {Up, Down, Left, Right}

    // private void Awake()//will call as soon as the script es loaded
    // {
    //     confiner = Object.FindFirstObjectByType<CinemachineConfiner2D>(); 
    // }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundry;
            UpdatePlayer(collision.gameObject);
        }
    }

    private void UpdatePlayer(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y +=additivePos;
                break;
            case Direction.Down:
                newPos.y -=additivePos;
                break;
            case Direction.Left:
                newPos.x -=additivePos;
                break;
            case Direction.Right:
                newPos.x +=additivePos;
                break;
        }

        player.transform.position = newPos;
    }

}
