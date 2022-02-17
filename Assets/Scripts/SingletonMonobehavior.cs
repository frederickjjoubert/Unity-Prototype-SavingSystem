using UnityEngine;

public abstract class SingletonMonobehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        Debug.Log("SingletonMonobehavior Awake");
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}