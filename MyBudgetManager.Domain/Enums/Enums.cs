namespace MyBudgetManager.Domain.Common;

public enum AccountStatus   
{
    Pending,
    Active,
    Inactive,
    Blocked
}

public enum TokenType
{
    RefreshToken,
    ActivationToken,
    AccessToken,
    ResetToken,
    ResetPasswordToken,
}

public enum Currency
{
    VND,
    USD
}

public enum CategoryType
{
    Income,
    Expense,
}

public enum Role
{
    Owner,
    Member
}

public enum SystemRole
{
    Admin,
    User
}