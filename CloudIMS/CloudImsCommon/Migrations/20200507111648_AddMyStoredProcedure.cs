using System;
using CloudImsCommon.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.MySqlClient;

namespace CloudImsCommon.Migrations
{
    public partial class AddMyStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var mcon = AppDbContext.GetConnectionString();
            MySqlConnection conn = new MySqlConnection(mcon);
            conn.Open();
            var myscript = new MySqlScript(conn, @"
DELIMITER ;;
CREATE DEFINER=`tplims`@`%` FUNCTION `setAuto_number`(auto_type varchar(10)) RETURNS varchar(20) CHARSET utf8
BEGIN

            declare var_text_prefix varchar(3);
    declare var_date_prefix varchar(10);
	declare var_next_value varchar(20);
    declare int_next_value int;
	declare var_auto_length varchar(2);
    
   
    declare var_autono_current_year varchar(4);
    declare var_system_current_year varchar(4);
   
	declare return_val varchar(20);
    
    
	SELECT 
    an_text_prefix,
    an_date_prefix,
    LPAD(an_last_value + 1, an_auto_length, '0'),
    an_last_value + 1,
    an_auto_length,
    an_current_year,
    DATE_FORMAT(SYSDATE(), '%Y')
	INTO @var_text_prefix , @var_date_prefix , @var_next_value , @int_next_value , @var_auto_length , @var_autono_current_year , @var_system_current_year FROM
		auto_number
	WHERE
		an_type = auto_type
	FOR UPDATE;
	
    if @var_autono_current_year = @var_system_current_year then
		set @return_val := concat(@var_text_prefix, '|', @var_date_prefix, '|', @var_next_value);

		UPDATE auto_number
		SET 
			an_last_value = @int_next_value
		WHERE
			an_type = auto_type;
    else
		set @return_val := concat(@var_text_prefix, '|', @var_date_prefix, '|', lpad(1, @var_auto_length, '0'));

		UPDATE auto_number
		SET 
			an_last_value = 1,
			an_current_year = @var_system_current_year
		WHERE
			an_type = auto_type;
    end if; 

RETURN @return_val;

                END ;;
DELIMITER ; ");


            myscript.Execute();
            conn.Close();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropFunction = @"DROP FUNCTION setAuto_number";
            migrationBuilder.Sql(dropFunction);
        }
    }
}
