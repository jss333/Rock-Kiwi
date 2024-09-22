using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInformation : MonoBehaviour
{
    // Start is called before the first frame update


    private bool immune;
    private int minionCount;
    [Header("References")]
    [SerializeField] private MinionSpawnerController msController;
    private BossShield bossShield;
    void Start()
    {
        bossShield =  this.gameObject.GetComponentInChildren<BossShield>();
        minionCount = msController.entityCount;
        immune = true;
    }

    // Update is called once per frame
    public bool getImmune()
    {
        return immune;
    }
    public void setImmune(bool i)
    {
        immune = i;
    }
    public void minionDestroyed()
    {
        bossShield = FindFirstObjectByType<BossShield>();
        minionCount--;
        if(minionCount <= 0)
        {
            setImmune(false);
            bossShield.playShieldBreakAnimation();
            bossShield.playShieldBreakAnimation();
        }
    }
}
