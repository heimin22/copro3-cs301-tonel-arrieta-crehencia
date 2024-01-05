using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace RPGCharacterCreationGame
{

    // ito bai yung abstract class saka yung method, ito rin yung class na kung saan iinherit ng Program class
    public abstract class CharacterDetailSummary
    {
        // ito naman yung method na i-ooverride duon sa Program class meaning may magaganap na polymorphism
        // nandito na rin sa dalawang method na to bai yung method overloading kasi same method pero iba parameters
        public abstract void DisplayCharacterSummary(Character character);
        public abstract string DisplayCharacterSummary(string characterID);        
    }
    // ito naman yung sa structure which is yung ginagamit dito is yung keyword na struct para sa mga variables about sa character details
    public struct Character
    {
        // ito yung mga variables bai

        public string name { get; set; }
        public string gender { get; set; }
        public string hairStyle { get; set; }
        public string facialHair { get; set; }
        public string hairColor { get; set; }
        public string skinColor { get; set; }
        public string tattoos { get; set; }
        public string markings { get; set; }
        public string ageGroup { get; set; }
        public string eyeColor { get; set; }
        public string height { get; set; }
        public string width { get; set; }
        public string accessories { get; set; }
        public string upperBodyClothing { get; set; }
        public string upperBodyClothingStyleOptions { get; set; }
        public string lowerBodyClothing { get; set; }
        public string lowerBodyClothingStyleOptions { get; set; }
        public string footwear { get; set; }
        public string characterRace { get; set; }
        public string characterClass { get; set; }
        public string keepsakes { get; set; }
        public int[] attributes { get; set; }
        public string characterID { get; set; } 
    }
    // ito naman yung sa interfaces bai, yung dito naman is about yung mga method na kung saan gagamitin sa 100% about sa database
    public interface IDatabaseActions
    {
        void SavingCharacter(Character character, string[] attributeNames);
        void LoadingCharacter();
        void DisplayCharacterList();
        void DeleteAllCharacters();
        void DeleteCharacter();

    }
    // dito para sa account ng user to


    public class CharacterManager : IDatabaseActions
    {
        private const string databaseConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\HANDOUTS\COMPUTER PROGRAMMING 1\RPGCHARACTERCREATIONGAME\RPGCHARACTERCREATIONGAME\CHARACTERCREATIONDATABASE.MDF;Integrated Security=True;Connect Timeout=30";
        public void HandleCharacterActions()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("\nCharacter Actions: \n1. Create New Character\n2. Load Character\n3. Delete Character\n4. Exit");
                    Console.Write("Choose an option: ");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Program.GetCharacter();
                            break;
                        case 2:
                            DisplayCharacterList();
                            LoadingCharacter();
                            break;
                        case 3:
                            DisplayCharacterList();

                            Console.WriteLine("\n1. Delete a specific character\n2. Delete all chaarcters");
                            int delChoice = int.Parse(Console.ReadLine());

                            switch (delChoice)
                            {
                                case 1:
                                    DeleteCharacter();
                                    break;
                                case 2:
                                    DeleteAllCharacters();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Please try again.");
                                    break;
                            }
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        public void SavingCharacter(Character character, string[] attributeNames)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO CharacterTable" +
                        "(characterName, characterGender, characterHairStyle, characterFacialHair, characterHairColor, characterSkinColor, characterTattoos, characterMarkings, characterAge, characterEyeColor, characterHeight, characterWidth, " +
                        "characterAccessories, characterUpperBodyC, characterUBStyle, characterLowerBodyC, characterLBSTyle, characterFootwear, " +
                        "characterRace, characterClass, characterKeepsakes, characterSTR, characterDEX, characterCON, characterINT, characterWIS, characterCHA, characterAGI, " +
                        "characterVIT, characterPER, characterLUK, characterWIL, characterFOR, characterARC, characterTEC, characterSTL)" +
                        "VALUES " +
                        "(@characterName, @characterGender, @characterHairStyle, @characterFacialHair, @characterHairColor, @characterSkinColor, @characterTattoos, @characterMarkings, @characterAge, @characterEyeColor, @characterHeight, @characterWidth, " +
                        "@characterAccessories, @characterUpperBodyC, @characterUBStyle, @characterLowerBodyC, @characterLBSTyle, @characterFootwear, " +
                        "@characterRace, @characterClass, @characterKeepsakes, @characterSTR, @characterDEX, @characterCON, @characterINT, @characterWIS, @characterCHA, @characterAGI, " +
                        "@characterVIT, @characterPER, @characterLUK, @characterWIL, @characterFOR, @characterARC, @characterTEC, @characterSTL)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@characterName", character.name);
                        command.Parameters.AddWithValue("@characterGender", character.gender);
                        command.Parameters.AddWithValue("@characterHairStyle", character.hairStyle);
                        command.Parameters.AddWithValue("@characterFacialHair", character.facialHair);
                        command.Parameters.AddWithValue("@characterHairColor", character.hairColor);
                        command.Parameters.AddWithValue("@characterSkinColor", character.skinColor);
                        command.Parameters.AddWithValue("@characterTattoos", character.tattoos);
                        command.Parameters.AddWithValue("@characterMarkings", character.markings);
                        command.Parameters.AddWithValue("@characterAge", character.ageGroup);
                        command.Parameters.AddWithValue("@characterEyeColor", character.eyeColor);
                        command.Parameters.AddWithValue("@characterHeight", character.height);
                        command.Parameters.AddWithValue("@characterWidth", character.width);
                        command.Parameters.AddWithValue("@characterAccessories", character.accessories);
                        command.Parameters.AddWithValue("@characterUpperBodyC", character.upperBodyClothing);
                        command.Parameters.AddWithValue("@characterUBStyle", character.upperBodyClothingStyleOptions);
                        command.Parameters.AddWithValue("@characterLowerBodyC", character.lowerBodyClothing);
                        command.Parameters.AddWithValue("@characterLBStyle", character.lowerBodyClothingStyleOptions);
                        command.Parameters.AddWithValue("@characterFootwear", character.footwear);
                        command.Parameters.AddWithValue("@characterRace", character.characterRace);
                        command.Parameters.AddWithValue("@characterClass", character.characterClass);
                        command.Parameters.AddWithValue("@characterKeepsakes", character.keepsakes);

                        // Dito naman bai ilalagay ko na sa database yung attributes
                        int[] attributes = character.attributes;
                        for (int i = 0; i < attributes.Length; i++)
                        {
                            command.Parameters.AddWithValue($"@character{attributeNames[i]}", attributes[i]);
                        }


                        command.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        public void LoadingCharacter()
        {
            try
            {
                Console.Write("Enter the number of the character you want to load: ");
                int characterNumber = int.Parse(Console.ReadLine());

                string characterName = GetCharacterNameByNumber(characterNumber);

                if (characterName != null)
                {
                    Console.WriteLine($"\nLoading character: {characterName}");

                    // Retrieve the character details from the database using characterName
                    Character loadedCharacter = RetrieveCharacterDetails(characterName);

                    // Call the GetCharacterForEditing method to edit the loaded character
                    GetCharacterForEditing(loadedCharacter);
                }
                else
                {
                    Console.WriteLine("Invalid character number. Please try again.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
        }


        private void GetCharacterForEditing(Character loadedCharacter)
        {
            try
            {
                // Use the provided loadedCharacter or create a new one
                Character character = loadedCharacter.Equals(default(Character)) ? new Character() : loadedCharacter;

                Program programInstance = new Program();

                Console.WriteLine("\nEditing Character Details:");
                character.gender = Program.GetGender();
                character.hairStyle = Program.GetHairStyle(character.gender);
                character.facialHair = Program.GetFacialHair();
                character.hairColor = Program.GetHairColor();
                character.skinColor = Program.GetSkinColor();
                character.tattoos = Program.GetTattoos();
                character.markings = Program.GetOtherMarkings();
                character.ageGroup = Program.GetCharacterAge();
                character.eyeColor = Program.GetEyeColor();
                character.height = Program.GetCharacterHeight();
                character.width = Program.GetBodyWidth();
                character.accessories = Program.GetAccessories();
                character.upperBodyClothing = Program.GetUpperBodyClothing();
                character.upperBodyClothingStyleOptions = Program.GetUpperBodyStyleOptions();
                character.lowerBodyClothing = Program.GetLowerBodyClothing();
                character.lowerBodyClothingStyleOptions = Program.GetLowerBodyStyleOptions();
                character.footwear = Program.GetFootwear();
                character.characterRace = Program.GetCharacterRace();
                character.characterClass = Program.GetCharacterClass();
                character.keepsakes = Program.GetKeepsakes();
                character.attributes = Program.GetAttributeAllocation();
                // attributes are already set during loading

                // Save the edited character details
                string[] attributeNames = { "STR", "DEX", "CON", "INT", "WIS", "CHA", "AGI", "VIT", "PER", "LUK", "WIL", "FOR", "ARC", "TEC", "STL" };
                UpdateCharacter(character, attributeNames);

                programInstance.DisplayCharacterSummary(character);
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        
        public void UpdateCharacter(Character updatedCharacter, string[] attributeNames)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    // Update query for the CharacterTable
                    string updateQuery = "UPDATE CharacterTable SET " +
                        "characterGender = @characterGender, characterHairStyle = @characterHairStyle, " +
                        "characterFacialHair = @characterFacialHair, characterHairColor = @characterHairColor, " +
                        "characterSkinColor = @characterSkinColor, characterTattoos = @characterTattoos, " +
                        "characterMarkings = @characterMarkings, characterAge = @characterAge, " +
                        "characterEyeColor = @characterEyeColor, characterHeight = @characterHeight, " +
                        "characterWidth = @characterWidth, characterAccessories = @characterAccessories, " +
                        "characterUpperBodyC = @characterUpperBodyC, characterUBStyle = @characterUBStyle, " +
                        "characterLowerBodyC = @characterLowerBodyC, characterLBSTyle = @characterLBSTyle, " +
                        "characterFootwear = @characterFootwear, characterRace = @characterRace, " +
                        "characterClass = @characterClass, characterKeepsakes = @characterKeepsakes, " +
                        "characterSTR = @characterSTR, characterDEX = @characterDEX, characterCON = @characterCON, " +
                        "characterINT = @characterINT, characterWIS = @characterWIS, characterCHA = @characterCHA, " +
                        "characterAGI = @characterAGI, characterVIT = @characterVIT, characterPER = @characterPER, " +
                        "characterLUK = @characterLUK, characterWIL = @characterWIL, characterFOR = @characterFOR, " +
                        "characterARC = @characterARC, characterTEC = @characterTEC, characterSTL = @characterSTL " +
                        "WHERE characterName = @characterName";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection)) 
                    {
                        // Add parameters for updating character details
                        command.Parameters.AddWithValue("@characterName", updatedCharacter.name);
                        command.Parameters.AddWithValue("@characterGender", updatedCharacter.gender);
                        command.Parameters.AddWithValue("@characterHairStyle", updatedCharacter.hairStyle);
                        command.Parameters.AddWithValue("@characterFacialHair", updatedCharacter.facialHair);
                        command.Parameters.AddWithValue("@characterHairColor", updatedCharacter.hairColor);
                        command.Parameters.AddWithValue("@characterSkinColor", updatedCharacter.skinColor);
                        command.Parameters.AddWithValue("@characterTattoos", updatedCharacter.tattoos);
                        command.Parameters.AddWithValue("@characterMarkings", updatedCharacter.markings);
                        command.Parameters.AddWithValue("@characterAge", updatedCharacter.ageGroup);
                        command.Parameters.AddWithValue("@characterEyeColor", updatedCharacter.eyeColor);
                        command.Parameters.AddWithValue("@characterHeight", updatedCharacter.height);
                        command.Parameters.AddWithValue("@characterWidth", updatedCharacter.width);
                        command.Parameters.AddWithValue("@characterAccessories", updatedCharacter.accessories);
                        command.Parameters.AddWithValue("@characterUpperBodyC", updatedCharacter.upperBodyClothing);
                        command.Parameters.AddWithValue("@characterUBStyle", updatedCharacter.upperBodyClothingStyleOptions);
                        command.Parameters.AddWithValue("@characterLowerBodyC", updatedCharacter.lowerBodyClothing);
                        command.Parameters.AddWithValue("@characterLBSTyle", updatedCharacter.lowerBodyClothingStyleOptions);
                        command.Parameters.AddWithValue("@characterFootwear", updatedCharacter.footwear);
                        command.Parameters.AddWithValue("@characterRace", updatedCharacter.characterRace);
                        command.Parameters.AddWithValue("@characterClass", updatedCharacter.characterClass);
                        command.Parameters.AddWithValue("@characterKeepsakes", updatedCharacter.keepsakes);

                        // Add parameters for updating attributes
                        for (int i = 0; i < attributeNames.Length; i++)
                        {
                            command.Parameters.AddWithValue($"@character{attributeNames[i]}", updatedCharacter.attributes[i]);
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine($"{e.Message}");
            }
        }

        private Character RetrieveCharacterDetails(string characterName)
        {
            Character character = new Character();

            string[] attributeNames = { "STR", "DEX", "CON", "INT", "WIS", "CHA", "AGI", "VIT", "PER", "LUK", "WIL", "FOR", "ARC", "TEC", "STL" };

            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM CharacterTable WHERE characterName = @characterName";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@characterName", characterName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                character.name = reader["characterName"].ToString();
                                character.gender = reader["characterGender"].ToString();
                                character.hairStyle = reader["characterHairStyle"].ToString();
                                character.facialHair = reader["characterFacialHair"].ToString();
                                character.hairColor = reader["characterHairColor"].ToString();
                                character.skinColor = reader["characterSkinColor"].ToString();
                                character.tattoos = reader["characterTattoos"].ToString();
                                character.markings = reader["characterMarkings"].ToString();
                                character.ageGroup = reader["characterAge"].ToString();
                                character.eyeColor = reader["characterEyeColor"].ToString();
                                character.height = reader["characterHeight"].ToString();
                                character.width = reader["characterWidth"].ToString();
                                character.accessories = reader["characterAccessories"].ToString();
                                character.upperBodyClothing = reader["characterUpperBodyC"].ToString();
                                character.upperBodyClothingStyleOptions = reader["characterUBStyle"].ToString();
                                character.lowerBodyClothing = reader["characterLowerBodyC"].ToString();
                                character.lowerBodyClothingStyleOptions = reader["characterLBSTyle"].ToString();
                                character.footwear = reader["characterFootwear"].ToString();
                                character.characterRace = reader["characterRace"].ToString();
                                character.characterClass = reader["characterClass"].ToString();
                                character.keepsakes = reader["characterKeepsakes"].ToString();

                                // Retrieve attributes
                                int[] attributes = new int[15];
                                for (int i = 0; i < attributes.Length; i++)
                                {
                                    attributes[i] = Convert.ToInt32(reader[$"character{attributeNames[i]}"]);
                                }
                                character.attributes = attributes;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            return character;
        }


        private string GetCharacterNameByNumber(int characterNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT characterName FROM CharacterTable";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 1;

                            while (reader.Read())
                            {
                                if (index == characterNumber)
                                {
                                    return reader["characterName"].ToString();
                                }

                                index++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            return null;
        }


        public void DisplayCharacterList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT characterName FROM CharacterTable";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 1;

                            Console.WriteLine("\nCharacters:");
                            while (reader.Read())
                            {
                                string characterName = reader["characterName"].ToString();
                                Console.WriteLine($"{(index)}. {characterName}");
                                index++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        public void DeleteAllCharacters()
        {
           try
           {

           }
           catch (Exception e) 
           {

           }
        }

        public void DeleteCharacter()
        {
            try
            {
                Console.Write("Enter the number of the character you want to delete: ");
                int characterNumber = int.Parse(Console.ReadLine());

                string characterName = GetCharacterNameByNumber(characterNumber);

                if (characterName != null)
                {
                    using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM CharacterTable WHERE characterName = @characterName";

                        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@characterName", characterName);
                            command.ExecuteNonQuery();

                            Console.WriteLine($"Character '{characterName}' deleted successfully.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Character not found. Please enter a valid character number.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
        }
    }

    // inheritance
    public class Program : CharacterDetailSummary
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to (Game Name) RPG!");
            
            CharacterManager manager = new CharacterManager();

            manager.HandleCharacterActions();

            Character character = GetCharacter();
            

            new Program().DisplayCharacterSummary(character);
        }

        public static Character GetCharacter()
        {
            Program program = new Program();
            Character character = new Character();
            Console.WriteLine("\nCharacter Details:");
            character.gender = GetGender();
            character.hairStyle = GetHairStyle(character.gender);
            character.facialHair = GetFacialHair();
            character.hairColor = GetHairColor();
            character.skinColor = GetSkinColor();
            character.tattoos = GetTattoos();
            character.markings = GetOtherMarkings();
            character.ageGroup = GetCharacterAge();
            character.eyeColor = GetEyeColor();
            character.height = GetCharacterHeight();
            character.width = GetBodyWidth();
            character.accessories = GetAccessories();
            character.upperBodyClothing = GetUpperBodyClothing();
            character.upperBodyClothingStyleOptions = GetUpperBodyStyleOptions();
            character.lowerBodyClothing = GetLowerBodyClothing();
            character.lowerBodyClothingStyleOptions = GetLowerBodyStyleOptions();
            character.footwear = GetFootwear();
            character.characterRace = GetCharacterRace();
            character.characterClass = GetCharacterClass();
            character.keepsakes = GetKeepsakes();
            character.attributes = GetAttributeAllocation();
            character.name = GetName();
            character.characterID = GenerateCharacterID();

            CharacterManager characterManager = new CharacterManager();
            string[] attributeNames = {"STR", "DEX", "CON", "INT", "WIS", "CHA", "AGI", "VIT", "PER", "LUK", "WIL", "FOR", "ARC", "TEC", "STL" };
            characterManager.SavingCharacter(character, attributeNames);
            

            program.DisplayCharacterSummary(character);
            Environment.Exit(0);

            return character;
        }

        public static string GetGender()
        {
            try
            {
                Console.WriteLine("\nEnter your gender:\n1. Male\n2. Female");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        return "Male";
                    case 2:
                        return "Female";
                    default:
                        Console.WriteLine("Invalid choice. Returning default gender: Male");
                        return "Male";
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid choice.");
                return "Male"; // Returning default gender in case of exception
            }
        }
        public static string GetHairStyle(string gender)
        {
            try
            {
                Console.WriteLine("\nChoose a hair style from the options: ");
                if (string.Equals(gender, "male", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Options (Male):\n1. Short Crew Cut\n2. Short Buzzed Cut\n3. Crew Cut\n4. Ivy League Crew Cut\n5. Short Textured Crop\n6. Curly Fade\n7. High Bald Fade\n8. Pompadour Fade\n9. Comb over with tapered sides\n10. Brushed up hairstyle\n11. Quiff\n12. Slicked back undercut\n13. Textured crop with fringe\n14. Short mohawk fade\n15. Curly mohawk\n16. Long curly hair\n17. Long straight hair\n18. Man bun\n19. Top knot bun\n20. Warrior braids\n21. Dreadlocks\n22. Shaved head\n23. Head completely bald");
                }
                else if (string.Equals(gender, "female", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Options (Female):\n1. Very short pixie cut\n2. Short bob haircut\n3. Chin-length bob\n4. Shoulder-length layered cut\n5. Long layers haircut\n6. Flowing straight hair\n7. Beach waves hair\n8. Tight curly hair\n9. Curly bob haircut\n10. Afro textured hair\n11. Buzzed sides with long top\n12. Undercut with long bangs\n13. Cornrow braids\n14. Goddess braids\n15. Long dreadlocks\n16. Short ponytail\n17. High ponytail\n18. Long braid\n19. French braid\n20. Dutch braid\n21. Fishtail braid\n22. Wispy bangs\n23. Swept bangs\n24. Head completely bald");
                }
                else
                {
                    Console.WriteLine("Please enter a valid gender to continue with hairstyle.");
                    GetGender();
                }

                Console.Write("\nEnter the number of your chosen hair style: ");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (string.Equals(gender, "male", StringComparison.OrdinalIgnoreCase))
                    {
                        switch (choice)
                        {
                            case 1:
                                return "Short Crew Cut";
                            case 2:
                                return "Short Buzzed Cut";
                            case 3:
                                return "Crew Cut";
                            case 4:
                                return "Ivy League Crew Cut";
                            case 5:
                                return "Short Textured Crop";
                            case 6:
                                return "Curly Fade";
                            case 7:
                                return "High Bald Fade";
                            case 8:
                                return "Pompadour Fade";
                            case 9:
                                return "Comb over with tapered sides";
                            case 10:
                                return "Brushed up hairstyle";
                            case 11:
                                return "Quiff";
                            case 12:
                                return "Slicked back undercut";
                            case 13:
                                return "Textured crop with fringe";
                            case 14:
                                return "Short mohawk fade";
                            case 15:
                                return "Curly mohawk";
                            case 16:
                                return "Long curly hair";
                            case 17:
                                return "Long straight hair";
                            case 18:
                                return "Man bun";
                            case 19:
                                return "Top knot bun";
                            case 20:
                                return "Warrior braids";
                            case 21:
                                return "Dreadlocks";
                            case 22:
                                return "Shaved head";
                            case 23:
                                return "Head completely bald";
                            default:
                                Console.WriteLine("Invalid choice. Returning default hair style.");
                                return "Short Crew Cut"; // Default hair style
                        }
                    }
                    else if (string.Equals(gender, "female", StringComparison.OrdinalIgnoreCase))
                    {
                        switch (choice)
                        {
                            case 1:
                                return "Very short pixie cut";
                            case 2:
                                return "Short bob haircut";
                            case 3:
                                return "Chin-length bob";
                            case 4:
                                return "Shoulder-length layered cut";
                            case 5:
                                return "Long layers haircut";
                            case 6:
                                return "Flowing straight hair";
                            case 7:
                                return "Beach waves hair";
                            case 8:
                                return "Tight curly hair";
                            case 9:
                                return "Curly bob haircut";
                            case 10:
                                return "Afro textured hair";
                            case 11:
                                return "Buzzed sides with long top";
                            case 12:
                                return "Undercut with long bangs";
                            case 13:
                                return "Cornrow braids";
                            case 14:
                                return "Goddess braids";
                            case 15:
                                return "Long dreadlocks";
                            case 16:
                                return "Short ponytail";
                            case 17:
                                return "High ponytail";
                            case 18:
                                return "Long braid";
                            case 19:
                                return "French braid";
                            case 20:
                                return "Dutch braid";
                            case 21:
                                return "Fishtail braid";
                            case 22:
                                return "Wispy bangs";
                            case 23:
                                return "Swept bangs";
                            case 24:
                                return "Head completely bald";
                            default:
                                Console.WriteLine("Invalid choice. Returning default hair style.");
                                return "Very short pixie cut"; // Default hair style
                        }
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Returning default hair style.");
                return "Very short crew cut"; // Default hair style
            }
            Console.WriteLine("Invalid input. Returning default hair style.");
            return "Very short crew cut"; // Default hair style
        }
        public static string GetFacialHair()
        {
            try
            {
                Console.WriteLine("\nDoes the character have facial hair? \n1. Yes\n2. No ");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        return "Yes";
                    case 2:
                        return "No";

                    default:
                        Console.WriteLine("Invalid choice. Returning default choice: No facial hair");
                        return "No";
                }
            } 
            catch (FormatException) 
            {
                Console.WriteLine("Invalid choice. Returning default choice: No facial hair");
                return "No";
            }      
        }

        public static string GetHairColor()
        {
            try
            {
                Console.WriteLine("\nChoose a hair color from the options: ");
                Console.WriteLine("a. Natural Hair Colors");
                Console.WriteLine("1. Black\n2. Dark Brown\n3. Light Brown\n4. Dark Blonde\n5. Light Blonde\n6. Strawberry Blonde\n7. Golden Blonde\n8. Red\n9. Auburn\n10. Gray/Silver\n11. White");

                Console.WriteLine("b. Unnatural Hair Colors");
                Console.WriteLine("12. Blue\n13. Green\n14. Purple\n15. Pink\n16. Orange\n17. Red-Orange\n18. Burgundy\n19. Lavender\n20. Ocean blue highlight\n21. Turquoise highlight\n22. Magenta highlight\n23. Blue and Purple Ombre\n24. Rainbow colors");

                Console.WriteLine("c. Formatting Options");
                Console.WriteLine("25. Two-toned hair color\n26. Dip-dyed hairstyle\n27. Dyed tips");

                int choice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 27);

                switch (choice)
                {
                    case 1: return "Black";
                    case 2: return "Dark Brown";
                    case 3: return "Light Brown";
                    case 4: return "Dark Blonde";
                    case 5: return "Light Blonde";
                    case 6: return "Strawberry Blonde";
                    case 7: return "Golden Blonde";
                    case 8: return "Red";
                    case 9: return "Auburn";
                    case 10: return "Gray/Silver";
                    case 11: return "White";
                    case 12: return "Blue";
                    case 13: return "Green";
                    case 14: return "Purple";
                    case 15: return "Pink";
                    case 16: return "Orange";
                    case 17: return "Red-Orange";
                    case 18: return "Burgundy";
                    case 19: return "Lavender";
                    case 20: return "Ocean blue highlight";
                    case 21: return "Turquoise highlight";
                    case 22: return "Magenta highlight";
                    case 23: return "Blue and Purple Ombre";
                    case 24: return "Rainbow colors";
                    case 25: return "Two-toned hair color";
                    case 26: return "Dip-dyed hairstyle";
                    case 27: return "Dyed tips";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Black");
                        return "Black";
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid choice. Returning default: Black");
                return "Black";
            }
        }

        public static string GetSkinColor()
        {
            try
            {
                Console.WriteLine("\nChoose a skin color from the options: ");
                Console.WriteLine("a. Fair Skin Tones");
                Console.WriteLine("1. Ivory\n2. Fair\n3. Beige\n4. Pink undertone\n5. Peach undertone\n6. Cream undertone\n7. Porcelain");

                Console.WriteLine("b. Light and Medium Skin Tones");
                Console.WriteLine("8. Tan\n9. Golden tan\n10. Olive tan\n11. Honey\n12. Light brown\n13. Tawny");

                Console.WriteLine("c. Darker Skin Tones");
                Console.WriteLine("14. Caramel\n15. Mocha\n16. Umber\n17. Chestnut brown\n18. Dark chocolate");

                Console.WriteLine("d. Fantasy Skin Colors");
                Console.WriteLine("19. Emerald\n20. Azure\n21. Lavender\n22. Lilac\n23. Midnight blue\n24. Violet\n25. Forest green\n26. Rose gold");

                int choice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 26);

                switch (choice)
                {
                    case 1: return "Ivory";
                    case 2: return "Fair";
                    case 3: return "Beige";
                    case 4: return "Pink undertone";
                    case 5: return "Peach undertone";
                    case 6: return "Cream undertone";
                    case 7: return "Porcelain";
                    case 8: return "Tan";
                    case 9: return "Golden tan";
                    case 10: return "Olive tan";
                    case 11: return "Honey";
                    case 12: return "Light brown";
                    case 13: return "Tawny";
                    case 14: return "Caramel";
                    case 15: return "Mocha";
                    case 16: return "Umber";
                    case 17: return "Chestnut brown";
                    case 18: return "Dark chocolate";
                    case 19: return "Emerald";
                    case 20: return "Azure";
                    case 21: return "Lavender";
                    case 22: return "Lilac";
                    case 23: return "Midnight blue";
                    case 24: return "Violet";
                    case 25: return "Forest green";
                    case 26: return "Rose gold";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Ivory");
                        return "Ivory";
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid choice. Returning default: Ivory");
                return "Ivory";
            }
        }
        public static string GetTattoos()
        {
            try
            {
                Console.WriteLine("\nChoose tattoos from the options: ");
                Console.WriteLine("a. Tattoos");
                Console.WriteLine("1. Tribal tattoo designs\n2. Flower tattoos\n3. Animal tattoos like tiger, snake, Phoenix\n4. Skull tattoos\n5. Geometric tattoo patterns\n6. Script/text tattoos\n7. Full sleeve arm tattoos\n8. Back tattoos");

                int choice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 8);

                switch (choice)
                {
                    case 1: return "Tribal tattoo designs";
                    case 2: return "Flower tattoos";
                    case 3: return "Animal tattoos like tiger, snake, Phoenix";
                    case 4: return "Skull tattoos";
                    case 5: return "Geometric tattoo patterns";
                    case 6: return "Script/text tattoos";
                    case 7: return "Full sleeve arm tattoos";
                    case 8: return "Back tattoos";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Tribal tattoo designs");
                        return "Tribal tattoo designs";

                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid choice. Returning default: Tribal tattoo designs");
                return "Tribal tattoo designs";
            }
            
        }
        public static string GetOtherMarkings()
        {
            try
            {
                Console.WriteLine("\nChoose other markings from the options: ");
                Console.WriteLine("b. Other Markings");
                Console.WriteLine("1. Beauty marks/moles\n2. Freckles\n3. Scars - different shapes and body locations\n4. War paint designs\n5. Glowing technicolor lines\n6. Cyborg implant markings\n7. Fantasy runes/sigils\n8. Birthmarks\n9. Stretch marks");

                int choice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 9);

                switch (choice)
                {
                    case 1: return "Beauty marks/moles";
                    case 2: return "Freckles";
                    case 3: return "Scars - different shapes and body locations";
                    case 4: return "War paint designs";
                    case 5: return "Glowing technicolor lines";
                    case 6: return "Cyborg implant markings";
                    case 7: return "Fantasy runes/sigils";
                    case 8: return "Birthmarks";
                    case 9: return "Stretch marks";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Beauty marks/moles");
                        return "Beauty marks/moles";
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Invalid choice. Returning default: Beauty marks/moles");
                return "Beauty marks/moles";
            }
        }
        public static string GetCharacterAge()
        {
            try
            {
                Console.WriteLine("\nEnter the character's age: \n1. Young\n2. Adult\n3. Elder ");
                Console.Write("Enter your age: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        return "Young";
                    case 2:
                        return "Adult";
                    case 3:
                        return "Elder";
                    default:
                        Console.WriteLine("Invalid age. Returning default: Adult");
                        return "Adult";
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid age. Returning default: Adult");
                return "Adult";
            }
        }
        public static string GetEyeColor()
        {
            try
            {
                Console.WriteLine("\nChoose an eye color from the options: ");
                Console.WriteLine("a. Common Eye Colors");
                Console.WriteLine("1. Brown\n2. Hazel\n3. Green\n4. Blue\n5. Gray\n6. Amber\n7. Violet");

                Console.WriteLine("b. Variant Eye Colors");
                Console.WriteLine("8. Olive green\n9. Pale blue\n10. Deep blue\n11. Light gray\n12. Dark gray\n13. Gold/yellow\n14. Red/rust color\n15. Purple");

                Console.WriteLine("b. Heterochromia Options");
                Console.WriteLine("16. Choose any two-eye color combinations");

                Console.WriteLine("c. Unnatural/Fantasy Eye Colors");
                Console.WriteLine("17. Pink\n18. Red\n19. Orange\n20. Silver\n21. White\n22. Black");

                int choice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 22);

                switch (choice)
                {
                    case 1: return "Brown";
                    case 2: return "Hazel";
                    case 3: return "Green";
                    case 4: return "Blue";
                    case 5: return "Gray";
                    case 6: return "Amber";
                    case 7: return "Violet";
                    case 8: return "Olive green";
                    case 9: return "Pale blue";
                    case 10: return "Deep blue";
                    case 11: return "Light gray";
                    case 12: return "Dark gray";
                    case 13: return "Gold/yellow";
                    case 14: return "Red/rust color";
                    case 15: return "Purple";
                    case 16: return "Heterochromia";
                    case 17: return "Pink";
                    case 18: return "Red";
                    case 19: return "Orange";
                    case 20: return "Silver";
                    case 21: return "White";
                    case 22: return "Black";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Brown");
                        return "Brown";
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Invalid choice. Returning default: Brown");
                return "Brown";
            }
        }
        public static string GetCharacterHeight()
        {
            try
            {
                Console.WriteLine("\nEnter the character's height: \n1. Short\n2. Average\n3. Tall ");
                Console.Write("Enter your height: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        return "Short";
                    case 2:
                        return "Average";
                    case 3:
                        return "Tall";
                    default:
                        // Default case if none of the options match
                        Console.WriteLine("Invalid height. Returning default: Average");
                        return "Average";
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Invalid height. Returning default: Average");
                return "Average";
            }
        }
        public static string GetBodyWidth()
        {
            try
            {
                Console.WriteLine("\nEnter the character's frame:\n1. Lean\n2.Muscular\n3.Broad");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice) 
                {
                    case 1:
                        return "Lean";
                    case 2:
                        return "Muscular";
                    case 3:
                        return "Broad";
                    default:
                        Console.WriteLine("Invalid frame. Returning default: Lean");
                        return "Lean";
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid frame. Returning default: Lean");
                return "Lean";
            }
        }
        public static string GetAccessories()
        {
            try
            {
                Console.WriteLine("\nChoose character's accessories from the options: ");
                Console.WriteLine("a. Head");
                Console.WriteLine("1. Glasses\n2. Hats\n3. Headbands & bandanas\n4. Jewelry - earrings, ear cuffs, facial piercings\n5. Eyepatches & blindfolds\n6. Cybernetic eyewear - monocles, lenses");

                Console.WriteLine("b. Face");
                Console.WriteLine("7. Facial hair - variety of beards, mustaches, etc.\n8. Makeup styles & colors\n9. Face paint designs\n10. Masks - decorative, tribal, superhero style\n11. Runes & face tattoos");

                Console.WriteLine("c. Body");
                Console.WriteLine("12. Necklaces & amulets\n13. Rings\n14. Bracelets & armbands\n15. Wrist jewelry - watches, gadgets\n16. Belts, girdles, sashes\n17. Body tattoos & piercings\n18. Armor pieces - spaulders, vambraces");

                int choice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 18);

                switch (choice)
                {
                    case 1: return "Glasses";
                    case 2: return "Hats";
                    case 3: return "Headbands & bandanas";
                    case 4: return "Jewelry - earrings, ear cuffs, facial piercings";
                    case 5: return "Eyepatches & blindfolds";
                    case 6: return "Cybernetic eyewear - monocles, lenses";
                    case 7: return "Facial hair - variety of beards, mustaches, etc.";
                    case 8: return "Makeup styles & colors";
                    case 9: return "Face paint designs";
                    case 10: return "Masks - decorative, tribal, superhero style";
                    case 11: return "Runes & face tattoos";
                    case 12: return "Necklaces & amulets";
                    case 13: return "Rings";
                    case 14: return "Bracelets & armbands";
                    case 15: return "Wrist jewelry - watches, gadgets";
                    case 16: return "Belts, girdles, sashes";
                    case 17: return "Body tattoos & piercings";
                    case 18: return "Armor pieces - spaulders, vambraces";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Glasses");
                        return "Glasses";
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid choice. Returning default: Glasses");
                return "Glasses";
            }
        }
        public static string GetUpperBodyClothing()
        {
            try 
            {
                Console.WriteLine("\nChoose upper body clothing for the character: ");
                Console.WriteLine("a. Tops");
                Console.WriteLine("1. Blouses - sleeveless, cap sleeve, short/long sleeve\n2. Sweaters - form fitting, baggy, crop, v-neck, turtleneck\n3. Jackets - leather, denim, bomber, trench coat, windbreaker\n4. Vests & waistcoats\n5. Hoodies - zip up, pullover\n6. Tank tops & sleeveless shirts\n7. Business shirts - button up, collared\n8. Robes & cloaks\n9. Armor plates & pauldrons");

                int clothingChoice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out clothingChoice) || clothingChoice < 1 || clothingChoice > 9);

                switch (clothingChoice)
                {
                    case 1: return "Blouses - sleeveless, cap sleeve, short/long sleeve";
                    case 2: return "Sweaters - form fitting, baggy, crop, v-neck, turtleneck";
                    case 3: return "Jackets - leather, denim, bomber, trench coat, windbreaker";
                    case 4: return "Vests & waistcoats";
                    case 5: return "Hoodies - zip up, pullover";
                    case 6: return "Tank tops & sleeveless shirts";
                    case 7: return "Business shirts - button up, collared";
                    case 8: return "Robes & cloaks";
                    case 9: return "Armor plates & pauldrons";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Blouses - sleeveless, cap sleeve, short/long sleeve");
                        return "Blouses - sleeveless, cap sleeve, short/long sleeve";
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Invalid choice. Returning default: Blouses - sleeveless, cap sleeve, short/long sleeve");
                return "Blouses - sleeveless, cap sleeve, short/long sleeve";
            }
        }
        public static string GetUpperBodyStyleOptions()
        {
            try
            {
                Console.WriteLine("\nChoose style options for upper body clothing: ");
                Console.WriteLine("b. Style Options");
                Console.WriteLine("1. Plain colors\n2. Striped patterns\n3. Logo & graphic prints\n4. Sheer, mesh & see-through fabrics\n5. Decorative embellishments - studs, rhinestones\n6. Ripped & distressed details");

                int styleChoice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out styleChoice) || styleChoice < 1 || styleChoice > 6);

                switch (styleChoice)
                {
                    case 1: return "Plain colors";
                    case 2: return "Striped patterns";
                    case 3: return "Logo & graphic prints";
                    case 4: return "Sheer, mesh & see-through fabrics";
                    case 5: return "Decorative embellishments - studs, rhinestones";
                    case 6: return "Ripped & distressed details";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Plain colors");
                        return "Plain colors";
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid choice. Returning default: Plain colors");
                return "Plain colors";
            }
        }
        public static string GetLowerBodyClothing()
        {
            try
            {
                Console.WriteLine("\nChoose lower body clothing for the character: ");
                Console.WriteLine("a. Bottoms");
                Console.WriteLine("1. Jeans - skinny, bootcut, flare, cuffed\n2. Shorts - bermuda, high waist, running shorts\n3. Leggings & tights\n4. Skirts - mini, midi, maxi, pencil, a-line\n5. Dresses - cocktail, sundresses, gowns\n6. Work pants - trousers, slacks, dress pants\n7. Athletic pants - track, yoga, sweatpants\n8. Kilts & sarongs\n9. Armor - greaves, tassets, cuisses");

                int clothingChoice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out clothingChoice) || clothingChoice < 1 || clothingChoice > 9);

                switch (clothingChoice)
                {
                    case 1: return "Jeans - skinny, bootcut, flare, cuffed";
                    case 2: return "Shorts - bermuda, high waist, running shorts";
                    case 3: return "Leggings & tights";
                    case 4: return "Skirts - mini, midi, maxi, pencil, a-line";
                    case 5: return "Dresses - cocktail, sundresses, gowns";
                    case 6: return "Work pants - trousers, slacks, dress pants";
                    case 7: return "Athletic pants - track, yoga, sweatpants";
                    case 8: return "Kilts & sarongs";
                    case 9: return "Armor - greaves, tassets, cuisses";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Jeans - skinny, bootcut, flare, cuffed");
                        return "Jeans - skinny, bootcut, flare, cuffed";
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Invalid choice. Returning default: Jeans - skinny, bootcut, flare, cuffed");
                return "Jeans - skinny, bootcut, flare, cuffed";
            }
        }
        public static string GetLowerBodyStyleOptions()
        {
            try
            {
                Console.WriteLine("\nChoose style options for lower body clothing: ");
                Console.WriteLine("b. Style Options");
                Console.WriteLine("1. Plain colors\n2. Camouflage prints\n3. Plaid patterns\n4. Striped patterns\n5. Ripped details\n6. Decorative studs/chains\n7. Sheer & fishnet fabrics");

                int styleChoice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out styleChoice) || styleChoice < 1 || styleChoice > 7);

                switch (styleChoice)
                {
                    case 1: return "Plain colors";
                    case 2: return "Camouflage prints";
                    case 3: return "Plaid patterns";
                    case 4: return "Striped patterns";
                    case 5: return "Ripped details";
                    case 6: return "Decorative studs/chains";
                    case 7: return "Sheer & fishnet fabrics";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Plain colors");
                        return "Plain colors";
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Invalid choice. Returning default: Plain colors");
                return "Plain colors";
            }
        }
        public static string GetFootwear()
        {
            try
            {
                Console.WriteLine("\nChoose footwear for the character: ");
                Console.WriteLine("a. Casual Shoes");
                Console.WriteLine("1. Sneakers - high tops, low tops, running shoes\n2. Flats - ballet flats, loafers, slip-ons\n3. Sandals - thong, sporty, gladiator\n4. Boots - ankle, calf, knee-high\n5. Heels - pumps, stilettos, wedges\n6. Work shoes - leather, casual");
                Console.WriteLine("b. Athletic Footwear");
                Console.WriteLine("7. Cleats\n8. Hiking boots\n9. Running shoes");
                Console.WriteLine("c. Fantasy Footwear");
                Console.WriteLine("10. Steampunk mechanical boots\n11. Armored sabatons\n12. Elven curved shoes\n13. Genie curled toes shoes\n14. Dragonscale boots\n15. Clawed feet");

                int footwearChoice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out footwearChoice) || footwearChoice < 1 || footwearChoice > 18);

                switch (footwearChoice)
                {
                    case 1: return "Sneakers - high tops, low tops, running shoes";
                    case 2: return "Flats - ballet flats, loafers, slip-ons";
                    case 3: return "Sandals - thong, sporty, gladiator";
                    case 4: return "Boots - ankle, calf, knee-high";
                    case 5: return "Heels - pumps, stilettos, wedges";
                    case 6: return "Work shoes - leather, casual";
                    case 7: return "Cleats";
                    case 8: return "Hiking boots";
                    case 9: return "Running shoes";
                    case 10: return "Steampunk mechanical boots";
                    case 11: return "Armored sabatons";
                    case 12: return "Elven curved shoes";
                    case 13: return "Genie curled toes shoes";
                    case 14: return "Dragonscale boots";
                    case 15: return "Clawed feet";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Sneakers - high tops, low tops, running shoes");
                        return "Sneakers - high tops, low tops, running shoes";
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Invalid choice. Returning default: Sneakers - high tops, low tops, running shoes");
                return "Sneakers - high tops, low tops, running shoes";
            }
        }
        public static string GetCharacterRace()
        {
            try
            {
                Console.WriteLine("\nEnter the character's race: \n1. Human\n2. Elf\n3. Dwarf\n4. Orc\n5. Gnome\n6. Halflings\n7. Tiefling\n8. Aasimar\n9. Tabaxi\n10. Genasi");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        return "Human";
                    case 2:
                        return "Elf";
                    case 3:
                        return "Dwarf";
                    case 4:
                        return "Orc";
                    case 5:
                        return "Gnome";
                    case 6:
                        return "Halflings";
                    case 7:
                        return "Tiefling";
                    case 8:
                        return "Aasimar";
                    case 9:
                        return "Tabaxi";
                    case 10:
                        return "Genasi";
                    default:
                        // Default case if none of the options match
                        Console.WriteLine("Invalid race. Returning default: Human");
                        return "Human";
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Invalid race. Returning default: Human");
                return "Human";
            }
        }
        public static string GetCharacterClass()
        {
            try
            {
                Console.WriteLine("Enter the character's class: \n1. Warrior\n2. Mage\n3. Ranger\n4. Rogue\n5. Cleric\n6. Paladin\n7. Bard\n8. Druid\n9. Monk\n10. Warlock");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        return "Warrior";
                    case 2:
                        return "Mage";
                    case 3:
                        return "Ranger";
                    case 4:
                        return "Rogue";
                    case 5:
                        return "Cleric";
                    case 6:
                        return "Paladin";
                    case 7:
                        return "Bard";
                    case 8:
                        return "Druid";
                    case 9:
                        return "Monk";
                    case 10:
                        return "Warlock";
                    default:
                        // Default case if none of the options match
                        Console.WriteLine("Invalid class. Returning default: Warrior");
                        return "Warrior";
                }
            }
            catch (FormatException) 
            {
                Console.WriteLine("Invalid class. Returning default: Warrior");
                return "Warrior";
            }
            
        }
        public static string GetKeepsakes()
        {
            try
            {
                Console.WriteLine("\nChoose character's keepsakes from the options: ");
                Console.WriteLine("a. Consumables");
                Console.WriteLine("1. Health potions\n2. Mana potions\n3. Buff potions\n4. Food/ingredient rations");

                Console.WriteLine("b. Items");
                Console.WriteLine("5. Spellbook or scrolls\n6. Pouches & bags\n7. Tools & gadgets\n8. Quest items like keys, artifacts");

                Console.WriteLine("c. Weapons");
                Console.WriteLine("9. Swords\n10. Daggers\n11. Bows\n12. Axes\n13. Maces\n14. Spears\n15. Guns\n16. Martial Arts Equipment\n17. Shield");

                int choice;
                do
                {
                    Console.Write("Enter the number corresponding to your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 17);

                switch (choice)
                {
                    case 1: return "Health potions";
                    case 2: return "Mana potions";
                    case 3: return "Buff potions";
                    case 4: return "Food/ingredient rations";
                    case 5: return "Spellbook or scrolls";
                    case 6: return "Pouches & bags";
                    case 7: return "Tools & gadgets";
                    case 8: return "Quest items like keys, artifacts";
                    case 9: return "Swords";
                    case 10: return "Daggers";
                    case 11: return "Bows";
                    case 12: return "Axes";
                    case 13: return "Maces";
                    case 14: return "Spears";
                    case 15: return "Guns";
                    case 16: return "Martial Arts Equipment";
                    case 17: return "Shield";
                    default:
                        Console.WriteLine("Invalid choice. Returning default: Health potions");
                        return "Health potions";
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid choice. Returning default: Health potions");
                return "Health potions";
            }
        }

        public static int[] GetAttributeAllocation()
        {
            Console.WriteLine("\nAttribute Allocation (0 to 20pts)");
            Console.WriteLine("- Distribute points between stats like strength, dexterity, intelligence, etc.");

            string[] attributeNames = { "Strength (STR)", "Dexterity (DEX)", "Constitution (CON)", "Intelligence (INT)", "Wisdom (WIS)",
                                "Charisma (CHA)", "Agility (AGI)", "Vitality (VIT)", "Perception (PER)", "Luck (LUK)",
                                "Willpower (WIL)", "Fortitude (FOR)", "Arcane (ARC)", "Tech (TEC)", "Stealth (STL)" };

            int[] attributes = new int[attributeNames.Length];

            Console.WriteLine("\nEnter points for each attribute (0 to 20):");

            for (int i = 0; i < attributeNames.Length; i++)
            {
                do
                {
                    Console.Write($"{attributeNames[i]}: ");
                } while (!int.TryParse(Console.ReadLine(), out attributes[i]) || attributes[i] < 0 || attributes[i] > 20);

                // Prompt the user to enter points again if the value exceeds 20
                while (attributes[i] > 20)
                {
                    Console.WriteLine("Points cannot exceed 20. Please enter again.");
                    Console.Write($"{attributeNames[i]}: ");
                    int.TryParse(Console.ReadLine(), out attributes[i]);
                }
            }

            return attributes;
        }
        public static string GetName()
        {
            Console.Write("\nEnter the character's name: ");
            return Console.ReadLine();
        }
        
        public override void DisplayCharacterSummary(Character character)
        {
            Console.WriteLine("\nCharacter Summary:");
            Console.WriteLine($"Name: {character.name}");
            Console.WriteLine($"Gender: {character.gender}");
            Console.WriteLine($"Age: {character.ageGroup}");
            Console.WriteLine($"Race: {character.characterRace}");
            Console.WriteLine($"Class: {character.characterClass}");
            Console.WriteLine($"Height: {character.height}");
            Console.WriteLine($"Width: {character.width}");
            Console.WriteLine($"Skin Color: {character.skinColor}");
            Console.WriteLine($"Eye Color: {character.eyeColor}");
            Console.WriteLine($"Hair Style: {character.hairStyle}");
            Console.WriteLine($"Hair Color: {character.hairColor}");
            Console.WriteLine($"Facial Hair: {character.facialHair}");
            Console.WriteLine($"Tattoos: {character.tattoos}");
            Console.WriteLine($"Other Markings: {character.markings}");
            Console.WriteLine($"Accessories: {character.accessories}");
            Console.WriteLine($"Upper Body Clothing: {character.upperBodyClothing}");
            Console.WriteLine($"Upper Body Clothing Style Options: {character.upperBodyClothingStyleOptions}");
            Console.WriteLine($"Lower Body Clothing: {character.lowerBodyClothing}");
            Console.WriteLine($"Lower Body Clothing Style Options: {character.lowerBodyClothingStyleOptions}");
            Console.WriteLine($"Footwear: {character.footwear}");
            Console.WriteLine($"Keepsakes: {character.keepsakes}");

            Console.WriteLine("\nAttributes Allocation:");
            string[] attributeNames = { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma",
                                "Agility", "Vitality", "Perception", "Luck", "Willpower", "Fortitude", "Arcane",
                                "Tech", "Stealth" };

            for (int i = 0; i < attributeNames.Length; i++)
            {
                Console.WriteLine($"{attributeNames[i]}: {character.attributes[i]}");
            }
        }

        private static string GenerateCharacterID()
        {
            Random random = new Random();
            int randomID = random.Next(0, 10000);
            return randomID.ToString();
        }

        public override string DisplayCharacterSummary(string characterID)
        {
            Console.WriteLine($"Character ID: {characterID}");
            return characterID;
        }
    }
}
