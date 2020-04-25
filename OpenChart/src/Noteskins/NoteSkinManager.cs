using OpenChart.UI.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace OpenChart.NoteSkins
{
    /// <summary>
    /// Manages loading in noteskins and retrieving them by name.
    /// </summary>
    public class NoteSkinManager
    {
        static Regex reNoteSkinKeyModeDir = new Regex(@"^(\d+)[kK]$");
        List<NoteSkin> noteSkins;

        /// <summary>
        /// Creates a new noteskin manager instance.
        /// </summary>
        public NoteSkinManager()
        {
            noteSkins = new List<NoteSkin>();
        }

        /// <summary>
        /// Returns the noteskin with the given name, or null if it hasn't been loaded or doesn't exist.
        /// </summary>
        /// <param name="name">The name of the noteskin.</param>
        public NoteSkin GetNoteSkin(string name)
        {
            return noteSkins.Find(ns => ns.Name == name);
        }

        /// <summary>
        /// Loads all noteskins found in the noteskins folder. <seealso cref="App.NoteSkinFolder" />
        /// </summary>
        public void LoadAll()
        {
            if (!Directory.Exists(App.NoteSkinFolder))
                throw new Exception($"The noteskins folder ('{App.NoteSkinFolder}') does not exist.");

            foreach (var dir in Directory.GetDirectories(App.NoteSkinFolder))
            {
                var name = Path.GetFileName(dir);
                NoteSkin ns;

                try
                {
                    ns = loadNoteSkin(name);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception while trying to load noteskin {name}: {e}");
                    continue;
                }

                noteSkins.Add(ns);
                Console.WriteLine($"Loaded NoteSkin '{ns.Name}'.");
            }
        }

        /// <summary>
        /// Attempts to load a noteskin with the given name.
        /// </summary>
        /// <param name="skinName">The name of the folder for the noteskin. <seealso cref="App.NoteSkinFolder" /></param>
        /// <returns>The loaded noteskin.</returns>
        private NoteSkin loadNoteSkin(string skinName)
        {
            var dirName = Path.Join(App.NoteSkinFolder, skinName);

            if (!Directory.Exists(dirName))
                throw new Exception($"Unable to find a noteskin with the name '{skinName}'.");

            var noteSkin = new NoteSkin(skinName);

            // Look for keymode folders inside the noteskin folder.
            foreach (var dir in Directory.EnumerateDirectories(dirName))
            {
                var match = reNoteSkinKeyModeDir.Match(Path.GetFileName(dir));

                if (!match.Success)
                {
                    Console.WriteLine($"Skipping directory '{dir}'.");
                    continue;
                }

                var keyCount = int.Parse(match.Groups[1].Value);
                loadKeyModeSkin(noteSkin, keyCount, dir);
            }

            return noteSkin;
        }

        /// <summary>
        /// Loads a single keymode for a particular noteskin.
        /// </summary>
        /// <param name="noteSkin">The NoteSkin object to load the keymode into.</param>
        /// <param name="keyCount">The keymode's key count.</param>
        /// <param name="dir">The noteskin directory path.</param>
        private void loadKeyModeSkin(NoteSkin noteSkin, int keyCount, string dir)
        {
            KeyModeSkin kms = new KeyModeSkin(keyCount);

            for (var i = 1; i <= keyCount; i++)
            {
                NoteSkinKey key = new NoteSkinKey();

                // key.Receptor = new Image(Path.Join(dir, $"receptor_{i}.png"));
                key.TapNote = new ImageAsset(Path.Join(dir, $"tap_{i}.png"));
                // key.HoldNote = new Image(Path.Join(dir, $"hold_{i}.png"));
                // key.HoldNoteBody = new Image(Path.Join(dir, $"hold_body_{i}.png"));

                kms.Set(i - 1, key);
            }

            noteSkin.AddKeyModeSkin(kms);
        }
    }
}
