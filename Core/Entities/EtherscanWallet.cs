using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class EtherscanTransaction
    {

        public string From { get; set; }

        public string To { get; set; }

        public string Value { get; set; }

        public string Gas { get; set; }
    }

    public class EtherscanBalance
    {
        public string Result { get; set; }
    }

    public class EtherscanWallet
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public List<EtherscanTransaction> Result { get; set; }
    }
}
