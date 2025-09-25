using NuGet.Protocol;

namespace WebAppAspLayered.Extensions;

public static class SessionExtensions
{
    public static void SetItem(this ISession session, string key, object o)
    {
        session.SetString(key, o.ToJson());
    }

    public static T? GetItem<T>(this ISession session, string key) where T : class
    {
        string? json = session.GetString(key);
        return json?.FromJson<T>();
    }
}
