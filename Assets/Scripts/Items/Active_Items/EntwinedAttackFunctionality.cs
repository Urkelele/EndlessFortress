using UnityEngine;

public class EntwinedAttackFunctionality : ItemFunctionality
{
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;
        

        return true;

    }
}
