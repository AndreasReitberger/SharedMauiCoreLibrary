using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreasReitberger.Shared.Core.Licensing.Interfaces
{
    public interface IActivationResponse
    {
        #region Properties

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        #endregion
    }
}
