using BansheeGz.BGDatabase;
using UnityEngine;
using YG;

namespace Localization
{
    public class LanguageManager : MonoBehaviour
    {
        void Start()
        {
            if (YG2.isSDKEnabled)
                Apply(YG2.lang);
            else
                YG2.GetLanguage();
        }

        void OnDestroy()
        {
            YG2.onCorrectLang -= Apply;
            YG2.onSwitchLang  -= Apply;
        }

        private void Apply(string lang)
        {
            BGRepo.I.Addons.Get<BGAddonLocalization>().CurrentLocale = lang;
            Debug.Log($"Locale set: {lang}");
        }

        public static string GetLocalizedName(string labelID, string tableName, string fieldName)
        {
            var currentLocalizedRow = BGRepo.I[tableName].FindEntity(entity => entity.Id.ToString() == labelID);

            if (currentLocalizedRow != null)
                return currentLocalizedRow.Get<string>(fieldName);

            Debug.Log("There is no translation for this text");
            return string.Empty;
        }
    }
}
