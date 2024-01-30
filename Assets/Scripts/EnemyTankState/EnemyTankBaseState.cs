using UnityEngine;

public abstract class EnemyTankBaseState
{
    public abstract void EnterState(EnemyTankStateManager enemyTank);
    public abstract void UpdateState(EnemyTankStateManager enemyTank);

    public abstract void UpdateState(EnemyTankStateManager enemyTank, Transform tower);
}
