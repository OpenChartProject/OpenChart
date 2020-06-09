using NUnit.Framework;
using OpenChart.Formats.StepMania.SM;
using OpenChart.Formats.StepMania.SM.Exceptions;

namespace OpenChart.Tests.Formats.StepMania.SM
{
    public class TestSMSerializer
    {
        SMSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            serializer = new SMSerializer();
        }

        [Test]
        public void Test_ExtractFields_UnexpectedEOF()
        {
            Assert.Throws<UnexpectedEOFException>(
                () => serializer.ExtractFields("#NAME:value")
            );
        }

        [Test]
        public void Test_ExtractFields_Empty()
        {
            var fields = serializer.ExtractFields("");

            Assert.AreEqual(0, fields.Count);
        }

        [Test]
        public void Test_ExtractFields_Unicode()
        {
            var fields = serializer.ExtractFields("#FIELD:こんにちは;");

            Assert.AreEqual(1, fields.Count);
            Assert.AreEqual(fields["FIELD"], "こんにちは");
        }

        [TestCase("#NAME:value;")]
        [TestCase("#NAME:value;\n")]
        [TestCase(" \n#NAME:value; ")]
        public void Test_ExtractFields_SingleField(string val)
        {
            var fields = serializer.ExtractFields(val);

            Assert.AreEqual(1, fields.Count);
            Assert.AreEqual("value", fields["NAME"]);
        }

        [TestCase("#FIELD1:one;#FIELD2:two;")]
        [TestCase("#FIELD1:one;\n#FIELD2:two;")]
        [TestCase("  #FIELD1:one;   #FIELD2:two;  ")]
        public void Test_ExtractFields_MultipleFields(string val)
        {
            var fields = serializer.ExtractFields(val);

            Assert.AreEqual(2, fields.Count);
            Assert.AreEqual("one", fields["FIELD1"]);
            Assert.AreEqual("two", fields["FIELD2"]);
        }

        [TestCase("//comment")]
        [TestCase("// comment \n")]
        [TestCase("// comment1\n// comment 2")]
        [TestCase("//#FIELD:value;")]
        public void Test_ExtractFields_Comment(string val)
        {
            var fields = serializer.ExtractFields(val);

            Assert.AreEqual(0, fields.Count);
        }

        [Test]
        public void Test_ExtractFields_CommentInValue()
        {
            var fields = serializer.ExtractFields("#FIELD:multi// oh yeah woo yeah\nline;");

            Assert.AreEqual(1, fields.Count);
            Assert.AreEqual("multi\nline", fields["FIELD"]);
        }
    }
}
