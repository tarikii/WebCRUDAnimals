using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WebAnimals.DataAccessLayer.Connection;
using WebAnimals.Models;
using System.Web.Helpers;

public class AnimalRepository
{
    private SqlConnection _connection;

    public AnimalRepository()
    {
        ConnectionDatabase db = new ConnectionDatabase();
        _connection = db.SqlConnection;
    }


    public Animal GetAnimalDB(int idAnimal)
    {
        _connection.Open();

        try
        {
            string query = "SELECT * FROM Animal WHERE IdAnimal = @IdAnimal;";
            SqlCommand cmd = new SqlCommand(query, this._connection);

            cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);

            SqlDataReader records = cmd.ExecuteReader();

            if (records.Read())
            {
                Animal animal = new Animal();
                animal.IdAnimal = records.GetInt32(records.GetOrdinal("IdAnimal"));
                animal.NombreAnimal = records.GetString(records.GetOrdinal("NombreAnimal"));
                animal.Raza = records.GetString(records.GetOrdinal("Raza"));
                animal.RIdTipoAnimal = records.GetInt32(records.GetOrdinal("RIdTipoAnimal"));
                animal.FechaNacimiento = records.GetDateTime(records.GetOrdinal("FechaNacimiento"));

                return animal;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
        finally
        {
            _connection.Close();
        }
    }

    public List<Animal> GetAllAnimalsDB()
    {
        List<Animal> animals = new List<Animal>();

        _connection.Open();
        try
        {

            string query = "SELECT * FROM Animal;";
            SqlCommand cmd = new SqlCommand(query, this._connection);

            // Recuperamos un lector...
            SqlDataReader records = cmd.ExecuteReader();

            while (records.Read())
            {
                Animal animal = new Animal();
                animal.IdAnimal = records.GetInt32(records.GetOrdinal("IdAnimal"));
                animal.NombreAnimal = records.GetString(records.GetOrdinal("NombreAnimal"));
                animal.Raza = records.GetString(records.GetOrdinal("Raza"));
                animal.RIdTipoAnimal = records.GetInt32(records.GetOrdinal("RIdTipoAnimal"));
                animal.FechaNacimiento = records.GetDateTime(records.GetOrdinal("FechaNacimiento"));

                // Agrega más campos según la estructura de tu tabla y tu clase Animal
                animals.Add(animal);
            }
            _connection.Close();


        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        return animals;
    }

    public bool CreateAnimalDB(Animal animal)
    {
        if (_connection == null)
            throw new InvalidOperationException("Database connection is not initialized.");

        try
        {
            _connection.Open();

            string query = "INSERT INTO Animal (NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento) "
                + "VALUES (@NombreAnimal, @Raza, @RIdTipoAnimal, @FechaNacimiento);";

            using (SqlCommand cmd = new SqlCommand(query, this._connection))
            {
                cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                cmd.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento == DateTime.MinValue 
                    ? (object)DBNull.Value : animal.FechaNacimiento);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        catch (SqlException sqlEx)
        {
            // Log SQL exceptions
            Console.WriteLine("SQL Error: " + sqlEx.Message);
            return false;
        }
        catch (Exception ex)
        {
            // Log general exceptions
            Console.WriteLine("General Error: " + ex.Message);
            return false;
        }
        finally
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }


    public bool UpdateAnimalDB(Animal animal)
    {
        if (_connection == null)
            throw new InvalidOperationException("Database connection is not initialized.");

        _connection.Open();

        try
        {
            string query = "UPDATE Animal " +
                           "SET NombreAnimal = @NombreAnimal, Raza = @Raza, RIdTipoAnimal = @RIdTipoAnimal, FechaNacimiento = @FechaNacimiento " +
                           "WHERE IdAnimal = @IdAnimal;";

            SqlCommand cmd = new SqlCommand(query, this._connection);

            cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
            cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
            cmd.Parameters.AddWithValue("@Raza", animal.Raza);
            cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
            cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento);

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
        }
        finally
        {
            _connection.Close();
        }
    }

    public bool DeleteAnimalDB(int idAnimal)
    {
        if (_connection == null)
            throw new InvalidOperationException("Database connection is not initialized.");

        _connection.Open();

        try
        {
            string query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal;";

            SqlCommand cmd = new SqlCommand(query, this._connection);

            cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
        }
        finally
        {
            _connection.Close();
        }
    }
}
