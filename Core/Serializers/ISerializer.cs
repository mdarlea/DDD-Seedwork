using System.IO;

namespace Swaksoft.Core.Serializers
{
    public interface ISerializer
    {
        string Serialize<TRequest>(TRequest request);

        TResponse Desearialize<TResponse>(Stream stream);
        TResponse Desearialize<TResponse>(string source);

        string ContentType { get; }
    }
}
