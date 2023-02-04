using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{

    Vector3 pos;
    public float speed = 0.01f;
    float angle;
    public float RootSpeed;
    public bool boost = false;

    // References
    public GameObject BodyPrefab;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
        RotateRoot();

        if(Input.GetKeyDown("space")){
            boost = true;
        } else {
            boost = false;
        }
    }

    void GrowRoot(){
            GameObject body = Instantiate(BodyPrefab);
            Vector3 point = PositionsHistory[0];
            body.transform.position = transform.position;
            BodyParts.Add(body);
    }

    void FollowMouse()
    {
        pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, 0.0f, pos.z), speed * Time.deltaTime);
        if (!boost) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, 0.0f, pos.z), speed * Time.deltaTime);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, 0.0f, pos.z), 0.9f);
        }
        
        // Store position history
        PositionsHistory.Insert(0, transform.position);
        if (PositionsHistory.Count > 1) {
            if (PositionsHistory[0] != PositionsHistory[1]){
                GrowRoot();
            }
        }
    }

    void RotateRoot()
    {
        angle += Input.GetAxis("Mouse X") * RootSpeed * -Time.deltaTime;
        angle = Mathf.Clamp(angle, -90, 270);
        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

}
