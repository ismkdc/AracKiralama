using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace AracKiralama.Models
{
    public class AccessManager
    {
        readonly string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|db.mdb;";
        private OleDbConnection connection;

        public AccessManager()
        {
            connection = new OleDbConnection(connectionString);
        }
        private void Open()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public List<Car> GetAllCars()
        {
            Open();
            var cmd = new OleDbCommand("select * from Cars", connection);

            var cars = new List<Car>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    cars.Add(new Car
                    {
                        Id = (int)reader["Id"],
                        Brand = reader["Brand"].ToString(),
                        Model = reader["Model"].ToString(),
                        Price = (decimal)reader["Price"],
                        ProductionYear = (int)reader["ProductionYear"],
                        ImageUrl = reader["ImageUrl"].ToString()
                    });
                }
            }
            return cars;
        }

        public void InsertCar(Car car)
        {
            Open();
            var cmd = new OleDbCommand("insert into Cars(Brand, Model, ProductionYear, Price, ImageUrl) values(@Brand, @Model, @ProductionYear, @Price, @ImageUrl)", connection);
            cmd.Parameters.AddWithValue("@Brand", car.Brand);
            cmd.Parameters.AddWithValue("@Model", car.Model);
            cmd.Parameters.AddWithValue("@ProductionYear", car.ProductionYear);
            cmd.Parameters.AddWithValue("@Price", car.Price);
            cmd.Parameters.AddWithValue("@ImageUrl", car.ImageUrl);
            cmd.ExecuteNonQuery();
        }

        public void UpdateCar(Car car)
        {
            Open();
            var cmd = new OleDbCommand("update Cars set Brand=@Brand, Model=@Model, ProductionYear=@ProductionYear, Price=@Price, ImageUrl=@ImageUrl where Id=@Id", connection);
            cmd.Parameters.AddWithValue("@Brand", car.Brand);
            cmd.Parameters.AddWithValue("@Model", car.Model);
            cmd.Parameters.AddWithValue("@ProductionYear", car.ProductionYear);
            cmd.Parameters.AddWithValue("@Price", car.Price);
            cmd.Parameters.AddWithValue("@ImageUrl", car.ImageUrl);
            cmd.Parameters.AddWithValue("@Id", car.Id);
            cmd.ExecuteNonQuery();
        }

        public void DeleteCar(int id)
        {
            Open();
            var cmd = new OleDbCommand("delete from Cars where Id=@Id", connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}