using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileClient.Logic.Transport;
public interface IHttpClientFacade
{
    public Task<HttpResponseMessage> GetAsync(string url, HttpContent content = null!);

    public Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
}
