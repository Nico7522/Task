using System.Data.Common;
using System.Reflection;

namespace Task.Database
{
    public static class DbConnectionExtensions
    {

        public static object? ExecuteScalar(this DbConnection connection, string query, object? parameters)
        {
            EnsureValidConnection(connection);
            using (DbCommand command = CreateCommand(connection, query, parameters))
            {
                object? result = command.ExecuteScalar();
                return (result is DBNull) ? null : result;
            }
        }
        public static IEnumerable<T> ExecuteReader<T>(this DbConnection connection, string query, Func<DbDataReader, T> mapper, object? parameters)
        {
            EnsureValidConnection(connection);
            List<T> result = new List<T>();
            using (DbCommand command = CreateCommand(connection, query, parameters))
            {
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(mapper(reader));
                }
            }
            return result;
        }

        public static int ExecuteNonQuery(this DbConnection connection, string query, object? parameters)
        {
            EnsureValidConnection(connection);
            using (DbCommand command = CreateCommand(connection, query, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }


        private static DbCommand CreateCommand(DbConnection connection, string query, object? parameters)
        {
            DbCommand command = connection.CreateCommand();
            command.CommandText = query;

            if (parameters is not null)
            {
                //Récupérer le type de l'objet
                Type parametersType = parameters.GetType();

                //Récupérer les propriétés accessible en lecture
                IEnumerable<PropertyInfo> properties = parametersType.GetProperties().Where(p => p.CanRead);

                //pour chacune d'entre elle, je vais récupérer le nom et sa valeur
                foreach (PropertyInfo property in properties)
                {
                    DbParameter parameter = command.CreateParameter();
                    //Je récupère le nom de la propriété comme nom de paramètre
                    parameter.ParameterName = property.Name;
                    //Je récpère la valeur de la propriété sur une instance spécifique de l'objet
                    parameter.Value = property.GetValue(parameters) ?? DBNull.Value;
                    //J'ajoute le paramètre à ma commande
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        private static void EnsureValidConnection(DbConnection connection)
        {
            ArgumentNullException.ThrowIfNull(connection);

            if (connection.State is not System.Data.ConnectionState.Open)
                throw new InvalidOperationException("The connection must be open for execute query...");
        }
    }
}
