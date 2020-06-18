namespace OpenChart.Formats.StepMania.SM.Data
{
    public class FieldValue
    {
        public readonly string Value;

        public FieldValue(string val)
        {
            Value = val;
        }

        public bool AsBool()
        {
            return Value == "yes" || Value == "1";
        }

        public float AsFloat()
        {
            return float.Parse(Value);
        }

        public int AsInt()
        {
            return int.Parse(Value);
        }

        public string AsString()
        {
            return Value;
        }
    }
}
