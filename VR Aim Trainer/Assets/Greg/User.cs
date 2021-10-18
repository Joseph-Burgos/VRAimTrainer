using UnityEngine;
// using System.IO;

// Represents data about the current logged in user.
public static class User
{
    // directory to store user data for testing
    public static string directory = "/Greg/data/";
    // file for the current users data
    public static string currentUserFile = "CurrentUser.txt";
    // file with data for all users
    public static string usersFile = "Users.txt";

    // user data fields
    // userID (immutable), provided by server or read from disk
    private static string userID = "None";
    // username (mutable)
    private static string userName = "User";
    // auth token or simple PIN (mutable), generated by server
    private static string userToken = "None";

    // METHODS
    // getter for username
    public static string getUsername() { return userName; }
    
    // setter for username
    private static void setUsername(string newName) { userName = newName; }
    
    // getter for auth token or simple PIN
    public static string getToken() { return userToken; }
    
    // setter for auth token or simple PIN
    private static void setToken(string newToken) { userToken = newToken; }
    
    // getter for userID
    public static string getUserID() { return userID; }
    
    // setter for auth token or simple PIN
    private static void setUserID(string newUserID) { userToken = newToken; }

    // TODO contact server to request sign-in returns necessary user data, will require a pin

    // TODO method to generate a random name

    // TODO method to load user data from disk

    // TODO method to write user data to disk
}
