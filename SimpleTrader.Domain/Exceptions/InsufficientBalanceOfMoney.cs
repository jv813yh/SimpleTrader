using System.Runtime.Serialization;

namespace SimpleTrader.Domain.Exceptions
{
    public class InsufficientBalanceOfMoney : Exception
    {
        double _balance;

        public InsufficientBalanceOfMoney(double balance)
        {
            _balance = balance;
        }

        public InsufficientBalanceOfMoney(double balance, string? message) : base(message)
        {
            _balance = balance;
        }

        public InsufficientBalanceOfMoney(double balance, string? message, Exception? innerException) : base(message, innerException)
        {
            _balance = balance;
        }
    }
}
