using Moq;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.Domain.Services.TransactionProviders;

namespace SimpleTrader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class SellStockProviderTests
    {
        private SellStockProvider _sellStockService;

        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<IAccountService> _mockAccountService;

        [SetUp]
        public void Setup()
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IAccountService>();

            _sellStockService = new SellStockProvider(_mockAccountService.Object, _mockStockPriceService.Object);
        }

        [Test]
        public void SellStock_WithNullSeller_ThrowsArgumentNullException()
        {
            // Arrange
            Account seller = null;
            string stockSymbol = "MSFT",
                expectedExceptionMessage = "Seller is not set";
            int shares = 10;

            // Act
            NullReferenceException nullReferenceException = Assert.ThrowsAsync<NullReferenceException>(()
                => _sellStockService.SellStockAsync(seller, stockSymbol, shares));

            // Assert
            StringAssert.Contains(expectedExceptionMessage, nullReferenceException.Message);
        }

        [Test]
        public void SellStock_WithNullOrEmptyStockSymbol_ThrowsArgumentNullException()
        {
            // Arrange
            Account seller = new Account
            {
                AssetTransactions = new List<AssetTransaction>(),
                Balance = 100
            };
            string stockSymbol = string.Empty,
                expectedExceptionMessage = "Symbol is not set";
            int shares = 10;

            // Act
            ArgumentNullException nullReferenceException = Assert.ThrowsAsync<ArgumentNullException>(()
                               => _sellStockService.SellStockAsync(seller, stockSymbol, shares));

            // Assert
            StringAssert.Contains(expectedExceptionMessage, nullReferenceException.Message);
        }

        [Test]
        public void SellStock_WithInsufficientShares_ThrowsInsufficientSharesException()
        {
            // Arrange
            string expectedSymbol = "MSFT";
            int shares = 5, sharesForSell = 6;
            double balance = 1000;
            Account seller = CreateTestAccount(expectedSymbol, shares, balance, true);

            // Act & Assert
            InsufficientSharesException insufficientSharesException = Assert.ThrowsAsync<InsufficientSharesException>(()
                               => _sellStockService.SellStockAsync(seller, expectedSymbol, sharesForSell));

            Assert.AreEqual(expectedSymbol, insufficientSharesException.Symbol);
            Assert.AreEqual(shares, insufficientSharesException.AccountShares);
            Assert.AreEqual(sharesForSell, insufficientSharesException.RequiredShares);
        }

        [Test]
        public void SellStock_WithInvalidSymbol_ThrowsInvalidSymbolException()
        {
            // Arrange
            string expectedSymbol = "invalidSymbol";
            int shares = 5, sharesForSell = 1;
            double balance = 1000;
            Account seller = CreateTestAccount(expectedSymbol, shares, balance, true);

            _mockStockPriceService.Setup(service => service.GetPriceAsync(expectedSymbol))
                .ThrowsAsync(new InvalidSymbolException(expectedSymbol));

            // Act & Assert
            InvalidSymbolException invalidSymbolException = Assert.ThrowsAsync<InvalidSymbolException>(()
                                              => _sellStockService.SellStockAsync(seller, expectedSymbol, sharesForSell));

            Assert.AreEqual(expectedSymbol, invalidSymbolException.Symbol);
        }

        [Test]
        public void SellStock_WithGetPriceFailure_ThrowsException()
        {
            // Arrange
            Account seller = CreateTestAccount("MSFT", 50, 1000, true);
            _mockStockPriceService.Setup(s => s.GetPriceAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception()); 
            
            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => 
                _sellStockService.SellStockAsync(seller, "MSFT", 1));
        }

        [Test]
        public void SellStock_WithAccountUpdateFailure_ThrowsException()
        {
            // Arrange
            Account seller = CreateTestAccount("MSFT", 50, 1000, true);
            _mockStockPriceService.Setup(s => s.GetPriceAsync(It.IsAny<string>()))
                .ReturnsAsync(100);

            _mockAccountService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync((Account)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => 
                _sellStockService.SellStockAsync(seller, "MSFT", 1));
        }

        [Test]
        public void SellStock_WithSuccess_ReturnsUpdatedAccount()
        {
            // Arrange
            int expectedTrancationCount = 2;
            Account seller = CreateTestAccount("MSFT", 50, 1000, true);
            _mockStockPriceService.Setup(s => s.GetPriceAsync(It.IsAny<string>()))
                .ReturnsAsync(50);

            _mockAccountService.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<Account>()))
                .ReturnsAsync(seller);

            // Act
            Account updatedAccount = _sellStockService.SellStockAsync(seller, "MSFT", 1).Result;

            // Assert
            Assert.AreEqual(1050.0, updatedAccount.Balance);
            Assert.AreEqual(50, updatedAccount.AssetTransactions.First().SharesAmount);
            Assert.AreEqual(expectedTrancationCount, updatedAccount.AssetTransactions.Count);
        }

        private static Account CreateTestAccount(string symbol, int shares, double balance, bool isPurchase)
        {
            return new Account
            {
                AssetTransactions = new List<AssetTransaction>
                {
                    new AssetTransaction
                    {
                        Asset = new Asset
                        {
                            Symbol = symbol

                        },
                        IsPurchase = isPurchase,
                        SharesAmount = shares,
                        DateProcessed = DateTime.Now
                    }
                },

                Balance = balance,
            };
        }
    }
}
