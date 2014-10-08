using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarsBuilder.Shared
{
    public interface IBarsCreator
    {
        void AddQuote(DateTime dateTime, decimal price);

        int BarsCount { get; }

        //void Save(string filePath);

        string StoragePath { get; }

        void Finish();
    }
}
