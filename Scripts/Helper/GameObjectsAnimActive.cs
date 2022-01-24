using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsAnimActive : MonoBehaviour
{
    [SerializeField] GameObject[] objs;
    // Start is called before the first frame update
    void Start()
    {
        DeactiveObjs();
    }

    public void ActiveObjs()
    {
        for (int i = 0; i < objs.Length; ++i)
            objs[i].SetActive(true);
    }

    public void DeactiveObjs()
    {
        for (int i = 0; i < objs.Length; ++i)
            objs[i].SetActive(false);
    }
}
