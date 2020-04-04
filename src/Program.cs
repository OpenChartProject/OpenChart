using System;
using ManagedBass;

namespace charter
{
    class Program
    {
        public static void Main(string[] args)
        {
            playAudio();
            App.Init();
            App.Run();
        }

        private static void playAudio()
        {
            if (!Bass.Init())
            {
                Console.WriteLine("Failed to initialize libbass");
                App.Quit();
            }

            var stream = Bass.CreateStream("audio.wav");
            Bass.ChannelPlay(stream);
        }
    }
}