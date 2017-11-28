namespace DvdStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (Name, SignUpFee, DurationInMonths, DiscountRate)  VALUES ('Pay as you go',0,0,0)");
            Sql("INSERT INTO Membershiptypes (Name, SignUpFee, DurationInMonths, DiscountRate)  VALUES ('Monthly',30,1,10)");
            Sql("INSERT INTO Membershiptypes (Name, SignUpFee, DurationInMonths, DiscountRate)  VALUES ('Quarterly',90,3,15)");
            Sql("INSERT INTO Membershiptypes (Name, SignUpFee, DurationInMonths, DiscountRate)  VALUES ('Annual',300,12,20)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM MembershipTypes");
        }
    }
}
