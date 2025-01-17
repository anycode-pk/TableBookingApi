namespace TableBooking.Logic.Converters.TableConverters;

using Model.Dtos.TableDtos;
using Model.Models;

public interface ITableToGetConverter
{
    IEnumerable<GetTablesDto> TablesToTableDtos(IEnumerable<Table> tables);
    public GetTablesDto TableToTableDto(Table table);
}