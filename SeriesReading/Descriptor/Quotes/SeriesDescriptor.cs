using FinancialConfiguration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SeriesReading.Descriptor.Quotes {
  public class SeriesDescriptor : IDescriptor {
    private string _rootFolder;
    private List<InstrumentDescriptor> _instrumentDescriptors = new List<InstrumentDescriptor>();
    public SeriesDescriptor() {
      _rootFolder = Configuration.Instace.DataRoot;
      Name = new DirectoryInfo(_rootFolder).Name;
      Directory.EnumerateDirectories(_rootFolder).ToList().ForEach(instrumentFolder => {
        _instrumentDescriptors.Add(new InstrumentDescriptor(instrumentFolder));
      });
    }
    public string Name { get; private set; }
    public string Path { get { return _rootFolder; } }
    public List<IDescriptor> ChildDescriptors {
      get { return _instrumentDescriptors.Cast<IDescriptor>().ToList(); }
    }
    public List<InstrumentDescriptor> InstrumentDescriptors { get { return _instrumentDescriptors; } }
  }
}
