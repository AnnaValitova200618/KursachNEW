using Kursach.DTO;
using Kursach.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kursach.Model
{
    class SqlModel
    {
        private SqlModel() { }
        static SqlModel sqlModel;
        public static SqlModel GetInstance()
        {
            if (sqlModel == null)
                sqlModel = new SqlModel();
            return sqlModel;
        }

        internal List<Types> SelectType()
        {
            List<Types> types = new List<Types>();
            string query = "select * from type";
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        types.Add(new Types
                        {
                            ID = dr.GetInt32("id"),
                            type = dr.GetString("type"),
                            
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return types;
        }

        internal List<Carrying> SelectCarrying()
        {
            List<Carrying> carryings = new List<Carrying>();
            string query = "select * from carrying";
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        carryings.Add(new Carrying
                        {
                            ID = dr.GetInt32("id"),
                            carrying = dr.GetString("carrying"),

                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return carryings;
        }

        internal List<Coach> SelectAllCoach()
        {
            List<Coach> coachs = new List<Coach>();
            string query = "select * from couch";
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        coachs.Add(new Coach
                        {
                            ID = dr.GetInt32("id"),
                            FirstName = dr.GetString("firstname"),
                            LastName = dr.GetString("lastname"),
                            Patronymic = dr.GetString("patronymic"),
                            Phone = dr.GetString("phone"),
                            Birthday = dr.GetDateTime("bithday")
                            

                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return coachs;
        }

        //INSERT INTO `group` set title='1125', year = 2018;
        // возвращает ID добавленной записи
        public int Insert<T>(T value)
        {
            string table;
            List<(string, object)> values;
            GetMetaData(value, out table, out values);
            var query = CreateInsertQuery(table, values);
            var db = MySqlDB.GetDB();
            // лучше эти 2 запроса объединить в один с помощью транзакции
            int id = db.GetNextID(table);
            db.ExecuteNonQuery(query.Item1, query.Item2);
            return id;
        }
        // обновляет объект в бд по его id
        public void Update<T>(T value) where T : BaseDTO
        {
            string table;
            List<(string, object)> values;
            GetMetaData(value, out table, out values);
            var query = CreateUpdateQuery(table, values, value.ID);
            var db = MySqlDB.GetDB();
            db.ExecuteNonQuery(query.Item1, query.Item2);
        }

        public void Delete<T>(T value) where T : BaseDTO
        {
            var type = value.GetType();
            string table = GetTableName(type);
            var db = MySqlDB.GetDB();
            string query = $"delete from `{table}` where id = {value.ID}";
            db.ExecuteNonQuery(query);
        }

        public int GetNumRows(Type type)
        {
            string table = GetTableName(type);
            return MySqlDB.GetDB().GetRowsCount(table);
        }

        private static string GetTableName(Type type)
        {
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            return ((TableAttribute)tableAtrributes.First()).Table;
        }

        public List<Sportsman> SportsmanList()
        {
            var sportsman = new List<Sportsman>();
            var mySqlDB = MySqlDB.GetDB();
            string query = "select * from sportsman";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        sportsman.Add(new Sportsman
                        {
                            ID = dr.GetInt32("id"),
                            FirstName = dr.GetString("firstname"),
                            LastName = dr.GetString("lastname"),
                            Patronymic = dr.GetString("patronymic"),
                            Phone = dr.GetString("phone"),
                            Bithday = dr.GetDateTime("bithday"),
                            Sportscategory = dr.GetString("sportscategory"),
                            IdCoach = dr.GetInt32("id_couch")

                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return sportsman;
        }

        public List<Coach> CoachList()
        {
            var coachs = new List<Coach>();
            var mySqlDB = MySqlDB.GetDB();
            string query = "select * from couch";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        coachs.Add(new Coach
                        {
                            ID = dr.GetInt32("id"),
                            FirstName = dr.GetString("firstname"),
                            LastName = dr.GetString("lastname"),
                            Patronymic = dr.GetString("patronymic"),
                            Phone = dr.GetString("phone"),
                            Birthday = dr.GetDateTime("bithday")
                         
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return coachs;
        }

        public List<Vacation> ListVacation()
        {
            var vacations = new List<Vacation>();
            var mySqlDB = MySqlDB.GetDB();
            string query = "select * from vacation, couch where id_couch = couch.id";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        vacations.Add(new Vacation
                        {
                            ID = dr.GetInt32("id"),
                            Start = dr.GetDateTime("start"),
                            End = dr.GetDateTime("end"),
                            IdCouch = dr.GetInt32("id_couch"),
                            Coach = new Coach { ID = dr.GetInt32("id_couch") , LastName = dr.GetString("lastname")}
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return vacations;
        }

        private static (string, MySqlParameter[]) CreateInsertQuery(string table, List<(string, object)> values)//Создание записи в таблице
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"INSERT INTO `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static (string, MySqlParameter[]) CreateUpdateQuery(string table, List<(string, object)> values, int id)//Обновление записи в таблице
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            stringBuilder.Append($" WHERE id = {id}");
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static List<MySqlParameter> InitParameters(List<(string, object)> values, StringBuilder stringBuilder)//создает набор параметров к запросу. в каждом параметре значение для определенного столбца
        {
            var parameters = new List<MySqlParameter>();
            int count = 1;
            var rows = values.Select(s =>
            {
                parameters.Add(new MySqlParameter($"p{count}", s.Item2));
                return $"{s.Item1} = @p{count++}";
            });
            stringBuilder.Append(string.Join(',', rows));
            return parameters;
        }

        private static void GetMetaData<T>(T value, out string table, out List<(string, object)> values)//перебор свойств на объекте, для получения списка колонок, в которые потом пойдет запись
        {
            var type = value.GetType();
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            table = ((TableAttribute)tableAtrributes.First()).Table;
            values = new List<(string, object)>();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var columnAttributes = prop.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (columnAttributes.Length > 0)
                {
                    string column = ((ColumnAttribute)columnAttributes.First()).Column;
                    values.Add(new(column, prop.GetValue(value)));
                }
            }
        }
    }
}
