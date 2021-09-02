using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomly : MonoBehaviour
{
    public float timer;
    public int newTarget;
    public float speed;
    public UnityEngine.AI.NavMeshAgent nav;

    public Vector3 Target;





    // Start is called before the first frame update
    void Start()
    {
        nav = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += timer.deltaTime;
        nav.speed = speed;
        if (timer >= newTarget){
            newTarget();
            timer = 0;
        }
    }
    
    void newTarget(){
        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;

        float xPos = myX + MoveRandomly.Range(myX - 100, myX + 100);
        float zPos = myZ + MoveRandomly.Range(myZ - 100, myZ + 100);

        Target = new Vector3(xPos, gameObject.transform.position.y, zPos);

        nav.SetDestination(Target);
    }
}
