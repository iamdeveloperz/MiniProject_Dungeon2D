
using Firebase;
using Firebase.Auth;
using System;

public static class AuthErrorMessage
{
    public static string GetErrorMessage(Exception exception)
    {
        if(exception is FirebaseException firebaseException)
        {
            AuthError authError = (AuthError)Enum.Parse(typeof(AuthError), firebaseException.ErrorCode.ToString());

            string errorMessage = authError switch
            {
                AuthError.MissingEmail => "이메일이 입력 되지 않았습니다.",
                AuthError.InvalidEmail => "올바른 형태의 이메일을 작성하세요.",
                AuthError.MissingPassword => "비밀번호가 입력 되지 않았습니다.",
                AuthError.WeakPassword => "비밀번호는 6자리 이상이어야 합니다.",
                AuthError.EmailAlreadyInUse => "이미 사용중인 이메일입니다.",
                AuthError.Failure => "이메일 또는 비밀번호가 올바르지 않습니다.",
                _ => "알 수 없는 오류입니다."
            };

            return errorMessage;
        }

        return "알 수 없는 오류입니다.";
    }
}
