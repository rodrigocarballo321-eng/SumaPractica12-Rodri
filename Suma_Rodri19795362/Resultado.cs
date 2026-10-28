using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suma_Rodri19795362
{
    [Table("resultado")]
    public class Resultado
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        
        public int Id { get; set; }

        [Column("numero1")]
       
        public string? Numero1 { get; set; }

        [Column("numero2")]
        
        public string? Numero2 { get; set; }

        [Column("suma")]
     
        public string? Suma { get; set; }
    }
}
