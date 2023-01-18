using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pg : MonoBehaviour
{
    public abstract GameObject Execute();

    protected GameObject Create(GameObject go) 
    {
        return(Instantiate(go));
    }
}

public class PgPick : Pg
{
    private GameObject[] list;

    public PgPick(GameObject[] list)
    {
        this.list = list;
    }

    public override GameObject Execute()
    {
        return(list[Random.Range(0, list.Length)]);
    }
}

public class PgCreate : Pg
{
    private GameObject prefab;

    public PgCreate(GameObject prefab) 
    {
        this.prefab = prefab;
    }

    public override GameObject Execute()
    {
        return(Create(prefab));
    }
}
