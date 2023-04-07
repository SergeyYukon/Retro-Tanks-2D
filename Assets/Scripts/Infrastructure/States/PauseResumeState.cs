using UnityEngine;

namespace Infrastructure.States
{
    public class PauseResumeState : IState
    {
        public void Enter()
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
