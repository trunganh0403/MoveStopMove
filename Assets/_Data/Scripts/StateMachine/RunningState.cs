using UnityEngine;

public class RunningState : IState<Character>
{
    public void OnEnter(Character t)
    {

    }

    public void OnExecute(Character t)
    {
        t.PlayerIdle(t);
        t.BotIdle(t);

    }

    public void OnExit(Character t)
    {

    }
}

