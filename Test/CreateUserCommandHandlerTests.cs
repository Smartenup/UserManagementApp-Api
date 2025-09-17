using FluentAssertions;
using Moq;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Users.Commands.CreateUser;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Tests;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _passwordHasherMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenEmailIsUnique()
    {
        // Arrange
        var command = new CreateUserCommand("John Doe", "john@example.com", "Password123");

        _userRepositoryMock
            .Setup(repo => repo.IsEmailUniqueAsync(command.Email, CancellationToken.None))
            .ReturnsAsync(true);

        _passwordHasherMock
            .Setup(hasher => hasher.HashPassword(command.Password))
            .Returns("hashed_password");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new CreateUserCommand("John Doe", "john@example.com", "Password123");

        _userRepositoryMock
            .Setup(repo => repo.IsEmailUniqueAsync(command.Email, CancellationToken.None))
            .ReturnsAsync(false);

        _passwordHasherMock
            .Setup(hasher => hasher.HashPassword(command.Password))
            .Returns("hashed_password");


        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}