using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{

    [SerializeField]private PlayerStats stats;

    public PlayerStats PlayerStats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    private void Awake()
    {
        GetStats();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // This is Automaticly being called in Parent script Awake() There is another way See PlayerStats InitMethod
    protected override void GetStats()
    {
        PlayerStats = GetComponent<PlayerStats>();
    }

    protected override void OnCharacterDeath()
    {
        throw new System.NotImplementedException();
    }


}
