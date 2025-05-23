using UnityEngine;

public class BaseActiveScript : ItemBaseScript
{
    public virtual void UseActive()
    {
        Debug.Log(this.name + " ACTIVATED");
    }
}
