using HolaMundo.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;

namespace HolaMundo.Controllers
{
    public class ValuesController : ApiController
    {
        string myConnectionString;

        public ValuesController()
        {
            this.myConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=desarrollo";
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { DataTableToJSON(selectData()) };
        }

        // GET api/values/5
        [Route("api/getuser"), HttpGet]

        public string Get([FromBody] Model model)
        {
            return model.id.ToString();
        }

        // POST api/values          INSERT
        [Route("api/newuser"), HttpPost]

        public void Post([FromBody] Model model)
        {
            insertData( model.first, model.last);
        }

        // PUT api/values/5         UPDATE
        public void Put(int id, [FromBody]string first, [FromBody]string last)
        {
            updateData(id, first, last);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            deleteData(id);
        }

        private void insertData(string first, string last)
        {
            try
            {
                string Query = "insert into users(Id,FirstName,LastName) values(null ,'"+first+"','"+last+"');";
                MySqlConnection MyConn2 = new MySqlConnection(myConnectionString);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void updateData(int id, string first, string last)
        {
            try
            {
                string Query = "update users set FirstName='" + first + "',LastName='" + last + "' where Id=" + id + ";";
                MySqlConnection MyConn2 = new MySqlConnection(myConnectionString);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void deleteData(int id)
        {
            try
            {
                string Query = "delete from users where Id=" + id + ";";
                MySqlConnection MyConn2 = new MySqlConnection(myConnectionString);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private DataTable selectData()
        {
            DataTable dTable = new DataTable();
            try
            {
                string Query = "select * from users;";
                MySqlConnection MyConn2 = new MySqlConnection(myConnectionString);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MyConn2.Open();  
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                MyAdapter.Fill(dTable);
                MyConn2.Close();
                return dTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return dTable;
            }
        }

        public static string DataTableToJSON(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = (Convert.ToString(row[col]));
                }
                list.Add(dict);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(list);
        }

        [DataContract]
        public class Model
        {
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string first { get; set; }

            [DataMember]
            public string last { get; set; }
        }
    }
}
