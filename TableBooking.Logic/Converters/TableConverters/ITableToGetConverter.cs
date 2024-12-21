using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableBooking.Model.Dtos.TableDtos;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Converters.TableConverters
{
    public interface ITableToGetConverter
    {
        IEnumerable<GetTablesDto> TablesToTableDtos(IEnumerable<Table> tables);
        public GetTablesDto TableToTableDto(Table table);
    }
}
