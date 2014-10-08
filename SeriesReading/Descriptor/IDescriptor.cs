using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeriesReading.Descriptor {
  public interface IDescriptor {
    string Name { get; }
    string Path { get; }
    List<IDescriptor> ChildDescriptors { get; }
  }
}
