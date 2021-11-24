using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning
{

    /// <summary>
    /// 離散動作空間
    /// </summary>
    public static class DiscreteActionSpace
    {
        public enum Action
        {
            Up,
            Down,
            Left,
            Right
        }
        private static int _length = Values.Length;
        public static int Length
        {
            get
            {
                return _length;
            }
        }
        public static Action[] Values
        {
            get
            {
                return Enum.GetValues(typeof(Action)).Cast<Action>().ToArray();
            }
        }

    }
}
