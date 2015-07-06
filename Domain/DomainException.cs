using System;

namespace Swaksoft.Domain.Seedwork
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), Serializable]
    public class DomainServiceException : Exception
    {
        public DomainServiceException(string message) :base(message)
        {
        }

        public string Code { get; set; }

        public string MoreInfo { set; get; }
    }
}
