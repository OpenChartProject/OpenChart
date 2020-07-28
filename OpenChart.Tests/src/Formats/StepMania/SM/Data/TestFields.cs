using NUnit.Framework;
using OpenChart.Formats.StepMania.SM.Data;

namespace OpenChart.Tests.Formats.StepMania.SM.Data
{
    public class TestFields
    {
        Fields fields;

        [SetUp]
        public void SetUp()
        {
            fields = new Fields();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Test_GetBool_Default(bool val)
        {
            Assert.AreEqual(val, fields.GetBool("field", val));
        }

        [Test]
        public void Test_GetBool_Exists()
        {
            fields.Add("field", "yes");
            Assert.AreEqual(true, fields.GetBool("field"));
        }

        [TestCase(0)]
        [TestCase(123.45)]
        public void Test_GetDouble_Default(double val)
        {
            Assert.AreEqual(val, fields.GetDouble("field", val));
        }

        [Test]
        public void Test_GetDouble_Exists()
        {
            fields.Add("field", "123.45");
            Assert.AreEqual(123.45, fields.GetDouble("field"));
        }

        [TestCase(0)]
        [TestCase(123)]
        public void Test_GetInt_Default(int val)
        {
            Assert.AreEqual(val, fields.GetInt("field", val));
        }

        [Test]
        public void Test_GetInt_Exists()
        {
            fields.Add("field", "123");
            Assert.AreEqual(123, fields.GetInt("field"));
        }

        [TestCase("")]
        [TestCase("foo")]
        public void Test_GetString_Default(string val)
        {
            Assert.AreEqual(val, fields.GetString("field", val));
        }

        [Test]
        public void Test_GetString_Exists()
        {
            fields.Add("field", "foo");
            Assert.AreEqual("foo", fields.GetString("field"));
        }
    }
}
