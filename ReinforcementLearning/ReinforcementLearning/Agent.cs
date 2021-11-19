using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning
{
    public class Agent
    {
        public delegate State Transition();
        public State CurrentState { get; set; }
        public int BatchSize { get; set; }
        private ILearningAlgorithm _learningAlgorithm;
        List<Memory> batchMemories = new List<Memory>();
        public Agent(ILearningAlgorithm learningAlgorithm)
        {
            _learningAlgorithm = learningAlgorithm;
        }

        /// <summary>
        /// 選擇動作
        /// </summary>
        /// <returns></returns>
        public ActionSpace.Action ChooseAction(double epsilon)
        {
            return _learningAlgorithm.ChooseAction(CurrentState, epsilon);
        }
        /// <summary>
        /// 將狀態轉移的記憶紀錄下來
        /// </summary>
        /// <param name="memory"></param>
        public void Record(Memory memory)
        {
            //這裡應該要使用樹或heap加入並排序重要性，而不是直接加入list
            batchMemories.Add(memory);
            Forget();
        }
        /// <summary>
        /// 檢查記憶中是否包含某狀態
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsContainMemory(State state)
        {
            bool result = false;
            for (int i = 0; i < batchMemories.Count - 1; i++)
            {
                result = batchMemories[i].State0 == state || batchMemories[i].State1 == state;
                if (result)
                    return result;
            }
            return result;
        }
        private void Forget()
        {
            //這裡應該要把不重要的去掉，而不是只刪頭
            if (batchMemories.Count > BatchSize)
                batchMemories.RemoveAt(0);
        }

        public void Learn()
        {
            if (batchMemories.Count < BatchSize)
                return;
            //這

            Random random = new Random();
            /*for (int i = 0; i < batchMemories.Count; i++)
            {
                int j = random.Next(0, batchMemories.Count);
                Memory memory = batchMemories[i];
                batchMemories[i] = batchMemories[j];
                batchMemories[j] = memory;
            }*/
            batchMemories.Sort((x, y) => { return random.Next(); });
            for (int i = 0; i < batchMemories.Count / 3; i++)
            {
                _learningAlgorithm.Learn(batchMemories[i].State0, batchMemories[i].Action, batchMemories[i].State1, batchMemories[i].Reward, batchMemories[i].IsTerminal);
            }

        }
    }
}