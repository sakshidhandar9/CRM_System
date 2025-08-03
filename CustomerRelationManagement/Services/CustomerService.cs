using CustomerRelationManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CustomerRelationManagement.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly string _connectionString;
        public CustomerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Location> GetLocations()
        {
            var list = new List<Location>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT LocationId, LocationName FROM Location";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Location
                    {
                        LocationId = (int)reader["LocationId"],
                        LocationName = reader["LocationName"].ToString()
                    });
                }
            }
            return list;
        }

        public List<Budget> GetBudgetOptions()
        {
            var list = new List<Budget>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT BudgetId, BudgetName FROM Budget";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Budget
                    {
                        BudgetId =(int)reader["BudgetId"],
                        BudgetName = reader["BudgetName"].ToString()
                    });
                }
            }

            return list;
        }

        public List<PreferredLocation> GetPreferredLocations()
        {
            var list = new List<PreferredLocation>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT PreferredLocationId,PreferredLocationName  FROM PreferredLocation";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PreferredLocation
                    {
                        PreferredLocationId = (int)reader["PreferredLocationId"],
                        PreferredLocationName = reader["PreferredLocationName"].ToString()
                    });
                }
            }
            return list;
        }

        public List<Builder> GetBuilders()
        {
            var list = new List<Builder>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT BuilderId, BuilderName FROM Builder";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Builder
                    {
                        BuilderId = (int)reader["BuilderId"],
                        BuilderName = reader["BuilderName"].ToString()
                    });
                }
            }
            return list;
        }

        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT ProjectId, ProjectName, BuilderId FROM Project";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(new Project
                        {
                            ProjectId = Convert.ToInt32(reader["ProjectId"]),
                            ProjectName = reader["ProjectName"].ToString(),
                            BuilderId = Convert.ToInt32(reader["BuilderId"])
                        });
                    }
                }
            }

            return projects;
        }


        public List<Project> GetProjectsByBuilderId(int builderId)
        {
            var list = new List<Project>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT ProjectId, ProjectName FROM Project WHERE BuilderId = @BuilderId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BuilderId", builderId);
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Project
                    {
                        ProjectId = (int)reader["ProjectId"],
                        ProjectName = reader["ProjectName"].ToString()
                    });
                }
            }
            return list;
        }

        

        public void AddCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@LocationId", customer.LocationId);
                    //cmd.Parameters.AddWithValue("@BudgetId", customer.BudgetId);
                    cmd.Parameters.AddWithValue("@PreferredLocationId", customer.PreferredLocationId);
                    cmd.Parameters.AddWithValue("@BuilderId", customer.BuilderId);
                    cmd.Parameters.AddWithValue("@ProjectId", customer.ProjectId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public List<CustomerViewModel> GetAllCustomersDetails()
        {
            var customers = new List<CustomerViewModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllCustomersWithDetails", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new CustomerViewModel
                            {
                                CustomerId = reader["CustomerId"] != DBNull.Value ? Convert.ToInt32(reader["CustomerId"]) : 0,
                                CustomerName = reader["CustomerName"]?.ToString(),

                                LocationId = reader["LocationId"] != DBNull.Value ? Convert.ToInt32(reader["LocationId"]) : 0,
                                LocationName = reader["LocationName"]?.ToString(),

                                PreferredLocationId = reader["PreferredLocationId"] != DBNull.Value ? Convert.ToInt32(reader["PreferredLocationId"]) : 0,
                                PreferredLocationName = reader["PreferredLocationName"]?.ToString(),

                                BuilderId = reader["BuilderId"] != DBNull.Value ? Convert.ToInt32(reader["BuilderId"]) : 0,
                                BuilderName = reader["BuilderName"]?.ToString(),

                                ProjectId = reader["ProjectId"] != DBNull.Value ? Convert.ToInt32(reader["ProjectId"]) : 0,
                                ProjectName = reader["ProjectName"]?.ToString()
                            });
                        }
                    }
                }
            }

            return customers;
        }

    }
}






