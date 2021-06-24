using SimpleMigrations;

namespace ClayTest.Migrations.Migrations
{
    [Migration(2021_06_21_23_30, "Init companies DB")]
    public class __2021_06_21_23_30_Init : Migration
    {
        protected override void Up()
        {
            Execute(@"

				create table [Companies]
				(
					[Id] [uniqueidentifier] not null constraint PK_Companies primary key clustered,
					[Name] [nvarchar](256) not null
				);

				create table [Doors]
				(
					[Id] [uniqueidentifier] not null constraint PK_Doors primary key clustered,
					[CompanyId] [uniqueidentifier] not null constraint FK_Doors_CompanyId foreign key (CompanyId)
																									references [Companies] (Id),
					[Name] [nvarchar](256) not null,
					[Closed] [bit] not null,
					[Latitude] decimal(8,6) not null,
					[Longitude] decimal(8,6) not null,
					[Deleted] [datetime2](0) null,
					index IX_Doors_CompanyId nonclustered (CompanyId)
				);

				create table [DoorEventLogs]
				(
					[Id] [uniqueidentifier] not null constraint PK_DoorEventLogs primary key clustered,
					[DoorId] [uniqueidentifier] not null constraint FK_DoorEventLogs_DoorId foreign key (DoorId)
																									references [Doors] (Id),
					[UserId] [uniqueidentifier] not null,
					[Event] [nvarchar](256) not null,
					[Created] [datetime2](0) not null,
					index IX_DoorEventLogs_DoorId nonclustered (DoorId)
				);

				insert into Companies (Id, Name)
				values (newid(), N'CLAY SOLUTIONS B.V.')
				
				insert into Doors (Id, Name, Closed, Latitude, Longitude, CompanyId)
				values	(newid(), 'Office Tunnel Door',	0,	52.3714600, 4.9087600,	(select top 1 Id from Companies)),
						(newid(), 'Office Main Door',	0,	52.3714400, 4.9087400,	(select top 1 Id from Companies));
			");
        }

        protected override void Down()
        {
            Execute(@"
				drop table DoorEventLogs
				drop table Doors
				drop table Companies
			");
        }
    }
}
