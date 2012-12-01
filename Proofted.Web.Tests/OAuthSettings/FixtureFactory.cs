using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Proofted.Web.Tests.OAuthSettings
{
    public class FixtureFactory
    {
        public static IFixture CreateFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            return fixture;
        }
    }
}