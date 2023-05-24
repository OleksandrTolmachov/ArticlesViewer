using AutoFixture;
using AutoFixture.Xunit2;
using AutoFixture.AutoMoq;

public class AutoDomainDataAttribute : AutoDataAttribute
{
    public AutoDomainDataAttribute()
        : base(CreateFixture)
    {
    }

    public static IFixture CreateFixture()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization());
        fixture.Behaviors.Remove(fixture.Behaviors.OfType<ThrowingRecursionBehavior>().First());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture;
    }
}