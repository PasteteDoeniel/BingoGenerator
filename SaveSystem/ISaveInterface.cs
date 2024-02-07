using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BingoGenerator
{
    interface ISaveInterface
    {
        public void Save(ref SaveDocument saveArchive);
        public void Load(SaveDocument loadArchive);
    }
}
