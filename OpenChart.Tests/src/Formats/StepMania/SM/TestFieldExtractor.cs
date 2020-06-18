using NUnit.Framework;
using OpenChart.Formats.StepMania.SM;
using OpenChart.Formats.StepMania.SM.Exceptions;

namespace OpenChart.Tests.Formats.StepMania.SM
{
    public class TestFieldExtractor
    {
        [Test]
        public void Test_Extract_UnexpectedEOF()
        {
            Assert.Throws<UnexpectedEOFException>(
                () => FieldExtractor.Extract("#NAME:value")
            );
        }

        [Test]
        public void Test_Extract_Empty()
        {
            var fields = FieldExtractor.Extract("");
            Assert.AreEqual(0, fields.FieldDict.Count);
        }

        [Test]
        public void Test_Extract_Unicode()
        {
            var fields = FieldExtractor.Extract("#FIELD:こんにちは;");
            Assert.AreEqual("こんにちは", fields.GetString("FIELD"));
        }

        [TestCase("#FIELD:value;")]
        [TestCase("#FIELD:value;\n")]
        [TestCase(" \n#FIELD:value; ")]
        public void Test_Extract_SingleField(string val)
        {
            var fields = FieldExtractor.Extract(val);
            Assert.AreEqual("value", fields.GetString("FIELD"));
        }

        [TestCase("#FIELD1:one;#FIELD2:two;")]
        [TestCase("#FIELD1:one;\n#FIELD2:two;")]
        [TestCase("  #FIELD1:one;   #FIELD2:two;  ")]
        public void Test_Extract_MultipleFields(string val)
        {
            var fields = FieldExtractor.Extract(val);

            Assert.AreEqual("one", fields.GetString("FIELD1"));
            Assert.AreEqual("two", fields.GetString("FIELD2"));
        }

        [TestCase("//comment")]
        [TestCase("// comment \n")]
        [TestCase("// comment1\n// comment 2")]
        [TestCase("//#FIELD:value;")]
        public void Test_Extract_Comment(string val)
        {
            var fields = FieldExtractor.Extract(val);
            Assert.AreEqual(0, fields.FieldDict.Count);
        }

        [Test]
        public void Test_Extract_CommentInValue()
        {
            var fields = FieldExtractor.Extract("#FIELD:multi// oh yeah woo yeah\nline;");
            Assert.AreEqual("multi\nline", fields.GetString("FIELD"));
        }


        [TestCase("#FIELD:multi\r\nline;")]
        [TestCase("#FIELD:multi// comment \r\nline;")]
        public void Test_Extract_IgnoresCRLF(string val)
        {
            var fields = FieldExtractor.Extract(val);
            Assert.AreEqual("multi\nline", fields.GetString("FIELD"));
        }

        [TestCase("#FIELD: foo;")]
        [TestCase("#FIELD:foo ;")]
        [TestCase("#FIELD: foo ;")]
        public void Test_Extract_TrimsValueWhitespace(string val)
        {
            var fields = FieldExtractor.Extract(val);
            Assert.AreEqual("foo", fields.GetString("FIELD"));
        }
    }
}
