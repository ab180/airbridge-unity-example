/// <summary>
/// Enumeration of log levels used in [AirbridgeData](@ref AirbridgeData#logLevel).
/// </summary>
class AirbridgeLogLevel
{ 
    // [ Android ] https://github.com/ab180/airbridge-android-sdk/blob/main/airbridge/src/main/java/co/ab180/airbridge/AirbridgeLogLevel.kt
    // [ iOS     ] https://github.com/ab180/airbridge-ios-sdk/blob/main/Source/Core/Data/AirbridgeLogLevel.swift
    
    public static readonly string[] LogLevel = { "Debug", "Info", "Warning", "Error", "Fault" };
    
    /// <summary>
    /// @note Default is `Warning`
    /// </summary>
    public const int defaultLogLevel = 2;
}