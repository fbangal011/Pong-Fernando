using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.name.Equals("WallLeft"))
        {
            GameManager.Instance.AddPoints(2);
        }

        if (this.name.Equals("WallRight"))
        {
            GameManager.Instance.AddPoints(1);
        }
    }
}
