using OrchardCore.Data.Migration;

namespace OrchardCore.Cms.BookingModule.Migrations;

public class EventBookingMigrations : DataMigration
{
    public int Create()
    {
        SchemaBuilder.CreateTable("EventBooking_Booking", table => table
            .Column<int>("Id", column => column.PrimaryKey().Identity())
            .Column<int>("EventBookingContentItemId")
            .Column<string>("UserId")
            .Column<DateTime>("BookingDate")
        );

        // No need to create table for EventBooking content part as Orchard Core handles it

        return 1;
    }
}