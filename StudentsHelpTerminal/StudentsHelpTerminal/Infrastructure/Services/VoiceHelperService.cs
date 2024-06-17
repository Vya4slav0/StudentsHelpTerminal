using StudentsHelpTerminal.Infrastructure.Extensions;
using System.Speech.Synthesis;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class VoiceHelperService
    {
        public static void SayHello(string name)
        {
            CreateSynthesier().SpeakAsync("Приветствую, " + name);
        }

        public static void SayHelloCreator()
        {
            CreateSynthesier().SpeakAsync("Приветствую, о величайший создатель-повелитель");           
        }

        private static bool IsVoiceInstalled(string voiceName)
        {
            bool isVoiceInstalled = false;
            foreach (InstalledVoice voice in new SpeechSynthesizer().GetInstalledVoices())
            {
                isVoiceInstalled |= voice.VoiceInfo.Name == voiceName;
            }
            return isVoiceInstalled;
        }
        private static SpeechSynthesizer CreateSynthesier()
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            string voiceName = SettingsInteractor.GetSettingValueByName("VoiceName");
            if (IsVoiceInstalled(voiceName))
            {
                synthesizer.SelectVoice(voiceName);
            }
            synthesizer.Volume = SettingsInteractor.GetSettingIntValueByName("Volume").Clamp(0, 100);
            synthesizer.Rate = SettingsInteractor.GetSettingIntValueByName("VoiceRate").Clamp(-10, 10);
            synthesizer.SetOutputToDefaultAudioDevice();
            return synthesizer;
        }
    }
}
