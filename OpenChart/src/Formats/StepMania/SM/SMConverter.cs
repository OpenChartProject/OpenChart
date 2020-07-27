using OpenChart.Formats.StepMania.SM.Data;
using OpenChart.Formats.StepMania.SM.Enums;
using OpenChart.Formats.StepMania.SM.Exceptions;
using OpenChart.Projects;
using Serilog;

namespace OpenChart.Formats.StepMania.SM
{
    /// <summary>
    /// Converter for importing and exporting SM files.
    /// </summary>
    public class SMConverter : IProjectConverter<StepFileData>
    {
        public StepFileData FromNative(Project chart)
        {
            throw new System.NotImplementedException();
        }

        public Project ToNative(StepFileData sfd)
        {
            if (sfd.PlayData.BPMs.Count == 0)
                throw new NoBPMException();

            var p = new Project();
            p.SongMetadata = new Songs.SongMetadata();

            // Convert every chart in the step file.
            for (var i = 0; i < sfd.Charts.Count; i++)
            {
                Log.Information($"Converting chart {i} of {sfd.Charts.Count}...");
                p.AddChart(convertSMChartToNative(sfd, sfd.Charts[i]));
            }

            p.Name = sfd.SongData.Title;
            p.SongMetadata.Title = sfd.SongData.Title;
            p.SongMetadata.Artist = sfd.SongData.Artist;
            p.SongMetadata.AudioFilePath = sfd.SongData.Music;

            return p;
        }

        private Charting.Chart convertSMChartToNative(StepFileData sfd, Chart smChart)
        {
            var c = new Charting.Chart(smChart.GetKeyCount());

            c.Author = smChart.Author;
            c.ChartName = sfd.SongData.Title;

            // Convert the BPM changes.
            foreach (var bpm in sfd.PlayData.BPMs)
            {
                c.BPMList.BPMs.Add(new Charting.Properties.BPM(bpm.Value, bpm.Beat));
            }

            // Holds have a start and an end in the .sm format, and we don't know where the end
            // is until we find it. When we encounter the head of a hold, we'll store it in this
            // array and update it when we find the tail.
            var holds = new Charting.Objects.BaseLongObject[c.KeyCount.Value];

            // Convert each measure.
            foreach (var measure in smChart.Measures)
            {
                for (var i = 0; i < measure.BeatRows.Length; i++)
                {
                    var row = measure.BeatRows[i];
                    var beat = new Charting.Properties.Beat(
                        (measure.Number * 4) + (i / (double)measure.Subdivisions)
                    );

                    for (var key = 0; key < row.KeyCount; key++)
                    {
                        // If we are waiting to find the tail for a hold/roll and we find anything
                        // else, just ignore it.
                        if (holds[key] != null && row.Notes[key] != NoteType.HoldRollTail && row.Notes[key] != NoteType.Empty)
                        {
                            Log.Warning("Found a note illegally placed during a hold/roll, ignoring...");
                            continue;
                        }

                        switch (row.Notes[key])
                        {
                            // Ignore these note types.
                            case NoteType.Empty:
                            case NoteType.KeySound:
                            case NoteType.Fake:
                            case NoteType.Mine:
                                break;

                            // Add tap notes.
                            case NoteType.Lift:
                            case NoteType.Tap:
                                c.Objects[key].Add(new Charting.Objects.TapNote(key, beat));
                                break;

                            // Add holds/rolls.
                            case NoteType.HoldHead:
                            case NoteType.RollHead:
                                // The duration here isn't important since it will be updated.
                                var hold = new Charting.Objects.HoldNote(key, beat, 1);
                                holds[key] = hold;
                                c.Objects[key].Add(hold);
                                break;

                            // Update a previous hold/roll with its duration.
                            case NoteType.HoldRollTail:
                                if (holds[key] == null)
                                    Log.Warning("Found a hold tail without a matching head, ignoring...");
                                else
                                {
                                    holds[key].SetLength(beat.Value - holds[key].Beat.Value);
                                    holds[key] = null;
                                }
                                break;
                        }
                    }
                }
            }

            // Verify that all of the holds/rolls were "closed".
            foreach (var hold in holds)
            {
                if (hold != null)
                {
                    Log.Warning("Expected to find a hold/roll tail but hit EOF, ignoring...");
                    break;
                }
            }

            return c;
        }
    }
}
