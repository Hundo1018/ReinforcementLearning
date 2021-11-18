using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning.Algorithm
{
    public class QLearning : ILearningAlgorithm
    {
        private Dictionary<State, Dictionary<ActionSpace.Action, double>> _qTable;
        private double _learningRate;
        private double _discountFactor;
        /// <summary>
        /// 取得當前狀態下最大Q值的動作
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private ActionSpace.Action GetMaxAction(State state)
        {
            KeyValuePair<ActionSpace.Action, double> maxPair = _qTable[state].First();
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
        ActionSpace.Action ILearningAlgorithm.ChooseAction(State currentState)
        {
            return GetMaxAction(currentState);
        }

        /// 依照策略選取行動
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="epsilon">有多少機率會歪出最高Q值以外的動作</param>
        /// <returns></returns>
        ActionSpace.Action ILearningAlgorithm.ChooseAction(State currentState, double epsilon)
        {
            double randomNumber = new Random().NextDouble();
            if (randomNumber < epsilon)
            {
                return (ActionSpace.Action)new Random().Next(0, ActionSpace.Length);
            }
            return GetMaxAction(currentState);

            //以下撰寫非0非1的時候如何處理


            /*
            //取得動作list
            List<KeyValuePair<ActionSpace.Action, double>> actions = _qTable[currentState].ToList();
            //基礎機率是平均的
            double baseProb = 1d / actions.Count;

            0的話，會讓最大的數值放到滿
            1的話，會讓所有數值等於0.25
            //排序

            //0.5 * x *(1- y) = 1
            //

            actions.Sort((x, y) => { return (x.Value - y.Value) < 0 ? -1 : 1; });
            actions = MinMaxNormalization(actions);
            double random = new Random().NextDouble();
            double total = actions.Sum(x => x.Value);
            for (int i = 0; i < actions.Count; i++)
            {

            }
            */
        }

        /// <summary>
        /// 學習
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="action"></param>
        /// <param name="nextState"></param>
        /// <param name="reward"></param>
        /// <returns></returns>
        double ILearningAlgorithm.Learn(State currentState, ActionSpace.Action action, State nextState, double reward, bool isTerminal)
        {
            double newQ = 0;
            newQ = (1 - _learningRate) * _qTable[currentState][action] + _learningRate * (reward + _discountFactor * (isTerminal ? 1 : GetMaxQ(nextState)));
            _qTable[currentState][action] = newQ;
            return newQ;
        }


        public QLearning()
        {
            _qTable = new Dictionary<State, Dictionary<ActionSpace.Action, double>>(new StateComparer());
        }
        public QLearning(double learningRate, double discountFactor) : this()
        {
            _learningRate = learningRate;
            _discountFactor = discountFactor;


        }

        public void SetEnvironment(int[,] maze, int xLimit, int yLimit, bool isRandomInit)
        {
            _qTable = new Dictionary<State, Dictionary<ActionSpace.Action, double>>(new StateComparer());
            Random random = new Random();
            ActionSpace.Action[] actions = ActionSpace.Values;

            for (int y = -1; y <= yLimit + 1; y++)
            {
                for (int x = -1; x <= xLimit + 1; x++)
                {
                    State state = new State(x, y);
                    Dictionary<ActionSpace.Action, double> acitonValues = new Dictionary<ActionSpace.Action, double>();
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
