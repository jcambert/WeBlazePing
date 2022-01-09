namespace WePing.Domain
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false,Inherited =true)]
    public class IgnoreAttribute:Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class StringConverterAttribute : Attribute
    {
        public abstract string Convert(string value);
    }
    public class LowerCaseAttribute : StringConverterAttribute
    {
        public override string Convert(string value) =>
            Thread.CurrentThread.CurrentCulture.TextInfo.ToLower(value);

    }
    public class UpperCaseAttribute : StringConverterAttribute
    {
        public override string Convert(string value) =>
            Thread.CurrentThread.CurrentCulture.TextInfo.ToUpper(value);

    }
    public class NoneCaseAttribute : StringConverterAttribute
    {
        public override string Convert(string value) => value;
    }
}