using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReinforcementLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementLearning.Tests
{
    [TestClass()]
    public class StateTests
    {
        [TestMethod()]
        public void ManhattanDistanceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetEuclideanDistanceTest()
        {
            State state = new State(1, 1);
            Assert.Fail();
        }

        [TestMethod()]
        public void EqualsTest()
        {
            State state = new State(1,1);
            State state1 = new State(1,1);
            Assert.AreEqual(state1, state);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            State state = new State(2,2);
            State state1 = new State(2, 2);
            Assert.AreNotEqual(state.GetHashCode(), state1.GetHashCode());
        }

        [TestMethod()]
        public void StateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StateTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StateTest2()
        {
            Assert.Fail();
        }
    }
}