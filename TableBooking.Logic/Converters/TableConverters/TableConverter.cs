using TableBooking.Model.Dtos.RatingDtos;
using TableBooking.Model.Dtos.TableDtos;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Converters.TableConverters
{
    public class TableConverter : ITableConverter
    {
        public IEnumerable<TableDto> TablesToTableDtos(IEnumerable<Table> tables)
        {
            var tablesDto = new List<TableDto>();
            foreach (var table in tables)
            {
                tablesDto.Add(TableToTableDto(table));
            }
            return tablesDto;
        }
        public TableDto TableToTableDto(Table table)
        {
            return new TableDto
            {
                RestaurantId = table.RestaurantId,
                NumberOfSeats = table.NumberOfSeats,
            };
        }
    }
}