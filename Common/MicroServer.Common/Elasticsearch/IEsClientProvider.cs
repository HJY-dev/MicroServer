using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServer.Common.Elasticsearch
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();
    }
}
