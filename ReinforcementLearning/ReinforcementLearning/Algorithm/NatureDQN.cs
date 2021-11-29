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
        /// <summary>
        /// 目標網路，
        /// </summary>
        INeuralNetwork targetNet;
        /// <summary>
        /// 評估網路、估計網路，計算當下狀態所獲得的各個動作的QValue
        ///  並且隨時更新
        /// </summary>
        INeuralNetwork evaluateNet;

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
