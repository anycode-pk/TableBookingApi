using TableBooking.Model.Dtos.TableDtos;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Converters.TableConverters
{
    public interface ITableConverter
    {
        IEnumerable<TableDto> TablesToTableDtos(IEnumerable<Table> tables);
        public TableDto TableToTableDto(Table table);
    }
}
