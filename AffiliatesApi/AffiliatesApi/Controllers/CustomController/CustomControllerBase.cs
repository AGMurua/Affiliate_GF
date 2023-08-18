using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace AffiliatesApi.Controllers.CustomController
{
    public abstract class CustomControllerBase : ControllerBase
    {
        internal readonly static string onlyLettersAndSpace = "^[a-zA-Z ]+$";
        internal readonly static string nameErrorMsg = "The name can only contain letters.";
        internal readonly static string idNotFoundMsg = "The affiliate ID does not exist.";

        internal static bool CheckRegexName(string name)
        {
            if (Regex.IsMatch(name, onlyLettersAndSpace))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static string TrimName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            string[] words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string trimmedName = string.Join(" ", words);

            return trimmedName;
        }


    }
}
