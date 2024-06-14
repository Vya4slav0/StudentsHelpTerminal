using StudentsHelpTerminal.Infrastructure.Extensions;
using System.Speech.Synthesis;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class VoiceHelperService
    {
        private static SpeechSynthesizer _synthesizer;

        static VoiceHelperService()
        {
            _synthesizer = new SpeechSynthesizer();
            string voiceName = SettingsInteractor.GetSettingValueByName("VoiceName");
            if (IsVoiceInstalled(voiceName))
            {
                _synthesizer.SelectVoice(voiceName);
            }
            _synthesizer.Volume = SettingsInteractor.GetSettingIntValueByName("Volume").Clamp(0, 100);
            _synthesizer.Rate = SettingsInteractor.GetSettingIntValueByName("VoiceRate").Clamp(-10, 10);
        }

        public static void SayHello(string name)
        {
            _synthesizer.SetOutputToDefaultAudioDevice();
            _synthesizer.SpeakAsync("Приветствую, " +  name);
        }

        public static void SayHelloCreator()
        {
            _synthesizer.SetOutputToDefaultAudioDevice();
            _synthesizer.SpeakAsync("Приветствую, о величайший создатель-повелитель");
        }

        private static bool IsVoiceInstalled(string voiceName)
        {
            bool isVoiceInstalled = false;
            foreach (InstalledVoice voice in _synthesizer.GetInstalledVoices())
            {
                isVoiceInstalled |= voice.VoiceInfo.Name == voiceName;
            }
            return isVoiceInstalled;
        }
    }
}
