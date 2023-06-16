/**
 * Author:      Yannick Santa Cruz Feuillias
 * Created:     15/06/2023
 **/

/// Dependencies
using UnityEngine;

namespace YannickSCF.CountriesData.Objects {
    [System.Serializable]
    public class CountryData {
        [SerializeField, HideInInspector] private string countryArrayName;

        [SerializeField] private string countryId;
        [SerializeField] private string longCountryId;
        [SerializeField] private CountryNameTranslations countryName;
        [SerializeField] private Sprite countryFlag;

        public string CountryId { get => countryId; }
        public string LongCountryId { get => longCountryId; }
        public CountryNameTranslations CountryName { get => countryName; }
        public Sprite CountryFlag { get => countryFlag; }

        public void CreateArrayName() {
            const string missStr = " - MISS:";
            countryArrayName = countryName.GetTranslatedName(SystemLanguage.English) + " (" + countryId + " / " + longCountryId + ")" + missStr;

            countryArrayName += countryName.HasTranslation(SystemLanguage.English) ? "" : " EN,";
            countryArrayName += countryName.HasTranslation(SystemLanguage.Spanish) ? "" : " ES,";
            countryArrayName += countryName.HasTranslation(SystemLanguage.Italian) ? "" : " IT,";
            countryArrayName += countryName.HasTranslation(SystemLanguage.French) ? "" : " FR,";

            if (countryArrayName.EndsWith(","))
                countryArrayName = countryArrayName.Remove(countryArrayName.Length - 1);
            if (countryArrayName.EndsWith(missStr))
                countryArrayName = countryArrayName.Replace(missStr, "");
        }

        public bool HasNameCoincidence(string name, SystemLanguage? language = null) {
            return !language.HasValue ?
                countryName.HasCoincidence(name) :
                countryName.HasCoincidence(name, language.Value);
        }

        public bool HasPartialNameCoincidence(string partialName, SystemLanguage? language = null) {
            return !language.HasValue ?
                countryName.HasPartialCoincidence(partialName) :
                countryName.HasPartialCoincidence(partialName, language.Value);
        }
    }
}
