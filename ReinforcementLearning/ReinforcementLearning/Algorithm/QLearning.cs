using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning.Algorithm
{
    public class QLearning : ILearningAlgorithm
    {
        private Dictionary<State, Dictionary<DiscreteActionSpace.Action, double>> _qTable;
        private double _learningRate;
        private double _discountFactor;
        /// <summary>
        /// 取得當前狀態下最大Q值的動作
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private DiscreteActionSpace.Action GetMaxAction(State state)
        {
            KeyValuePair<DiscreteActionSpace.Action, double> maxPair = _qTable[state].First();
            var actions = _qTable[state];
            foreach (var actionPair in actions)
                if (actionPair.Value >= maxPair.Value)
                    maxPair = actionPair;
            return maxPair.Key;
        }

        /// <summary>
        /// 取得當下狀態動作下的最大Q值
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private double GetMaxQ(State state)
        {
            if (_qTable.ContainsKey(state))
            {
                return _qTable[state][GetMaxAction(state)];
            }
            return 0;
        }

        /// <summary>
        /// 選取最大Q值的行動
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        DiscreteActionSpace.Action ILearningAlgorithm.ChooseAction(State currentState)
        {
            return GetMaxAction(currentState);
        }

        /// 依照策略選取行動
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="epsilon">有多少機率會歪出最高Q值以外的動作</param>
        /// <returns></returns>
        DiscreteActionSpace.Action ILearningAlgorithm.ChooseAction(State currentState, double epsilon)
        {
            double randomNumber = new Random().NextDouble();
            if (randomNumber < epsilon)
            {
                return (DiscreteActionSpace.Action)new Random().Next(0, DiscreteActionSpace.Length);
            }
            return GetMaxAction(currentState);
        }

        /// <summary>
        /// 學習
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="action"></param>
        /// <param name="nextState"></param>
        /// <param name="reward"></param>
        /// <returns></returns>
        double ILearningAlgorithm.Learn(State currentState, DiscreteActionSpace.Action action, State nextState, double reward, bool isTerminal)
        {
            double newQ;
            double evaluationQ = _qTable[currentState][action];
            double targetQ = _learningRate * (reward + _discountFactor * (isTerminal ? 1 : GetMaxQ(nextState)));
            newQ = (1 - _learningRate) * evaluationQ + targetQ;
            _qTable[currentState][action] = newQ;
            return newQ;
            /*
            double newQ;
            double evaluationQ = _qTable[currentState][action];
            double targetQ = reward + _discountFactor * (isTerminal ? 1 : GetMaxQ(nextState));
            newQ = evaluationQ + _learningRate * (targetQ - evaluationQ);
            return newQ;
            */
        }


        public QLearning()
        {
            _qTable = new Dictionary<State, Dictionary<DiscreteActionSpace.Action, double>>(new StateComparer());
        }
        public QLearning(double learningRate, double discountFactor) : this()
        {
            _learningRate = learningRate;
            _discountFactor = discountFactor;
        }

        public void SetEnvironment(int[,] maze, int xLimit, int yLimit, bool isRandomInit)
        {

            _qTable = new Dictionary<State, Dictionary<DiscreteActionSpace.Action, double>>(new StateComparer());
            Random random = new Random();
            DiscreteActionSpace.Action[] actions = DiscreteActionSpace.Values;

            for (int y = -1; y <= yLimit + 1; y++)
            {
                for (int x = -1; x <= xLimit + 1; x++)
                {
                    State state = new State(x, y);
                    Dictionary<DiscreteActionSpace.Action, double> acitonValues = new Dictionary<DiscreteActionSpace.Action, double>();
                    if (isRandomInit)
                        foreach (var action in actions)
                            acitonValues.Add(action, random.NextDouble());
                    else
                        foreach (var action in actions)
                            acitonValues.Add(action, 0);
                    _qTable.Add(state, acitonValues);
                }
            }
        }
    }
}
