using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PgUtility
{
    // public Pg Execute(Pg pg)
    // {
    //     return(pg.Execute());
    // }
}

public class PgPick : Pg
{
    public PgPick(Pg[] nodes)
    {
        this.nodes = nodes;
    }

    public override PgObject Execute()
    {
        return(nodes[Random.Range(0, nodes.Length)].Execute());
    }
}

public class PgConstruct : Pg
{
    private Pg parent;
    private Pg preFab;
    private string parentAnchor;
    private float angle;
    private Vector3 offset;

    public PgConstruct(Pg parent, string parentAnchor, Pg preFab, Vector3 offset, float angle)
    {
        this.parent = parent;
        this.preFab = preFab;
        this.parentAnchor = parentAnchor;
        this.angle = angle;
        this.offset = offset;
    }

    public override PgObject Execute()
    {
        PgObject parentObj = parent.Execute();
        PgObject preFabObj = preFab.Execute();

        Transform anchors = parentObj.GetObject().transform.Find("Anchors");

        Transform anchor = anchors.Find(parentAnchor);

        GameObject newGameObject = Object.Instantiate(preFabObj.GetObject(), parentObj.GetTransform());
        newGameObject.transform.position = anchor.transform.position + offset;
        newGameObject.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

        return(parentObj);
    }
}

public class PgRandomPlace : Pg
{
    private PgObject parent;
    private PgObject preFab;
    private string parentAnchor;
    private int n;
    private Vector3 offset;

    public PgRandomPlace(PgObject parent, string parentAnchor, PgObject preFab, int n)
    {
        this.offset = new Vector3();
        this.parent = parent;
        this.preFab = preFab;
        this.n = n;
        this.parentAnchor = parentAnchor;
    }

    public override PgObject Execute()
    {
        Transform anchors = parent.GetObject().transform.Find("Anchors");

        Transform anchor = anchors.Find(parentAnchor);

        for (int i = 0; i < n; i++)
        {
            offset.x = Random.Range(-6.0f, 6.0f);
            offset.y = 0.0f;
            offset.z = Random.Range(-6.0f, 6.0f);

            GameObject newGameObject = Object.Instantiate(preFab.GetObject(), parent.GetTransform());
            newGameObject.transform.position = anchor.transform.position + offset;
            newGameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        return(parent);
    }
}

public class PgCreate : Pg
{
    private PgObject preFab;
    private Vector3 position;
    private float angle;

    public PgCreate(PgObject preFab, Vector3 position, float angle) 
    {
        this.preFab = preFab;
        this.position = position;
        this.angle = angle;
    }

    public override PgObject Execute()
    {
        GameObject go = Object.Instantiate(preFab.GetObject(), position, Quaternion.Euler(0.0f, angle, 0.0f));

        return(new PgObject(go));
    }
}

public class PgObject
{
    public GameObject go;

    public PgObject(GameObject go)
    {
        this.go = go;
    }

    public GameObject GetObject()
    {
        return(go);
    }

    public Transform GetTransform()
    {
        return(go.transform);
    }
}

public abstract class Pg
{
    protected Pg[] nodes;

    public abstract PgObject Execute();
}
