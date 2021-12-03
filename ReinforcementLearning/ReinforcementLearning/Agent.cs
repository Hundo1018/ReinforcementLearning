using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning
{
    public class Agent
    {
        //public delegate State Transition();
        public State CurrentState { get; set; }
        public int BatchSize { get; set; }//一次採樣多少筆來學習
        public int MemorySize { get; set; }//每次最多存多少筆資料
        public Memory Memory { get; set; } = new Memory();//記憶
        private readonly ILearningAlgorithm _learningAlgorithm;
        public Agent(ILearningAlgorithm learningAlgorithm)
        {
            _learningAlgorithm = learningAlgorithm;
        }

        /// <summary>
        /// 選擇動作
        /// </summary>
        /// <returns></returns>
        public DiscreteActionSpace.Action ChooseAction(double epsilon)
        {
            return _learningAlgorithm.ChooseAction(CurrentState, epsilon);
        }

        
        /// <summary>
        /// 學習，更新參數
        /// </summary>
        public void Learn()
        {
            //連batch都不夠取就不train
            if (Memory.MemorySize < BatchSize)
                return;
            //打亂
            Memory.Shuffle();
            Memory.GetTransitions(BatchSize).ForEach(transition=> 
            {
                _learningAlgorithm.Learn(
                    transition.State0,
                    transition.Action,
                    transition.State1,
                    transition.Reward,
                    transition.IsTerminal);
            });
        }

        /// <summary>
        /// 開始新的一輪
        /// </summary>
        public void ResetEpoch(bool isResetMemory)
        {
            if (isResetMemory)
                Memory.Clear();
        }
    }
}