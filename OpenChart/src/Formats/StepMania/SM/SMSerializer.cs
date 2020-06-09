using OpenChart.Formats.StepMania.SM.Data;
using OpenChart.Formats.StepMania.SM.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace OpenChart.Formats.StepMania.SM
{
    /// <summary>
    /// Serializer class for importing .sm files.
    ///
    /// .sm docs: https://github.com/stepmania/stepmania/wiki/sm
    /// </summary>
    public class SMSerializer : IFormatSerializer<StepFileData>
    {
        enum State
        {
            LookingForField,
            ReadingName,
            ReadingValue,
            InComment,
        }

        const char TOKEN_COMMENT = '/';
        const char TOKEN_FIELD_START = '#';
        const char TOKEN_FIELD_NAME_END = ':';
        const char TOKEN_FIELD_VALUE_END = ';';
        const char TOKEN_NEWLINE = '\n';

        /// <summary>
        /// Deserializes raw file data into a StepFileData object.
        /// </summary>
        public StepFileData Deserialize(byte[] data)
        {
            var strData = Encoding.UTF8.GetString(data);
            var fields = ExtractFields(strData);

            return null;
        }

        /// <summary>
        /// Serializes a StepFileData object into the raw .sm data.
        /// </summary>
        public byte[] Serialize(StepFileData obj)
        {
            return null;
        }

        /// <summary>
        /// Extracts the raw fields from the step file. Returns a dictionary of the field names
        /// and values. Field names are transformed to uppercase.
        /// </summary>
        public Dictionary<string, string> ExtractFields(string data)
        {
            var dict = new Dictionary<string, string>();
            var state = State.LookingForField;
            var preCommentState = state;

            var buffer = new StringBuilder();
            char last = '\0';
            string name = "";

            foreach (var c in data)
            {
                // Check for comments first. Save the state we were in so we can return to that
                // state after the comment.
                if (c == TOKEN_COMMENT && last == TOKEN_COMMENT)
                {
                    preCommentState = state;
                    state = State.InComment;

                    // When a comment is found in the middle of a value, the first forward slash
                    // will be added to the buffer.
                    if (buffer.Length > 0)
                        buffer.Remove(buffer.Length - 1, 1);

                    continue;
                }

                switch (state)
                {
                    // Looking for the start of a field. Skip until we hit a field start or find
                    // a comment.
                    case State.LookingForField:
                        if (c == TOKEN_FIELD_START)
                        {
                            buffer.Clear();
                            state = State.ReadingName;
                        }

                        break;

                    // Reading the field name. Fill the buffer until we hit the separator.
                    case State.ReadingName:
                        if (c == TOKEN_FIELD_NAME_END)
                        {
                            // Save the field name.
                            name = buffer.ToString().ToUpper();
                            buffer.Clear();
                            state = State.ReadingValue;
                        }
                        else
                            buffer.Append(c);

                        break;

                    // Reading the field value. Fill the buffer until we hit the end of the field,
                    // then add the field to the dictionary.
                    case State.ReadingValue:
                        if (c == TOKEN_FIELD_VALUE_END)
                        {
                            // Add the field to the dictionary.
                            dict[name] = buffer.ToString();
                            buffer.Clear();
                            state = State.LookingForField;
                        }
                        else
                            buffer.Append(c);

                        break;

                    // Inside of a comment. Ignore the rest of the line.
                    case State.InComment:
                        if (c == TOKEN_NEWLINE)
                        {
                            state = preCommentState;
                            buffer.Append(TOKEN_NEWLINE);
                        }

                        break;
                }

                last = c;
            }

            if (state == State.ReadingName || state == State.ReadingValue)
                throw new UnexpectedEOFException();

            return dict;
        }
    }
}
