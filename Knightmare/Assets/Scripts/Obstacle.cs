using UnityEngine;
using System.Collections;
public class Obstacle : MonoBehaviour
{
    // this script is original and was created by me
    // when the player collides with the obstacle object call the enemyattack method in gameplay

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GamePlay.Instance.EnemyAttack();
        }
    }
}
