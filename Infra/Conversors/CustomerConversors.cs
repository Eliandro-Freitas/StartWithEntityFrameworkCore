using DominandoEfCore.Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEfCore.Infra.Conversors
{
    public class CustomerConversors : ValueConverter<Status, string>
    {
        public CustomerConversors() : base( 
            x => ConverterToDatabase(x),
            value => ConverterToApplication(value))
        {

        }

        static string ConverterToDatabase(Status status)
            => status.ToString()[0..1];

        static Status ConverterToApplication(string value)
        {
            var status = Enum.GetValues<Status>().FirstOrDefault(x => x.ToString()[0..1] == value);
                return status;
        }
    }
}
