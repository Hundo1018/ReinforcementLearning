using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning
{
    public class Memory
    {
        public State State0 { get; set; }
        public ActionSpace.Action Action { get; set; }
        public double Reward { get; set; }
        public State State1 { get; set; }
        public bool IsTerminal { get; set; }
        public Memory(State state0, ActionSpace.Action action, double reward, State state1)
        {
            State0 = state0;
            Action = action;
            Reward = reward;
            State1 = state1;
        }
        public Memory()
        {

        }
    }
}
