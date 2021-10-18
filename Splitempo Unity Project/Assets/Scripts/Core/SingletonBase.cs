using UnityEngine;
 
public abstract class SingletonBase<T> : MonoBehaviour
    where T : class
{
    /// <summary>
    /// SingletoneBase instance back field
    /// </summary>
    private static T instance = null;
    /// <summary>
    /// SingletoneBase instance
    /// </summary>
    public static T I
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                    Debug.LogError("SingletoneBase<T>: Could not found GameObject of type " + typeof(T).Name);
            }
            return instance;
        }
    }

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}