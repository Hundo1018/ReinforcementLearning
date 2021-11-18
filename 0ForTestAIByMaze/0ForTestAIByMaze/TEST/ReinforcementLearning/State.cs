using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ReinforcementLearning
{
    /// <summary>
    /// 表示一個同個時間下的狀態
    /// </summary>
    public class State
    {
        
        double _positionX, _positionY;
        public double PositionX { get => _positionX; set { _positionX = value; } }
        public double PositionY { get => _positionY; set { _positionY = value; } }
        public int X { get => (int)_positionX; set { _positionX = value; } }
        public int Y { get => (int)_positionY; set { _positionY = value; } }

        public State(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
        public State(double x, double y)
        {
            PositionX = x;
            PositionY = y;
        }
        public static State operator +(State state, State state1)
        {
            return new State(state.PositionX + state1.PositionX, state.PositionY + state1.PositionY);
        }
        public static bool operator ==(State state, State state1)
        {
            return (state.PositionX == state1.PositionX) && (state.PositionY == state1.PositionY);
        }
        public static bool operator !=(State state, State state1)
        {
            return (state.PositionX != state1.PositionX) || (state.PositionY != state1.PositionY);
        }
        public static State operator -(State state, State state1)
        {
            return new State(state.X - state1.X, state.Y - state1.Y);
        }
        public double GetDistance(State state)
        {
            return Math.Sqrt(Math.Pow(this.PositionX - state.PositionX, 2) + Math.Pow(this.PositionY - state.PositionY, 2));
        }


    }
    public class StateComparer : IEqualityComparer<State>
    {
        public bool Equals(State x, State y)
        {
            return (x.PositionX == y.PositionX) && (x.PositionY == y.PositionY);
        }

        public int GetHashCode(State obj)
        {
            return obj.PositionX.GetHashCode() ^ obj.PositionY.GetHashCode();
        }
    }
    // TODO:State存Vector，並使用array作為結構使用dimention來定義長度，並包含向量的各種運算
}
