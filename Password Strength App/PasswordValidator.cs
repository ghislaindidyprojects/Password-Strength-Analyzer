namespace Password_Strength_App
{
    public enum passwordStrength
    {
        UpperCase =10, LowerCase =9, Symbol= 11, Digit= 8, Length= 12, NotCommonList= 50,
    }
    internal class PasswordValidator
    {
        Dictionary<passwordStrength, bool> Conditions;
        Dictionary<passwordStrength, string> Suggestions;

        public PasswordValidator()
        {
            Conditions = new Dictionary<passwordStrength, bool>();
            Suggestions = new Dictionary<passwordStrength, string>();
            Suggestions.Add(passwordStrength.UpperCase, "Please add an UpperCase Character");
            Suggestions.Add(passwordStrength.LowerCase, "Please add a LowerCase Character");
            Suggestions.Add(passwordStrength.Symbol, "Please add a special Character");
            Suggestions.Add(passwordStrength.Digit, "Please add a number");
            Suggestions.Add(passwordStrength.Length, "Password must be at least 8 characters");
            Suggestions.Add(passwordStrength.NotCommonList, "Password is in the common list, please try a different one");


        }
        internal bool IsStrong(string password, out string message)
        {
            message = string.Empty;
            setPasswordStrength(password);
            return checkPasswordScore (ref message);
        }

        
  
      private bool checkPasswordScore(ref string message)
      {

        int passwordScore = 0;

            foreach (var strengh in Conditions)
            {
                if (strengh.Value)
                {
                    passwordScore += (int)strengh.Key;
                }

            }
            if (passwordScore < 50)
            {
                message = "Password is in the common list and easy to crack\n" + additionalSuggestions();
                return false;
            }
            if (passwordScore > 50 && passwordScore < 60)
            {
                message = "Password is very weak. \n" + additionalSuggestions();
                return false;
            }
            if (passwordScore >= 60 && passwordScore < 70)
            {
                message = "Password is weak. \n" + additionalSuggestions();
                return false;
            }
            if (passwordScore >= 70 && passwordScore < 80)
            {
                message = "Password is strong. \n" + additionalSuggestions();
                return false;
            }
            if (passwordScore >= 80 && passwordScore <= 92)
            {
                message = "Password is very strong. \n" + additionalSuggestions();
                return true;
            }
            return true;
      } 

        private void setPasswordStrength(string password)
        {
            Conditions.Clear();
            setPasswordStrengths (passwordStrength.Length, password.Length >= 8);
            setPasswordStrengths(passwordStrength.UpperCase, password.Any(char.IsUpper));
            setPasswordStrengths(passwordStrength.LowerCase, password.Any(char.IsLower));
            setPasswordStrengths(passwordStrength.Symbol, password.Any(c =>! char.IsLetterOrDigit(c)));
            setPasswordStrengths(passwordStrength.Digit, password.Any (char.IsDigit));
            setPasswordStrengths(passwordStrength.NotCommonList, !passwordExists(password));
        }

        private bool passwordExists(string password)
        {
            IEnumerable<string> lines = File.ReadLines("commonList.txt");
            foreach (string line in lines)
            {
                if (password == line)
                    return true;
            }
            return false;
        }

        private void setPasswordStrengths (passwordStrength strengths, bool IsSatisfied)
        {
            Conditions[strengths] = IsSatisfied;
        }

        private string additionalSuggestions()
        {
            string additionalSuggestions = string.Empty;

            foreach (var strength in Conditions)
            {
                if (!strength.Value)
                {
                    additionalSuggestions += "\n" + Suggestions[strength.Key];
                }
            }
            return additionalSuggestions;
        }
    }
}