using System;
using System.Collections.Generic;
using System.Text;

namespace GeorgisGarage.Services.Contracts
{
    public interface IVideoService
    {
        string ReturnEmbedYoutubeLink(string url);
    }
}
