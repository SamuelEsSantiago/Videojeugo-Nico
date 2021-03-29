using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov3 : GnomeFov
{
    [SerializeField] private Transform center;
    [SerializeField] private float radius;
    private Vector2 endPoint;
    private float angle =0;
    private float speed=(2*Mathf.PI)/5 ;//2*PI in degress is 360, so you get 5 seconds to complete a circle
    private  float radiuss=5;


    private enum Direction
    {
        Clockwise, 
        Anticlockwise
    }
    private Direction direction;

    protected override void Move()
    {
        if (direction == Direction.Clockwise)
        {
            angle -= speed*Time.deltaTime;
        }
        else
        {
            angle += speed*Time.deltaTime;
        }
        endPoint.x = Mathf.Cos(angle)*radiuss+center.position.x;
        endPoint.y = Mathf.Sin(angle)*radiuss+center.position.y;
        mesh.transform.position = endPoint;
        Debug.DrawLine(center.position, endPoint, Color.red);
        return;
    }

    // Start is called before the first frame update
    new void Start()
    {
        direction = Direction.Clockwise;
        //base.Start();
    }
    
    new void Update()
    {
         //if you want to switch direction, use -= instead of +=
        if (touchingPlayer)
        {
            ChangeDirection();
        }
        Move();
    }

    private void ChangeDirection()
    {
        if (direction == Direction.Clockwise)
        {
            direction = Direction.Anticlockwise;
        }
        else
        {
            direction = Direction.Clockwise;
        }
    }
}
