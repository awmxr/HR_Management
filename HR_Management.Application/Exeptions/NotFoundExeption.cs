using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Exeptions;

public class NotFoundExeption : ApplicationException
{
    public NotFoundExeption(string name , object key )
        : base($"{name} ({key}) not found.")
    {
        
    }
}
