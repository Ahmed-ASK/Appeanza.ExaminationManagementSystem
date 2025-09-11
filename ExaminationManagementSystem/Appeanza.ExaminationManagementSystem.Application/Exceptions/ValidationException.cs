using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Application.Exceptions
{
    public class ValidationException(string message) : BadRequestException(message)
    {
        public required IEnumerable<string> Errors { get; set; }

    }
}
