using UnityEngine;

public interface IStateBase
{
    public void Enter(Enemy enemy);
    public void Update(Enemy enemy);
    public void Exit(Enemy enemy);
}
