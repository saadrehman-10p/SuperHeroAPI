using SuperHeroAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace SuperHeroAPI.Services
{
    public class SuperHeroServices:ISuperHeroService
    {
        public string Conn { get; set; }
        public IConfiguration _configuaration { get; set; }
        public SqlConnection sqlConnection { get; set; }

        public SuperHeroServices( IConfiguration configuaration)
        {
          
            _configuaration = configuaration;
            Conn = _configuaration.GetConnectionString("DefaultConnection");
        }
        public  List<SuperHero> GetSuperHeroes()
        {   
             List <SuperHero> superHeroes=new List<SuperHero> ();
            try
            {
                using (sqlConnection = new SqlConnection(Conn))
                {
                    sqlConnection.Open();
                    var cmd = new SqlCommand("SP_GETSUPERHERO", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        SuperHero superHero = new SuperHero();
                        superHero.Id=Convert.ToInt32(rdr["ID"]);
                        superHero.Name = rdr["Name"].ToString();
                        superHero.FirstName = rdr["FirstName"].ToString();
                        superHero.LastName = rdr["LastName"].ToString();
                        superHero.Place = rdr["Place"].ToString();
                        superHeroes.Add(superHero);
                    }
                }
                return superHeroes;
            }
            catch
            {
                throw;
            }
        }
        public SuperHero GetSuperHeroById(int id)
        {
            SuperHero superHero = new SuperHero();
            try
            {
                using (sqlConnection = new SqlConnection(Conn))
                {
                    sqlConnection.Open();
                    var cmd = new SqlCommand("SP_SUPERHEROID", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value =id;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {


                        superHero.Id = Convert.ToInt32(rdr["ID"]);
                        superHero.Name = rdr["Name"].ToString();
                        superHero.FirstName = rdr["FirstName"].ToString();
                        superHero.LastName = rdr["LastName"].ToString();
                        superHero.Place = rdr["Place"].ToString();
                        return superHero;

                    }

                    return superHero;
                }
               
            }
            catch 
            {
                throw;
            }
        }
        public void PostSuperHero(SuperHeroDTO superhero)
        {

            try
            {
                using (sqlConnection = new SqlConnection(Conn))
                {
                    sqlConnection.Open();
                    var cmd = new SqlCommand("SP_PostSuperHero", sqlConnection);
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = superhero.Name;
                    cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = superhero.FirstName;
                    cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = superhero.LastName;
                    cmd.Parameters.Add("@place", SqlDbType.VarChar).Value = superhero.Place;
                    cmd.CommandType = CommandType.StoredProcedure;
             
                    cmd.ExecuteNonQuery();
                    
                    
                }

            }
            catch
            {
                throw;
            }
        }
        public void PutSuperHero(SuperHero superhero)
        {
            SuperHero SuperHero = new SuperHero();
            try
            {
                using (sqlConnection = new SqlConnection(Conn))
                {
                    sqlConnection.Open();
                    var cmd1 = new SqlCommand("SP_GETSUPERHERO", sqlConnection);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd1.ExecuteReader();
                   
                    while (rdr.Read())
                    {
                        SuperHero.Id = Convert.ToInt32(rdr["ID"]);
                        if (SuperHero.Id == superhero.Id)
                        {
                            rdr.Close();
                            sqlConnection.Open();
                            var cmd = new SqlCommand("SP_PutSuperHero", sqlConnection);
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = superhero.Id;
                            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = superhero.Name;
                            cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = superhero.FirstName;
                            cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = superhero.LastName;
                            cmd.Parameters.Add("@place", SqlDbType.VarChar).Value = superhero.Place;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            sqlConnection.Close();
                        }
                        
                     
                    }
                   

                }

            }
            catch 
            {
                throw;
            }
        }
        public void DeleteSuperHero(int id)
        {
            SuperHero SuperHero = new SuperHero();
            try
            {
                using (sqlConnection = new SqlConnection(Conn))
                {
                    sqlConnection.Open();
                    var cmd1 = new SqlCommand("SP_GETSUPERHERO", sqlConnection);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd1.ExecuteReader();

                    while (rdr.Read())
                    {
                        SuperHero.Id = Convert.ToInt32(rdr["ID"]);
                        if (SuperHero.Id == id)
                        {
                            rdr.Close();
                            sqlConnection.Open();
                            var cmd = new SqlCommand("SP_DeleteSuperHero", sqlConnection);
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            sqlConnection.Close();
                        }


                    }


                }

            }
            catch 
            {
                throw;
            }
        }

    }

    public interface ISuperHeroService
    {
        public List<SuperHero> GetSuperHeroes();
        public SuperHero GetSuperHeroById(int id);
        public void PostSuperHero(SuperHeroDTO superhero);
        public void PutSuperHero(SuperHero superhero);
        public void DeleteSuperHero(int id);
    }
}
