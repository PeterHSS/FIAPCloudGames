using FIAPCloudGames.Application.DTOs.Users;

namespace FIAPCloudGames.Application.Validators.Users;

internal sealed class UpdateUserValidator : AbstractUserValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        AddNameRule(user => user.Name);

        AddNicknameRule(user => user.Nickname);
    }
}
