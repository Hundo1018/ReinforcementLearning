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
        private double _learningRate;
        private double _discountFactor;
        private const int TRAINING_TIME = 1;
        private const int REPLACE_STEP = 100;
        /// <summary>
        /// 目標網路，Fixed Q Target
        /// 相當於r+γmaxa'Q(s',a')) 
        /// </summary>
        INeuralNetwork targetNet;
        /// <summary>
        /// 評估網路、估計網路，計算當下狀態所獲得的各個動作的QValue
        ///  並且隨時更新
        /// 相當於 Q(s,a)
        /// </summary>
        INeuralNetwork evaluateNet;

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

            throw new NotImplementedException();
        }

        public DiscreteActionSpace.Action ChooseAction(State currentState, double epsilon)
        {
            throw new NotImplementedException();
        }

        public double Learn(State currentState, DiscreteActionSpace.Action action, State nextState, double reward, bool isTerminal)
        {
            
            //這裡用eval net?
            double evaluateQ = evaluateNet.GetResult(currentState)[(int)action];
            // List<double> nextResults = evaluateNet.GetResult(nextState);//這裡用target net?
            //double targetQ = reward + _discountFactor * (isTerminal ? 1 : nextResults[MaxArg(nextResults)]);
            double targetQ = targetNet.GetResult(nextState).Max();
            double newQ = Math.Pow(evaluateQ + _learningRate * (targetQ - evaluateQ), 2);//ToDo:這裡沒問題嗎?
            List<double> labels = new List<double>();
            for (int i = 0; i < DiscreteActionSpace.Length; i++)
            {
                labels.Add(0);
            }
            labels[(int)action] = newQ;//Todo:這樣是在train loss嗎
            //todo: 先看資料，如果這樣是在train loss，那應該要
            //todo: 1.可以直接傳入loss給神經網路train
            //todo: 而不是作為訓練資料(訓練資料又會再減一次loss取loss)
            //todo: 或 2.傳入新的值作為訓練資料，而不是相差的值(在神經網路內就會取loss了)
            //todo: 還是沒問題?
            evaluateNet.SetTrainingData(new List<List<double>>() { currentState }, new List<List<double>>() { labels });
            evaluateNet.StartTrain(TRAINING_TIME);
            //Console.WriteLine(newQ);
            return newQ;
        }
    }
}
