using CivEngage.API.Helpers;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class RepresentativeHelperTests
    {
        private RepresentativeHelper _helper;

        [SetUp]
        public void Setup()
        {
            _helper = new RepresentativeHelper();
        }

        [Test]
        public void ProcessGoogleResults_ReturnsFullyPopulatedRepresentative_GoogleAPIResultISWellFormed()
        {
            //Arrange
            var googleResult = GetValidGoogleApiResult();

            //Act
            var result = _helper.ProcessGoogleResult(googleResult).FirstOrDefault();

            //Assert
            Assert.IsTrue(result.Name == "Josiah Bartlett", $"Did not populate Name property correctly. Expected Josiah Bartlett but got {result.Name}");
            Assert.IsTrue(result.Party == "Democrat Party", $"Did not populate Party property correctly. Expected Democrat Party but got {result.Party}");
            Assert.IsTrue(result.PhoneNumber == "(111)-111-1111", $"Did not populate PhoneNumber property correctly. Expected (111)-111-1111 but got {result.PhoneNumber}");
            Assert.IsTrue(result.Email == "GoNotreDame@test.test", $"Did not populate Email property correctly. Expected GoNotreDame@test.test but got {result.Email}");
            Assert.IsTrue(result.Office == "President of the United States", $"Did not populate Office property correctly. Expected President of the United States but got {result.Office}");
            Assert.IsTrue(result.ImageUrl == "www.TheWestWing.fake", $"Did not populate ImageUrl property correctly. Expected www.TheWestWing.fake but got {result.ImageUrl}");
        }

        private string GetValidGoogleApiResult()
        {
            //Empty first three properties of the response since we don't use them, but we need them to exist
            var kind = "\"kind\":\"civicinfo#representativeInfoResponse\",";
            var normalizedInput = "\"normalizedInput\":{},";
            var divisions = "\"divisions\":{},";

            //These two properties we use a lot, I've left out the ones we don't use
            var offices = "\"offices\":[{\"name\":\"President of the United States\",\"divisionId\":\"ocd-division/country:us\",\"levels\":[\"country\"],\"roles\":[\"headOfState\",\"headOfGovernment\"],\"officialIndicies\":[0]}],";
            var officials = "\"officials\":[{\"name\":\"Josiah Bartlett\",\"party\":\"Democrat Party\",\"phones\":[\"(111)-111-1111\"],\"photoUrl\":\"www.TheWestWing.fake\",\"emails\":[\"GoNotreDame@test.test\"]}]";

            //Combine and return
            var validResult = $"{{{kind}{normalizedInput}{divisions}{offices}{officials}}}";
            return validResult;
        }
    }
}