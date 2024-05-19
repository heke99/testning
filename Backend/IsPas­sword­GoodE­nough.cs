using System;
using System.Text.RegularExpressions;

public class PasswordChecker
{
    public static bool IsPasswordGoodEnough(string password)
    {
        if (password.Length < 8)
        {
            return false;
        }
        if (!Regex.IsMatch(password, "[a-z]"))
        {
            return false;
        }
        if (!Regex.IsMatch(password, "[A-Z]"))
        {
            return false;
        }
        if (!Regex.IsMatch(password, "[0-9]"))
        {
            return false;
        }
        if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
        {
            return false;
        }
        return true;
    }

    public static void Main()
    {
        
}
