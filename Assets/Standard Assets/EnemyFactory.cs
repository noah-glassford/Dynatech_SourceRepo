using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    bool EnemyType();
    

}

public class HumanoidEnemy : IEnemy
{
    public bool EnemyType()
    {
        return false; //in enemy script false is human
    }
}

public class CanineEnemy : IEnemy
{
    public bool EnemyType()
    {
        return true; //in enemy script true is dog
    }
}

public enum EnemyType
{
    Humanoid,
    Canine
}


public class EnemyFactory : MonoBehaviour
{
    public static IEnemy GetEnemy(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Humanoid:
                return new HumanoidEnemy();
            case EnemyType.Canine:
                return new CanineEnemy();
            default:
                throw new System.Exception();
        }
    }
}
