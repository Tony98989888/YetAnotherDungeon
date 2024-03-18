using System.Text.RegularExpressions;

namespace UserCreation
{
    public static class UserDataHelper
    {
        // Since we are using player name to create folder
        // We need to check if player type in a correct name
        public static bool IsUserNameValid(string userName)
        {
            return IsValidFilename(userName);
        }

        static bool IsValidFilename(string testName)
        {
            Regex containsABadCharacter =
                new Regex("[" + Regex.Escape(System.IO.Path.GetInvalidPathChars().ToString()) + "]");
            if (containsABadCharacter.IsMatch(testName))
            {
                return false;
            }

            // other checks for UNC, drive-path format, etc
            return true;
        }
    }
}