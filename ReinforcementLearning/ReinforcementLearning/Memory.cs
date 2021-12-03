using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning
{
    public class Memory
    {
        /// <summary>
        /// 能儲存的最大記憶量
        /// </summary>
        public int MaxMemorySize { get; set; } = int.MaxValue;
        public int MemorySize { get { return _transitions.Count; } }
        private List<Transition> _transitions = new List<Transition>();
        private Random _random = new Random();

        /// <summary>
        /// 儲存狀態轉移過程
        /// </summary>
        /// <param name="memory"></param>
        public void StoreTransition(Transition memory)
        {
            _transitions.Add(memory);
            Forget();
        }

        /// <summary>
        /// 儲存狀態轉移過程
        /// </summary>
        /// <param name="state0"></param>
        /// <param name="action"></param>
        /// <param name="reward"></param>
        /// <param name="state1"></param>
        public void StoreTransition(State state0, DiscreteActionSpace.Action action, double reward, State state1,bool isTerminal)
        {
            StoreTransition(new Transition(state0, action, reward, state1, isTerminal));
        }

        /// <summary>
        /// 取得某個位置的狀態轉移記憶
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Transition GetTransition(int index)
        {
            return _transitions[index];
        }

        /// <summary>
        /// 從0開始取得幾筆狀態轉移記憶
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        public List<Transition> GetTransitions(int batchSize)
        {
            return _transitions.GetRange(0, batchSize);
        }

        /// <summary>
        /// 檢查記憶中是否包含某狀態
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsContainMemory(State state)
        {
            bool result = false;
            for (int i = 0; i < MemorySize-1; i++)
            {
                result = _transitions[i].State0 == state || _transitions[i].State1 == state;
                if (result)
                    return result;
            }
            return result;
        }

        /// <summary>
        /// 忘掉多餘的記憶
        /// </summary>
        private void Forget()
        {
            //這裡應該要把不重要的去掉，而不是只刪頭
            if (_transitions.Count > MaxMemorySize)
                _transitions.RemoveAt(0);
        }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort()
        {

        }
        
        /// <summary>
        /// 打亂
        /// </summary>
        public void Shuffle()
        {
            _transitions.Sort((x, y) => _random.Next(-1, 0));
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            _transitions = new List<Transition>();
        }

        public Memory()
        {
            _transitions = new List<Transition>();
        }
    }
}
