using System.Collections.Generic;

namespace OpenChart.Formats.StepMania.SM.Enums
{
    public static class ChartType
    {
        static Dictionary<string, Data.ChartType> Types;

        static ChartType()
        {
            Types = new Dictionary<string, Data.ChartType>();

            add(new Data.ChartType("dance-single", 4));
            add(new Data.ChartType("dance-solo", 6));
            add(new Data.ChartType("dance-double", 8));
        }

        public static Data.ChartType Get(string name)
        {
            Data.ChartType val;

            if (!Types.TryGetValue(name, out val))
                return null;

            return val;
        }

        public static IEnumerable<Data.ChartType> List()
        {
            return Types.Values;
        }

        private static void add(Data.ChartType chartType)
        {
            Types.Add(chartType.Name, chartType);
        }
    }
}
