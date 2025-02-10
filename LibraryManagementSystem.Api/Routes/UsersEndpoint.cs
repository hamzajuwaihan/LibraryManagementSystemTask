using FluentValidation;
using FluentValidation.Results;
using LibraryManagementSystem.Api.Dtos.Users;
using LibraryManagementSystem.Application.Services.Users;
using LibraryManagementSystem.Domain.Users.Entities;

namespace LibraryManagementSystem.Api.Routes;

public static class UsersEndpoint
{
    public static void MapUsersEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/users").WithTags("Users");

        group.MapPost("/", async (IUserService userService, CreateUserRequestDto requestDto, IValidator<CreateUserRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            User createdUser = await userService.Create(new User()
            {
                UserName = requestDto.UserName!,
                Password = requestDto.Password!,
                Email = requestDto.Email!,
                Role = requestDto.Role
            });

            return Results.Created($"/api/users/{createdUser.Id}", new UserResponseDto(createdUser));
        }).RequireAuthorization("Admin");

        group.MapGet("/{id:guid}", async (IUserService userService, Guid id) => Results.Ok(await userService.GetById(id))).RequireAuthorization();

        group.MapPost("/login", async (IUserService userService, LoginRequestDto loginRequest, IValidator<LoginRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(loginRequest);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage)});
            }

            string token = await userService.Login(loginRequest.Email, loginRequest.Password);
            return Results.Ok(new { Token = token });
        });

        group.MapPut("/{id:guid}", async (IUserService userService, Guid id, UpdateUserRequestDto requestDto, IValidator<UpdateUserRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            User user = new()
            {
                UserName = requestDto.UserName!,
                Password = requestDto.Password!,
                Email = requestDto.Email!,
                Role = requestDto.Role
            };

            User updatedUser = await userService.Update(id, user);

            return Results.Ok(new UserResponseDto(updatedUser));
        }).RequireAuthorization("Admin");
    }
}
