/**
 * Author:      Yannick Santa Cruz Feuillias
 * Created:     15/06/2023
 **/

/// Dependencies
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// Custom Dependencies
using YannickSCF.CountriesData.Editor;
using YannickSCF.CountriesData.Objects;

namespace YannickSCF.CountriesData {
    public enum Language { EN, ES, IT, FR }

    [CreateAssetMenu(fileName = "Country Data List", menuName = "Scriptable Objects/YannickSCF/Country Data List", order = 0)]
    public class CountriesDataList : ScriptableObject {

        [SerializeField, CustomCountryDataList("OnCountryDataChanged")] private List<CountryData> _allCountries;

        internal void UpdateArrayNames() {
            foreach(CountryData country in _allCountries) {
                country.CreateArrayName();
            }
        }
        public void OnCountryDataChanged() {
            foreach (CountryData country in _allCountries) {
                country.CreateArrayName();
            }
        }

        public Sprite GetFlagByCode(string code) {
            CountryData res = GetCountryDataByCode(code);

            if (res != null) return res.CountryFlag;
            return null;
        }

        public string GetNameByCode(string code, SystemLanguage? language = null) {
            CountryData res = GetCountryDataByCode(code);

            if (res != null) {
                if (!language.HasValue) language = Application.systemLanguage;
                return res.CountryName.GetTranslatedName(language.Value);
            }
            return null;
        }

        public string GetCodeByName(string name) {
            CountryData res = GetCountryDataByName(name);

            if (res != null) {
                return res.CountryId;
            }
            return null;
        }

        public bool IsCountryCodeInList(string code) {
            return GetCountryDataByCode(code) != null;
        }

        public bool IsCountryNameInList(string name) {
            return GetCountryDataByName(name) != null;
        }

        public Dictionary<string, Sprite> SearchCountriesByCode(string code) {
            Dictionary<string, Sprite> res = new Dictionary<string, Sprite>();

            List<CountryData> allMatches = _allCountries.Where(x => x.CountryId.Contains(code, System.StringComparison.InvariantCultureIgnoreCase)).ToList();
            foreach (CountryData data in allMatches) {
                res.Add(data.CountryId, data.CountryFlag);
            }

            return res;
        }

        public Dictionary<string, Sprite> SearchCountriesByPartialName(string partialName) {
            Dictionary<string, Sprite> res = new Dictionary<string, Sprite>();

            List<CountryData> allMatches = _allCountries.Where(x => x.HasPartialNameCoincidence(partialName)).ToList();
            foreach (CountryData data in allMatches) {
                res.Add(data.CountryId, data.CountryFlag);
            }

            return res;
        }

        private CountryData GetCountryDataByName(string name) {
            return _allCountries.FirstOrDefault(x => x.HasNameCoincidence(name));
        }

        private CountryData GetCountryDataByCode(string code) {
            if (string.IsNullOrEmpty(code) || code.Length != 2) {
                Debug.LogError("Country code wrong length!");
                return null;
            }

            return _allCountries.FirstOrDefault(x => x.CountryId.Equals(code, System.StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
