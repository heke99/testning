using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}

public class CreateMockUser
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

    public static string Encrypt(string password)
    {
        // Dummy encryption method for demonstration
        // Replace this with a real encryption method
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public static bool UserExists(List<User> existingUsers, string email)
    {
        // Kontrollera om användaren redan finns i listan
        return existingUsers.Exists(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public static List<object> CreateMockUsers()
    {
        string mockUsersFilePath = Path.Combine("json", "mock-users.json");
        string createdUsersFilePath = Path.Combine("json", "created-users.json");

        

        string jsonContent = File.ReadAllText(mockUsersFilePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(jsonContent);

        // Läs in existerande användare från filen created-users.json
        List<User> existingUsers = new List<User>();
        if (File.Exists(createdUsersFilePath))
        {
            string existingUsersContent = File.ReadAllText(createdUsersFilePath);
            existingUsers = JsonSerializer.Deserialize<List<User>>(existingUsersContent);
        }

        List<object> createdUsers = new List<object>();

        foreach (User user in users)
        {
            // Skapa lösenord baserat på e-postadressen
            string password = char.ToUpper(user.Email[0]) + user.Email.Substring(1);

            // Kontrollera om lösenordet är tillräckligt starkt
            if (IsPasswordGoodEnough(password))
            {
                string encryptedPassword = Encrypt(password);

                // Kontrollera om användaren redan finns i listan över existerande användare
                if (!UserExists(existingUsers, user.Email))
                {
                    user.Password = encryptedPassword; // Lagra lösenordet temporärt för lagring

                    // Lägg till användaren i listan över skapade användare (exklusive lösenord)
                    createdUsers.Add(new { user.FirstName, user.LastName, user.Email });

                    // Lägg till användaren i listan över existerande användare
                    existingUsers.Add(user);
                }
            }
        }

        // Skriv de uppdaterade användarna till created-users.json
        File.WriteAllText(createdUsersFilePath, JsonSerializer.Serialize(existingUsers));

        return createdUsers;
    }

    public static void Main()
    {
        
        {
            List<object> createdUsers = CreateMockUsers();
            foreach (var user in createdUsers)
            {
                Console.WriteLine($"Created user: {user}");
            }


        }
    }
}
