using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEntities : MonoBehaviour
{
    public enum Team { White, BLACK }

    public Team team;

    public Material white;
    public Material black;
    public bool staticColor = false;

    bool tWhite = false;

    public MeshRenderer[] mhRenders;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        mhRenders = GetComponentsInChildren<MeshRenderer>();
        mhRenders[0].material = team == Team.White ? white : black;
        tWhite = team == Team.White ? true : false;
        int i = 1;
        while(i < mhRenders.Length)
        {
            mhRenders[i].material = (i % 2 == 0 && tWhite) || (i % 2 == 1 && !tWhite) ? white : black;

            //Debug.Log("Name " + name + " Team " + team + " " + (i % 2 == 0));

            i++;
        }
    }

    public void ChangeColor()
    {
        if (staticColor) return;
        team = Team.BLACK == team ? Team.White : Team.BLACK;
    }
}
