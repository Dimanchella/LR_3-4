using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseAlpaca1
{
    public class Variable
    {
        private readonly string name;
        private readonly string type;

        public Variable(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        public string Name { get => name; }
        public string Type { get => type; }

        public override string ToString()
        {
            return $"{name}:{type}";
        }
    }
}
