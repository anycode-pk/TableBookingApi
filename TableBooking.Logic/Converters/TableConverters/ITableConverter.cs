namespace TableBooking.Logic.Converters.TableConverters;

using Model.Dtos.TableDtos;
using Model.Models;

public interface ITableConverter
{
    IEnumerable<TableDto> TablesToTableDtos(IEnumerable<Table> tables);
    public TableDto TableToTableDto(Table table);
}