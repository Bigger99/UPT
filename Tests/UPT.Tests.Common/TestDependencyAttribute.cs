namespace UPT.Common;

[AttributeUsage(AttributeTargets.Field)]
public class TestDependencyAttribute(bool shouldRegister = false) : Attribute
{
    public bool ShouldRegister { get; } = shouldRegister;
}
