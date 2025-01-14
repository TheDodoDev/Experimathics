using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] PhysicMaterial slipperyMat, floorMat;
    [SerializeField] AcromathicsManager acromathicsManager;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < 3.4)
        {
            gameObject.GetComponent<BoxCollider>().material = slipperyMat;
        }
        else
        {
            gameObject.GetComponent <BoxCollider>().material = floorMat;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Incorrect")
        {
            Destroy(gameObject);
        }
    }
}
