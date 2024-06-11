using System.Speech.Synthesis;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class VoiceHelperService
    {
        private static SpeechSynthesizer _synthesizer;

        static VoiceHelperService()
        {
            _synthesizer = new SpeechSynthesizer();
            _synthesizer.SelectVoice("Pavel");
            _synthesizer.SetOutputToDefaultAudioDevice();
        }

        public static void SayHello(string name)
        {
            _synthesizer.SpeakAsync("Приветствую, " +  name);
        }

        public static void SayHelloCreator()
        {
            _synthesizer.SpeakAsync("Приветствую, о величайший создатель-повелитель");
        }
    }
}
