using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Unit.Model.Dto
{
    public class EncryptedDto
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public byte[] SecretValue { get; set; }
    }
}
