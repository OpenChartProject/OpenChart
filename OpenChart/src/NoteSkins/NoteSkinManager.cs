using OpenChart.UI.Assets;
using Serilog;
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
        /// Loads all noteskins found in the given path.
        /// </summary>
        /// <param name="path">The path to the noteskins folder.</param>
        public void LoadAll(string path)
        {
            if (!Directory.Exists(path))
                throw new Exception($"The noteskins folder ('{path}') does not exist.");

            foreach (var dir in Directory.GetDirectories(path))
            {
                var name = Path.GetFileName(dir);
                NoteSkin ns;

                try
                {
                    ns = loadNoteSkin(path, name);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Exception while trying to load noteskin '{name}'.");
                    continue;
                }

                noteSkins.Add(ns);
                Log.Information($"Loaded noteskin '{ns.Name}' ({ns.KeyModes.Count} key skin(s) found).");
            }
        }

        /// <summary>
        /// Attempts to load a noteskin with the given name.
        /// </summary>
        /// <param name="skinName">The name of the folder for the noteskin. <seealso cref="ApplicationData.NoteSkinFolder" /></param>
        /// <returns>The loaded noteskin.</returns>
        private NoteSkin loadNoteSkin(string path, string skinName)
        {
            var skinPath = Path.Combine(path, skinName);

            if (!Directory.Exists(skinPath))
                throw new Exception($"Unable to find a noteskin with the name '{skinName}'.");

            var noteSkin = new NoteSkin(skinName);

            // Look for keymode folders inside the noteskin folder.
            foreach (var dir in Directory.EnumerateDirectories(skinPath))
            {
                var match = reNoteSkinKeyModeDir.Match(Path.GetFileName(dir));

                if (!match.Success)
                    continue;

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
        /// <param name="path">The noteskin directory path.</param>
        private void loadKeyModeSkin(NoteSkin noteSkin, int keyCount, string path)
        {
            KeyModeSkin kms = new KeyModeSkin(keyCount);

            for (var i = 1; i <= keyCount; i++)
            {
                NoteSkinKey key = new NoteSkinKey();

                // key.Receptor = new ImageAsset(Path.Join(path, $"receptor_{i}.png"));
                key.TapNote = new ImageAsset(Path.Join(path, $"tap_{i}.png"));
                key.HoldNote = new ImageAsset(Path.Join(path, $"hold_{i}.png"));
                key.HoldNoteBody = new ImagePattern(new ImageAsset(Path.Join(path, $"hold_body_{i}.png")));

                kms.Set(i - 1, key);
            }

            noteSkin.AddKeyModeSkin(kms);
        }
    }
}
