using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
    [Header("Waypoints")]
    public GameObject[] waypoints;
    public float minDistance = 1f;
    public float speed = 1f;

    private int _index = 0;

        public override void Update()
        {
            base.Update();
            if(Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance)
            {
                _index++;
                if(_index >= waypoints.Length)
                {
                    _index = 0;
                }

            }

            Vector3 targetPosition = waypoints[_index].transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }




    
}
