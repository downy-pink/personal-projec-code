using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Actors;
using StateStructure;

public class CinemaMachineControl : MonoBehaviour
{
    [SerializeField] GameObject playable;
    [SerializeField] GameObject dollyTrack;
    [SerializeField] GameObject dollyCam;
    [SerializeField] GameObject boss;
   
    // Start is called before the first frame update
    void Start()
    {
        playable.active = false;
        dollyTrack.active = false;
        dollyCam.active = false;
        boss.GetComponent<StateMachine>().enabled = false;
    }

    bool isFlag;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerActor>() != null)
        {
            if(!isFlag)
            {
                isFlag = true;
                StartCoroutine(DoBossScene());
            }
        }
    }

    IEnumerator DoBossScene()
    {
        playable.active = true;
        dollyTrack.active = true;
        dollyCam.active = true;
        yield return new WaitForSeconds(10f);
        playable.active = false;
        dollyTrack.active = false;
        dollyCam.active = false;
        Camera.main.transform.position = new Vector3(0, 0, 0);
        Camera.main.transform.rotation = new Quaternion(0, 0, 0,0);
        boss.GetComponent<StateMachine>().enabled = true;
    }
}
