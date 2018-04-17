using System.IO;

namespace Tanks3D.Localization
{
    public class LocalizationNotFoundExeption : FileNotFoundException
    {
        public LangCode Language { get; private set; }

        public LocalizationNotFoundExeption(LangCode langCode)
        {
            Language = langCode;
        }

        public override string Message
        {
            get
            {
                return "Localization cannot be found for language " + Language;
            }
        }
    }
}