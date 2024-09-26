using Microsoft.AspNet.Identity;
using Moq;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthentificationServices;
using SimpleTrader.Domain.Services.AuthentificationServices.Interfaces;
using SimpleTrader.Domain.Services.Interfaces;

namespace SimpleTrader.Domain.Tests.Services.AuthentificationServices
{
    [TestFixture]
    public class AuthentificationProviderTests
    {
        private Mock<IAccountService> _mockAccountService;
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private AuthentificationProvider _authenticationProvider;

        [SetUp]
        public void Setup()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authenticationProvider = new AuthentificationProvider(_mockAccountService.Object, _mockPasswordHasher.Object);
        }


        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ShouldReturnAccountForCorrectUsername()
        {
            // Arrange
            string expectedUsername = "testRegister", 
                password = "testRegister123";

            // Setup the mock account service to return an account with the expected username
            _mockAccountService.Setup(service => service.GetByUserName(expectedUsername))
                .ReturnsAsync(new Account { AccountHolder = new User { Username = expectedUsername } });
            // Setup the mock password hasher to return a successful password verification
            _mockPasswordHasher.Setup(service => service.VerifyHashedPassword(It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Success);

            // Act
            Account actualAccount = await _authenticationProvider.Login(expectedUsername, password);

            // Assert 
            string actualUsername = actualAccount.AccountHolder.Username;
            Assert.AreEqual(actualUsername, expectedUsername);
        }

        [Test]
        public void Login_WithInCorrectPasswordForExistingUsername_ThrowsInvalidPasswordException()
        {
            // Arrange
            string expectedUsername = "testRegister", 
                password = "testRegister";

            // Setup the mock account service to return an account with the expected username
            _mockAccountService.Setup(service => service.GetByUserName(expectedUsername))
                .ReturnsAsync(new Account { AccountHolder = new User { Username = expectedUsername } });
            // Setup the mock password hasher to return a failed password verification
            _mockPasswordHasher.Setup(service => service.VerifyHashedPassword(It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Failed);

            // Act
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(()    
                =>  _authenticationProvider.Login(expectedUsername, password));

            // Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(actualUsername, expectedUsername);
        }

        [Test]
        public async Task Login_WithNonExistingUsername_ThrowsUserNotFoundException()
        {
            // Arrange
            string expectedUsername = "IAmNotInTheSystemYet123",
                password = "testRegister";

            // Act
            UserNotFoundException globalException = Assert.ThrowsAsync<UserNotFoundException>(() 
                => _authenticationProvider.Login(expectedUsername, password));

            // Assert
            string actualUsername = globalException.Username;
            Assert.AreEqual(actualUsername, expectedUsername);
        }


        [Test]
        public async Task Register_WithNonMatchingPasswords_ReturnsPasswordDoNotMatch()
        {
            // Arrange
            string password = "IAmNotInTheSystemYet123", 
                confirmPassword = "doesNotMatch";
            RegistrationResult expectedRegistrationResult =  RegistrationResult.PasswordDoNotMatch;

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword, It.IsAny<double>());

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }

        [Test]
        public async Task Register_WithEmptyEmailOrUsernameOrPassword_ReturnsUsernameOrEmailOrPasswordIsEmpty()
        {
            // Arrange
            string email = "email", 
                username = "username", 
                password = "password";
            RegistrationResult expectedRegistrationResultEmailOrPasswordOrUsername = RegistrationResult.UsernameOrEmailOrPasswordIsEmpty;

            // Act
            RegistrationResult actualRegistrationResultEmailIsEmpty = await _authenticationProvider.Register("", username, password, password, It.IsAny<double>());
            RegistrationResult actualRegistrationResultUserNameIsEmpty = await _authenticationProvider.Register(email, "", password, password, It.IsAny<double>());
            RegistrationResult actualRegistrationResultPasswordIsEmpty = await _authenticationProvider.Register(email, username, "", "", It.IsAny<double>());

            // Assert
            Assert.AreEqual(expectedRegistrationResultEmailOrPasswordOrUsername, actualRegistrationResultEmailIsEmpty);
            Assert.AreEqual(expectedRegistrationResultEmailOrPasswordOrUsername, actualRegistrationResultUserNameIsEmpty);
            Assert.AreEqual(expectedRegistrationResultEmailOrPasswordOrUsername, actualRegistrationResultPasswordIsEmpty);
        }

        [Test]
        public async Task Register_WithEmailAlreadyExists_ReturnsEmailAlreadyExists()
        {
            // Arrange
            string email = "testRegister@test.com",
                  username = "username",
               password = "password";
            // Setup the expected result for the test
            RegistrationResult expectedRegistrationResult = RegistrationResult.EmailAlreadyExists;
            // Setup the mock account service to return an account with the expected email
            _mockAccountService.Setup(setup => setup.GetByEmail(email))
                .ReturnsAsync(new Account { AccountHolder = new User { Email = email } });

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(email, username, password, password, It.IsAny<double>());

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }

        [Test]
        public async Task Register_WithUsernameAlreadyExists_ReturnsUsernameAlreadyExists()
        {
            // Arrange
            string email = "test@test123.com",
                   username = "testRegister",
                   password = "password";
            // Setup the expected result for the test
            RegistrationResult expectedRegistrationResult = RegistrationResult.UsernameAlreadyExists;
            // Setup the mock account service to return an account with the expected username
            _mockAccountService.Setup(setup => setup.GetByUserName(username))
                .ReturnsAsync(new Account { AccountHolder = new User { Username = username } });

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(email, username, password, password, It.IsAny<double>());

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }

        [Test]
        public async Task Register_WithStartBalanceLessThan0_ReturnsStartBalanceMustBePositive()
        {
            // Arrange
            double notEnoughStartBalance = -1;
            string email = "email",
               username = "username",
               password = "password";
            RegistrationResult expectedRegistrationResult = RegistrationResult.StartingBalanceMustBePositive;

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(email, username, password, password, notEnoughStartBalance);

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }

        [Test]
        public async Task Register_WithNewUserAndCorrectParameters_ReturnsSuccess()
        {
            // Arrange
            string email = "new@user0.com",
                   username = "newUser0",
                   password = "password",
                   confirmPassword = "password";
            double startBalance = 50;
            // Setup the expected result for the test
            RegistrationResult expectedRegistrationResult = RegistrationResult.Success;

            // Act
            RegistrationResult actualRegistrationResult = await _authenticationProvider.Register(email, username, password, confirmPassword, startBalance);

            // Assert
            Assert.AreEqual(expectedRegistrationResult, actualRegistrationResult);
        }
    }
}
