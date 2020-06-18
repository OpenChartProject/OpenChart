using OpenChart.Formats.StepMania.SM.Data;
using OpenChart.Formats.StepMania.SM.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace OpenChart.Formats.StepMania.SM
{
    /// <summary>
    /// A static class for reading the fields in an SM file.
    /// </summary>
    public static class FieldExtractor
    {
        /// <summary>
        /// The different states the reader can be in while extracting fields.
        /// </summary>
        enum ReaderState
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
        /// Extracts the raw fields from the step file. Returns a dictionary of the field names
        /// and values. Field names are transformed to uppercase.
        /// </summary>
        public static Fields Extract(string data)
        {
            var fields = new Fields();
            var state = ReaderState.LookingForField;
            var preCommentState = state;

            var buffer = new StringBuilder();
            char last = '\0';
            string name = "";

            foreach (var c in data)
            {
                // Skip carriage returns to convert from CRLF -> LF.
                if (c == '\r')
                    continue;

                // Check for comments first. Save the state we were in so we can return to that
                // state after the comment.
                if (c == TOKEN_COMMENT && last == TOKEN_COMMENT)
                {
                    preCommentState = state;
                    state = ReaderState.InComment;

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
                    case ReaderState.LookingForField:
                        if (c == TOKEN_FIELD_START)
                        {
                            buffer.Clear();
                            state = ReaderState.ReadingName;
                        }

                        break;

                    // Reading the field name. Fill the buffer until we hit the separator.
                    case ReaderState.ReadingName:
                        if (c == TOKEN_FIELD_NAME_END)
                        {
                            // Save the field name.
                            name = buffer.ToString().ToUpper();
                            buffer.Clear();
                            state = ReaderState.ReadingValue;
                        }
                        else
                            buffer.Append(c);

                        break;

                    // Reading the field value. Fill the buffer until we hit the end of the field,
                    // then add the field to the dictionary.
                    case ReaderState.ReadingValue:
                        if (c == TOKEN_FIELD_VALUE_END)
                        {
                            // Add the field to the dictionary.
                            fields.Add(name, buffer.ToString().Trim());
                            buffer.Clear();
                            state = ReaderState.LookingForField;
                        }
                        else
                            buffer.Append(c);

                        break;

                    // Inside of a comment. Ignore the rest of the line.
                    case ReaderState.InComment:
                        if (c == TOKEN_NEWLINE)
                        {
                            state = preCommentState;
                            buffer.Append(TOKEN_NEWLINE);
                        }

                        break;
                }

                last = c;
            }

            // The file is incomplete if we hit the EOF while reading a field.
            if (state == ReaderState.ReadingName || state == ReaderState.ReadingValue)
                throw new UnexpectedEOFException();

            return fields;
        }
    }
}
