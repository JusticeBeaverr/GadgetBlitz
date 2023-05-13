using System.Collections;

namespace GadgetBlitzZTPAI.WebClient.Services.Navigation
{
    public interface INavigationService
    {
        Task<INavigationResult> NavigateAsync(Uri uri);
        Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters);
        Task<INavigationResult> NavigateAsync(string name);
        Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters);

    }

    public interface INavigationResult
    {
        bool Succes { get; }
        Exception Exception { get; }
    }

    public interface INavigationParameters : IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        int Count { get; }
        IEnumerable<string> Keys { get; }
        object this[string key] { get; }
        void Add(string key, object value);
        bool ContainsKey(string key);
        T GetValue<T>(string key);
        IEnumerable<T> GetValues<T>(string key);
        bool TryGetValue<T>(string key, out T value);
    }
}
