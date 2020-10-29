namespace OpenChart.Formats.StepMania.SM.Data
{
    public class ChartType
    {
        public string Name { get; private set; }
        public int KeyCount { get; private set; }

        public ChartType(string name, int keys)
        {
            Name = name;
            KeyCount = keys;
        }
    }
}
