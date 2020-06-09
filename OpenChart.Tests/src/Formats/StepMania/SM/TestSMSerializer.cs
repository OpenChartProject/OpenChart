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
        public void Test_ExtractFields_SingleField()
        {
            var fields = serializer.ExtractFields("#NAME:value;");

            Assert.AreEqual(1, fields.Count);
            Assert.AreEqual(fields["NAME"], "value");
        }

        [Test]
        public void Test_ExtractFields_Unicode()
        {
            var fields = serializer.ExtractFields("#FIELD:こんにちは;");

            Assert.AreEqual(1, fields.Count);
            Assert.AreEqual(fields["FIELD"], "こんにちは");
        }

        [Test]
        public void Test_ExtractFields_TrailingNewline()
        {
            var fields = serializer.ExtractFields("#NAME:value;\n");

            Assert.AreEqual(1, fields.Count);
            Assert.AreEqual(fields["NAME"], "value");
        }

        [Test]
        public void Test_ExtractFields_MultipleFields_NoWhitespace()
        {
            var fields = serializer.ExtractFields("#FIELD1:one;#FIELD2:two;");

            Assert.AreEqual(2, fields.Count);
            Assert.AreEqual(fields["FIELD1"], "one");
            Assert.AreEqual(fields["FIELD2"], "two");
        }

        [Test]
        public void Test_ExtractFields_MultipleFields_NewlineLF()
        {
            var fields = serializer.ExtractFields("\n#FIELD1:one;\n#FIELD2:two;\n");

            Assert.AreEqual(2, fields.Count);
            Assert.AreEqual(fields["FIELD1"], "one");
            Assert.AreEqual(fields["FIELD2"], "two");
        }

        [Test]
        public void Test_ExtractFields_MultipleFields_NewlineCRLF()
        {
            var fields = serializer.ExtractFields("\r\n#FIELD1:one;\r\n#FIELD2:two;\r\n");

            Assert.AreEqual(2, fields.Count);
            Assert.AreEqual(fields["FIELD1"], "one");
            Assert.AreEqual(fields["FIELD2"], "two");
        }

        [Test]
        public void Test_ExtractFields_MultiLineValue_CRLF()
        {
            var fields = serializer.ExtractFields("#FIELD:multi\r\nline;");

            Assert.AreEqual(1, fields.Count);
            Assert.AreEqual(fields["FIELD"], "multi\r\nline");
        }
    }
}
