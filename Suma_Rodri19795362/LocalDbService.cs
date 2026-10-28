using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suma_Rodri19795362
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_suma.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            //Le indica al sistema que crea la tabla de nuestro contexto
            _connection.CreateTableAsync<Resultado>();
        }
   

    public async Task<List<Resultado>> GetResultado()
        {
            return await _connection.Table<Resultado>().ToListAsync();
        }

        //metodo para listar los registro por id

        public async Task<Resultado> GetById(int id)
        {
            return await _connection.Table<Resultado>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Metodo para crear registro

        public async Task Create(Resultado resultado)
        {
            await _connection.InsertAsync(resultado);
        }

        //Metodo para actualizar

        public async Task Update(Resultado resultado)
        {
            await _connection.UpdateAsync(resultado);
        }

        //Metodo para eliminar

        public async Task Delete(Resultado resultado)
        {
            await _connection.DeleteAsync(resultado);
        }
    }
}