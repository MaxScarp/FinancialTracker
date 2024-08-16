using Firebase.Auth;
using System;
using System.Threading.Tasks;
using UnityEngine;

public static class Authenticator
{
    public struct UserResult
    {
        public AuthResult Result;
        public string ErrorMessage;
    }

    public static FirebaseAuth Auth { get; private set; }
    public static FirebaseUser User { get; private set; }

    public static void Init()
    {
        Auth = FirebaseAuth.DefaultInstance;
        if (Auth == null)
        {
            Debug.LogError("Error: Could not find a valid instance of Firebase auth!");
            return;
        }

        Auth.StateChanged += Auth_StateChanged;
    }

    private static void Auth_StateChanged(object sender, EventArgs e)
    {
        if (Auth.CurrentUser == null)
        {
            return;
        }
        if (Auth.CurrentUser == User)
        {
            return;
        }

        User = Auth.CurrentUser;
    }

    public static void LogoutUser()
    {
        Auth.SignOut();
        User = null;
    }

    public static async Task<UserResult> RegisterNewUser(string email, string password)
    {
        try
        {
            AuthResult authResult = await Auth.CreateUserWithEmailAndPasswordAsync(email, password);
            return new UserResult { Result = authResult, ErrorMessage = string.Empty };
        }
        catch (Exception authError)
        {
            return new UserResult { Result = null, ErrorMessage = authError.Message };
        }
    }

    public static async Task<UserResult> LoginUser(string email, string password)
    {
        //TODO
        //if (!Auth.CurrentUser.IsEmailVerified)
        //{
        //    return new UserResult { Result = null, ErrorMessage = "Your email has not been verified yet, please check your email." };
        //}

        try
        {
            Credential credential = EmailAuthProvider.GetCredential(email, password);
            AuthResult authResult = await Auth.SignInAndRetrieveDataWithCredentialAsync(credential);
            return new UserResult { Result = authResult, ErrorMessage = string.Empty };
        }
        catch (Exception authError)
        {
            return new UserResult { Result = null, ErrorMessage = authError.Message };
        }
    }

    public static async Task<UserResult> ReLoginUser(string email, string password)
    {
        try
        {
            Credential credential = EmailAuthProvider.GetCredential(email, password);
            AuthResult authResult = await User.ReauthenticateAndRetrieveDataAsync(credential);
            return new UserResult { Result = authResult, ErrorMessage = string.Empty };
        }
        catch (Exception authError)
        {
            return new UserResult { Result = null, ErrorMessage = authError.Message };
        }
    }

    public static async Task<string> DeleteUser()
    {
        try
        {
            await User.DeleteAsync();
            return string.Empty;
        }
        catch (Exception authError)
        {
            return authError.Message;
        }
    }

    public static async Task<string> SendVerificationEmail()
    {
        try
        {
            await User.SendEmailVerificationAsync();
            return string.Empty;
        }
        catch (Exception authError)
        {
            return authError.Message;
        }
    }
}
