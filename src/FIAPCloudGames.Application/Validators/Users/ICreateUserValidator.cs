using FIAPCloudGames.Application.DTOs.Users;
using FluentValidation;

namespace FIAPCloudGames.Application.Validators.Users;

public interface ICreateUserValidator : IValidator<CreateUserRequest> { }