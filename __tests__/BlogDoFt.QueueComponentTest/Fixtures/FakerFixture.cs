using Bogus;

namespace BlogDoFt.QueueComponentTest.Fixtures
{
    public static class FakerFixture
    {
        private static Faker _faker;

        public static Faker GetFaker() => _faker ??= new Faker("pt_BR");
    }
}
