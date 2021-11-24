using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning.Algorithm
{
    /// <summary>
    /// DQN 2015
    /// </summary>
    class NatureDQN : ILearningAlgorithm
    {
        public DiscreteActionSpace.Action ChooseAction(State currentState)
        {
            throw new NotImplementedException();
        }

        public DiscreteActionSpace.Action ChooseAction(State currentState, double epsilon)
        {
            throw new NotImplementedException();
        }

        public double Learn(State currentState, DiscreteActionSpace.Action action, State nextState, double reward, bool isTerminal)
        {
            throw new NotImplementedException();
        }
    }
}
