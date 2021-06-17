using System;
using WindesheimAD2021AutoVerzekeringsPremie.Implementation;
using Moq;
using Xunit;

namespace WindesheimAD2021AutoVerzekeringsPremieTest
{
    public class PremiumCalculationTest
    {
        /*
         * Het doel van deze unit test is om te kijken of de Waplus 
         * daadwerkelijk 20 procent duurder is. 
         * 
         * De data die ik heb gebruikt voor Vehicle is de data van een Tesla model X.
         * 
         * De data die ik heb gebruikt voor de Policyholder valt binnen de minder 
         * dan 5 jaar rijbewijs en ouder dan 23 jaar om te kijken of alles goed ging.
         */
        [Fact]
        public void WaPlus20ProcentMonthly()
        {
            //Arrange
            var FakeVehicle = new Vehicle(193, 120000, 2016);
            var FakePolicyHolder = new PolicyHolder(26, "13-09-2014", 1096, 3);

            //Act
            var monthlyCalculation = new PremiumCalculation(FakeVehicle, FakePolicyHolder, InsuranceCoverage.WA_PLUS);

            //Assert
            Assert.Equal(126.74, monthlyCalculation.PremiumPaymentAmount(PremiumCalculation.PaymentPeriod.MONTH));
        }

        /*
         * Het doel van deze unit test is om te kijken of het bedrag die je betaald klopt 
         * als je op een bepaalde leeftijd zit.
         * 
         * De data die ik heb gebruikt voor Vehicle is de data van een Tesla model S.
         * 
         * De data die ik heb gebruikt voor de Policyholder valt binnen de leeftijd range en buiten de 
         * leeftijd range. Zodat ik kan kijken of het bij beide situaties klopt. 
         */
        [Theory]
        [InlineData(4, 25, 800.08)]
        [InlineData(2, 21, 920.09)]
        public void WaAgeDifferencePaymentTest(int noClaimYears, int policyHolderAge, double Expected)
        {
            //Arrange
            var FakeVehicle = new Vehicle(165, 80000, 2014);
            var FakePolicyHolder = new PolicyHolder(policyHolderAge, "09-01-2013",4213,noClaimYears);

            //Act
            var doubleCalculation = new PremiumCalculation(FakeVehicle, FakePolicyHolder, InsuranceCoverage.WA);

            //Assert
            Assert.Equal(Expected, doubleCalculation.PremiumPaymentAmount(PremiumCalculation.PaymentPeriod.YEAR));
        }
    }
}