namespace AccountingProject.Services.Data.Tests
{
    using System;

    using AccountingProject.Web.Infrastructure.ValidationAttributes;
    using Xunit;

    public class CurrentDateMaxValueAttributeTests
    {
        [Fact]
        public void IsValidReturnsFalseForDateAfterCurrentDate()
        {
            var attribute = new CurrentDateMaxValueAttribute(2000);
            var isValid = attribute.IsValid(DateTime.UtcNow.AddDays(1));
            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForDateBeforeCurrentDate()
        {
            var attribute = new CurrentDateMaxValueAttribute(2000);
            var isValid = attribute.IsValid(DateTime.UtcNow.AddDays(-1));
            Assert.True(isValid);
        }

        [Fact]
        public void IsValidReturnsFalseForDateBeforeMinYear()
        {
            var attribute = new CurrentDateMaxValueAttribute(2000);
            var isValid = attribute.IsValid(new DateTime(1999, 12, 10));
            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForDateAfterMinYear()
        {
            var attribute = new CurrentDateMaxValueAttribute(2000);
            var isValid = attribute.IsValid(new DateTime(2000, 12, 10));
            Assert.True(isValid);
        }
    }
}
