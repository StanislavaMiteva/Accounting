﻿namespace AccountingProject.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Accounting Software";

        public const string AdministratorRoleName = "Administrator";

        public const string ChiefAccountantRoleName = "Chief Accountant";

        public const string AccountantRoleName = "Accountant";

        public const string AllAccountantsRoleNames = "Chief Accountant, Accountant";

        public const int MinimumAllowedYearForDocumentDate = 2000;

        public const string ErrorMessageForExistingName = "This name already exists.";

        public const string ErrorMessageForExistingCode = "This code already exists.";

        public const string MaxDecimalValue = "79228162514264337593543950335";

        public const string MinAccountBalance = "0";

        public const string MinPrice = "0.01";

        public const string MinSalvageValue = "0.00";

        public const int MinUsefulLife = 1;

        public const int MaxUsefulLife = 100;

        public const int MonthsPerYear = 12;

        public const double DaysPerMonth = 30;
    }
}
