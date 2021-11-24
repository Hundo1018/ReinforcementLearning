using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HundoMatrix;

namespace ReinforcementLearning
{
    /// <summary>
    /// 表示一個同個時間下的狀態，將Matrix包裝成Vector來使用
    /// </summary>
    public class State
    {
        public double[,] Value { get => _value; set => _value = value; }
        private Matrix _value;
        public static State operator +(State state, State state1)
        {
            return new State(state._value + state1._value);
        }
        //public static bool operator ==(State state, State state1)
        //{
        //    return state._value == state1._value;
        //}
        //public static bool operator !=(State state, State state1)
        //{
        //    return state._value != state1._value;
        //}
        public static State operator -(State state, State state1)
        {
            return new State(state._value - state._value);
        }
        public static State operator *(State state, State state1)
        {
            return new State(state._value * state1._value);
        }

        /// <summary>
        /// 取得曼哈頓距離
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public double ManhattanDistance(State state)
        {
            return _value.ManhattanDistance(state._value);
        }

        /// <summary>
        /// 取得歐幾里得距離
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public double GetEuclideanDistance(State state)
        {
            return _value.EuclideanDistance(state._value);
        }

        /// <summary>
        /// 判斷是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is State state &&
                   EqualityComparer<Matrix>.Default.Equals(_value, state._value);
        }

        /// <summary>
        /// 取得雜湊值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return -1755142420 + EqualityComparer<Matrix>.Default.GetHashCode(_value);
        }


        public double this[int index]
        {
            get { return _value[index]; }
            set { _value[index] = value; }
        }
        

        private State(Matrix matrix)
        {
            _value = matrix;
        }
        public State()
        {
            _value = new Matrix();
        }

        public State(double[,] vs)
        {
            _value = new Matrix(vs);
        }

        public State(params double[] vs)
        {
            _value = new Matrix(vs);
        }

        public State(int n, int m)
        {
            _value = new Matrix(n, m);
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public static implicit operator double[](State state) => state._value;

        public static implicit operator double[,](State state) => state._value;

        public static implicit operator List<double>(State state) => state._value;

    }
    public class StateComparer : IEqualityComparer<State>
    {
        /// <summary>
        /// 兩數值相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Equals(State a, State b)
        {
            return a.Value == b.Value;
        }

        /// <summary>
        /// 取得雜湊值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(State obj)
        {
            return obj.Value.GetHashCode();
        }
        
    }
}
