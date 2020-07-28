using NUnit.Framework;
using OpenChart.Formats.StepMania.SM.Data;

namespace OpenChart.Tests.Formats.StepMania.SM.Data
{
    public class TestFieldValue
    {
        [TestCase("no", false)]
        [TestCase("0", false)]
        [TestCase("yes", true)]
        [TestCase("1", true)]
        public void Test_AsBool(string val, bool expected)
        {
            Assert.AreEqual(expected, new FieldValue(val).AsBool());
        }

        [TestCase("-1.0", -1.0)]
        [TestCase("0", 0)]
        [TestCase("123.45", 123.45)]
        public void Test_AsDouble(string val, double expected)
        {
            Assert.AreEqual(expected, new FieldValue(val).AsDouble());
        }

        [TestCase("-1", -1)]
        [TestCase("0", 0)]
        [TestCase("123", 123)]
        public void Test_AsInt(string val, int expected)
        {
            Assert.AreEqual(expected, new FieldValue(val).AsInt());
        }

        [TestCase("")]
        [TestCase("asdf")]
        public void Test_AsString(string val)
        {
            Assert.AreEqual(val, new FieldValue(val).AsString());
        }
    }
}
