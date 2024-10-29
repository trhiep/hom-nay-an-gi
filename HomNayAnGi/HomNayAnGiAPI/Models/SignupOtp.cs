using System;
using System.Collections.Generic;

namespace HomNayAnGiAPI.Models
{
    public partial class SignupOtp
    {
        public string SignupRequestId { get; set; } = null!;
        public string Otpstring { get; set; } = null!;
        public int RequestAttemptsRemains { get; set; }
        public DateTime ExpiresAt { get; set; }
        
        public override string ToString()
        {
            return $"SignupOtp [SignupRequestId={SignupRequestId}, Otpstring={Otpstring}, RequestAttemptsRemains={RequestAttemptsRemains}, ExpiresAt={ExpiresAt}]";
        }
    }
}
