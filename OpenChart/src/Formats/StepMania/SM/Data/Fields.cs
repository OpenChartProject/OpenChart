using System.Collections.Generic;

namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents the fields in an SM file.
    /// </summary>
    public class Fields
    {
        /// <summary>
        /// A dictionary of field names to field values.
        /// </summary>
        public readonly Dictionary<string, FieldValue> FieldDict;

        /// <summary>
        /// A list of fields for note data.
        /// </summary>
        public readonly List<FieldValue> NoteDataFields;

        /// <summary>
        /// Creates a new Fields instance.
        /// </summary>
        public Fields()
        {
            FieldDict = new Dictionary<string, FieldValue>();
            NoteDataFields = new List<FieldValue>();
        }

        /// <summary>
        /// Adds a field to the dictionary.
        /// </summary>
        /// <param name="key">The field name.</param>
        /// <param name="val">The raw field value as a string.</param>
        public void Add(string key, string val)
        {
            FieldDict.Add(key, new FieldValue(val));
        }

        /// <summary>
        /// Adds a note data field.
        /// </summary>
        /// <param name="val">The raw field value as a string.</param>
        public void AddNoteData(string val)
        {
            NoteDataFields.Add(new FieldValue(val));
        }

        /// <summary>
        /// Gets a field as a bool, or the default if it doesn't exist.
        /// </summary>
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

        /// <summary>
        /// Gets a field as a double, or the default if it doesn't exist.
        /// </summary>
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

        /// <summary>
        /// Gets a field as an int, or the default if it doesn't exist.
        /// </summary>
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

        /// <summary>
        /// Gets a field as a string, or the default if it doesn't exist.
        /// </summary>
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
