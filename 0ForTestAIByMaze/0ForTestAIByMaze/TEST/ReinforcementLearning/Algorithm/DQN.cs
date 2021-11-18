using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning.Algorithm
{
    class DQN : ILearningAlgorithm
    {
        public ActionSpace.Action ChooseAction(State currentState)
        {
            throw new NotImplementedException();
        }

        public ActionSpace.Action ChooseAction(State currentState, double epsilon)
        {
            throw new NotImplementedException();
        }

        public double Learn(State currentState, ActionSpace.Action action, State nextState, double reward, bool isTerminal)
        {
            throw new NotImplementedException();
        }
    }
}
