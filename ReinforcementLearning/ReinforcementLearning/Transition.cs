using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning
{
    public struct Transition
    {
        public State State0 { get; set; }
        public DiscreteActionSpace.Action Action { get; set; }
        public double Reward { get; set; }
        public State State1 { get; set; }
        public bool IsTerminal { get; set; }

        public Transition(State state0, DiscreteActionSpace.Action action, double reward, State state1, bool isTerminal)
        {
            State0 = state0;
            Action = action;
            Reward = reward;
            State1 = state1;
            IsTerminal = isTerminal;
        }

        public Transition(State state0, DiscreteActionSpace.Action action, double reward, State state1) : this(state0, action, reward, state1, false)
        {
        }

        public Transition(Transition memory) : this(memory.State0, memory.Action, memory.Reward, memory.State1, memory.IsTerminal)
        {
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is Transition other && this.Equals(other);

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Equals(Transition o) => State0 == o.State0 && State1 == o.State1 && Action == o.Action && Reward == o.Reward;

        /// <summary>
        /// 取雜湊
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => (State0, Action, Reward, State1).GetHashCode();

        public static bool operator ==(Transition lhs, Transition rhs) => lhs.Equals(rhs);

        public static bool operator !=(Transition lhs, Transition rhs) => !(lhs == rhs);

        /// <summary>
        /// 顯示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"state t:{State0},action:{Action},reward:{Reward},state t+1:{State1},isTerminal{IsTerminal}";
        }
    }

}
