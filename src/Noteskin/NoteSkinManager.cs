using System;
using System.IO;
using System.Collections.Generic;

namespace charter.NoteSkin
{
    public class NoteSkinManager
    {
        List<NoteSkin> noteSkins;

        public NoteSkinManager()
        {
            noteSkins = new List<NoteSkin>();
        }

        /// <summary>
        /// Attempts to load a noteskin with the given name.
        /// </summary>
        /// <param name="skinName">The name of the folder for the noteskin. <seealso cref="App.NoteSkinFolder" /></param>
        public void Load(string skinName)
        {
            var dirName = Path.Join(App.NoteSkinFolder, skinName);

            if (!Directory.Exists(App.NoteSkinFolder))
            {
                throw new Exception($"The noteskins folder ('{App.NoteSkinFolder}') does not exist.");
            }
            else if (!Directory.Exists(dirName))
            {
                throw new Exception($"Unable to find a noteskin with the name '{skinName}'.");
            }

            // TODO: write a function that creates a new noteskin instance, looks for folders that match /\d+k/
        }
    }
}