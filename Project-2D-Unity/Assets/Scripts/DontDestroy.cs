using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField]
    protected new string tag;
    // -------------------------------------------------------------------------
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
