using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnim : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void ResetAttack()
    {
        if(player != null)
        {
            player.ResetAttack();
        }
    }

    public void ResetThrow()
    {
        if(player != null)
        {
            player.ResetThrow();
        }
    }
}
