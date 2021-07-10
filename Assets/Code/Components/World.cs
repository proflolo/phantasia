using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class World : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Serializable] class NumcirclesChangedEvent : UnityEvent<int> { }
    [SerializeField] NumcirclesChangedEvent sig_circlesChanged;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNumCirclesChanged(CircularMovement i_circularMovement, int i_numCircles)
    {
        sig_circlesChanged.Invoke(i_numCircles);
    }

    public void RequestAddXP(int i_xp)
    {
        Game.instance.AddXP(10);   
    }
}
