/// <summary>
/// Holds variable types of tracking-link that move user
/// to specific page of app and track click-event.
/// </summary>
public class AirbridgeTrackingLink
{
    /// A tracking-link url contains short mapping-id of option.
    public string ShortURL { get; }
    
    /// A qrcode image-url of tracking-link.
    public string QrcodeURL { get; }

    internal AirbridgeTrackingLink(string shortURL, string qrcodeURL)
    { 
        ShortURL = shortURL; 
        QrcodeURL = qrcodeURL;
    }
}