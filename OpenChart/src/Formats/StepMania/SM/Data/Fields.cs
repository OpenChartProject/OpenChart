using System.Collections.Generic;

namespace OpenChart.Formats.StepMania.SM.Data
{
    public class Fields
    {
        public readonly Dictionary<string, FieldValue> FieldDict;

        public Fields()
        {
            FieldDict = new Dictionary<string, FieldValue>();
        }

        public void Add(string key, string val)
        {
            FieldDict.Add(key, new FieldValue(val));
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            try
            {
                return FieldDict[key].AsBool();
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }

        public double GetDouble(string key, double defaultValue = 0)
        {
            try
            {
                return FieldDict[key].AsDouble();
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            try
            {
                return FieldDict[key].AsInt();
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }

        public string GetString(string key, string defaultValue = "")
        {
            try
            {
                return FieldDict[key].AsString();
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }
    }
}
