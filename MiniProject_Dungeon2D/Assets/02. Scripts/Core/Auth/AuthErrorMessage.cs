
using Firebase;
using Firebase.Auth;
using System;

public static class AuthErrorMessage
{
    public static string GetErrorMessage(Exception exception)
    {
        if(exception is FirebaseException firebaseException)
        {
            AuthError authError = (AuthError)firebaseException.ErrorCode;

            string errorMessage = "�α��� ����";
            errorMessage+= authError switch
            {
                AuthError.MissingEmail => "�̸����� �Է� ���� �ʾҽ��ϴ�.",
                AuthError.InvalidEmail => "�ùٸ� ������ �̸����� �ۼ��ϼ���.",
                AuthError.MissingPassword => "��й�ȣ�� �Է� ���� �ʾҽ��ϴ�.",
                AuthError.WeakPassword => "��й�ȣ�� 6�ڸ� �̻��̾�� �մϴ�.",
                AuthError.EmailAlreadyInUse => "�̹� ������� �̸����Դϴ�.",
                AuthError.UserNotFound => "�������� �ʴ� ������Դϴ�.",
                AuthError.WrongPassword => "��й�ȣ�� Ʋ�Ƚ��ϴ�.",
                _ => "�� �� ���� �����Դϴ�."
            };

            return errorMessage;
        }

        return "�� �� ���� �����Դϴ�.";
    }
}
