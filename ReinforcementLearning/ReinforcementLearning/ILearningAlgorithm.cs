using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning
{

    public interface ILearningAlgorithm
    {
        /// <summary>
        /// 在當下的狀態中選出一個動作
        /// </summary>
        /// <param name="currentState">當前狀態</param>
        /// <returns>選出的動作</returns>
        DiscreteActionSpace.Action ChooseAction(State currentState);

        /// <summary>
        /// 透過狀態轉移的獎勵學習
        /// </summary>
        /// <param name="currentState">當前狀態</param>
        /// <param name="action">動作</param>
        /// <param name="nextState">下一個狀態</param>
        /// <param name="reward">獎勵</param>
        /// <returns>Q值</returns>
        //double Learn(Transition transition);
        double Learn(State currentState, DiscreteActionSpace.Action action, State nextState, double reward, bool isTerminal);

        /// <summary>
        /// 在當下的狀態中選出一個動作
        /// </summary>
        /// <param name="currentState">當前狀態</param>
        /// <param name="epsilon">epsilon 1:全隨機, epsilon 0:選最大</param>
        /// <returns></returns>
        DiscreteActionSpace.Action ChooseAction(State currentState, double epsilon);

    }
}