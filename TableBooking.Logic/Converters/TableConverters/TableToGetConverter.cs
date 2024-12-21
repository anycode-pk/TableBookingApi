using TableBooking.Model.Dtos.TableDtos;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Converters.TableConverters
{
    public class TableToGetConverter : ITableToGetConverter
    { 
        public IEnumerable<GetTablesDto> TablesToTableDtos(IEnumerable<Table> tables)
        {
            var tablesDto = new List<GetTablesDto>();
            foreach (var table in tables)
            {
                tablesDto.Add(TableToTableDto(table));
            }
            return tablesDto;
        }

        public GetTablesDto TableToTableDto(Table table)
        {
            return new GetTablesDto
            {
                Id = table.Id,
                RestaurantId = table.RestaurantId,
                NumberOfSeats = table.NumberOfSeats,
                Bookings = table.Bookings
            };
        }
    }
}
