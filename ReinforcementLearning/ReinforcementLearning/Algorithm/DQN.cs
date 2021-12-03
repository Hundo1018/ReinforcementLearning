using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using HundoMatrix;
namespace ReinforcementLearning.Algorithm
{
    /// <summary>
    /// DQN 2013
    /// </summary>
    public class DQN : ILearningAlgorithm
    {
        private double _learningRate;
        private double _discountFactor;
        private const int TRAINING_TIME=1;
        private INeuralNetwork neuralNetwork;

        /// <summary>
        /// 當下狀態下最大Q值的index
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public int MaxArg(List<double> vs)
        {
            int index = -1;
            double max = double.MinValue;
            for (int i = 0; i < vs.Count(); i++)
            {
                if (vs[i] > max)
                {
                    max = vs[i];
                    index = i;
                }
            }
            return index;
        }

        public DiscreteActionSpace.Action ChooseAction(State currentState)
        {
            return ChooseAction(currentState, 0);
        }

        public DiscreteActionSpace.Action ChooseAction(State currentState, double epsilon)
        {
            if (new Random().NextDouble() < epsilon)
                return (DiscreteActionSpace.Action)new Random().Next(0, DiscreteActionSpace.Length);

            List<double> results = neuralNetwork.GetResult(currentState);
            return (DiscreteActionSpace.Action)MaxArg(results);
        }

        public double Learn(State currentState, DiscreteActionSpace.Action action, State nextState, double reward, bool isTerminal)
        {
            double newQ;
            double evaluateQ = neuralNetwork.GetResult(currentState)[(int)action];
            List<double> nextResults = neuralNetwork.GetResult(nextState);
            double targetQ = reward + _discountFactor * (isTerminal ? 1 : nextResults[MaxArg(nextResults)]);
            //newQ = Math.Pow(evaluateQ +  _learningRate * (targetQ - evaluateQ),2);//Todo:這裡沒問題嗎
            newQ = targetQ - evaluateQ;
            List<double> labels = new List<double>();
            for (int i = 0; i < DiscreteActionSpace.Length; i++)
            {
                labels.Add(0);
            }
            labels[(int)action] = newQ;//Todo:這樣是在Train loss嗎?
            neuralNetwork.SetTrainingData(new List<List<double>>() { currentState }, new List<List<double>>() { labels });
            neuralNetwork.StartTrain(TRAINING_TIME);
            //Console.WriteLine(newQ);
            return newQ;


            //

            /*double newQ;
            List<double> currentQ = neuralNetwork.GetResult(currentState);
            List<double> nextQ = neuralNetwork.GetResult(nextState);
            int nextMaxArg = MaxArg(nextQ);
            newQ = currentQ[(int)action] +_learningRate * (reward + _discountFactor * (isTerminal ? 1 : nextQ[nextMaxArg]));
            List<double> lables = new List<double>();
            for (int i = 0; i < DiscreteActionSpace.Length; i++)
            {
                lables.Add(0);
            }
            lables[(int)action] = newQ;

            neuralNetwork.SetTrainingData(new List<List<double>>() { currentState }, new List<List<double>>() { lables });
            neuralNetwork.StartTrain(1);
            return newQ;
            */
        }

        #region Constructor
        public DQN(double learningRate, double discountFactor)
        {
            _learningRate = learningRate;
            _discountFactor = discountFactor;
        }
        public DQN(INeuralNetwork nn, double learningRate, double discountFactor):this(learningRate,discountFactor)
        {
            neuralNetwork = nn;
        }
        public DQN(INeuralNetwork nn) : this(nn, 0.001, 0.9)
        {
        }
        public DQN()
        {

        }
        #endregion
    }
}
