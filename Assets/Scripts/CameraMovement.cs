using UnityEngine;

public class CameraMovement : MonoBehaviour
{



    [SerializeField] private Transform player;
    [SerializeField] private float speedCamera;

    private Vector3 toPosition;
    private new Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.transform;
        camera.position = new Vector3(player.position.x, player.position.y, camera.position.z);


    }

    void FixedUpdate()
    {

        toPosition = player.position;
        toPosition.z = camera.position.z;
        camera.position = Vector3.Lerp(camera.position, toPosition, speedCamera * Time.deltaTime);
    }
}
