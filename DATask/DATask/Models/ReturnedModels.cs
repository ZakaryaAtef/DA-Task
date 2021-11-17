using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DATask
{
    public class Transaction
    {
        public long createdAt { get; set; }
        public string name { get; set; }
        public string bank_name { get; set; }
        public string transfer_type { get; set; }
        public string receiving_amount { get; set; }
        public string status { get; set; }
        public string reference_number { get; set; }
        public string cf_number { get; set; }
        public string payout_location { get; set; }
        public string account_number { get; set; }
        public string paid_amount { get; set; }
        public string id { get; set; }
        public Transaction()
        {

        }
    }

}