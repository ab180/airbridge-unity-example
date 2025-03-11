using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AirbridgeTests")]
// Exclude a class from the document
/// @cond HIDDEN_SYMBOLS
static class AirbridgeNullCheck
{
    public class NullException : Exception
    {
        public NullException() : base() { }
        public NullException(string message) : base(message) { }
    }
    
    public static T RequireNonNull<T>(T value)
    {
        if (value == null)
        {
            throw new NullException("arg cannot be null");
        }
        return value;
    }
    
    public static void CallMethodWithNullCheck(Action action)
    {
        try
        {
            action();
        }
        catch (NullException) { /* Null Check */ }
    }
    
    public static T CallMethodWithNullCheck<T>(Func<T> func, T defaultValue)
    {
        try
        {
            return func();
        }
        catch (NullException) { /* Null Check */ }
        return defaultValue;
    }
}
// ReSharper disable once InvalidXmlDocComment
/// @endcond