using UnityEngine;

public static class GlobalHelper 
{
    //by this being static, this is going to be accesible for all scripts
    public static string GenerateUniqueID(GameObject obj)
    {
        return $"{obj.scene.name}_{obj.transform.position.x}_{obj.transform.position.y}"; //Chest_3_4
    }


}
