using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PubControl.Models
{
    public class Publicacao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao {  get; set; }
        public string CodigoReferencia {  get; set; }
        public int IdTipoPublicacao { get; set; }
        public DateTime Data { get; set; }
        public string Observacao{ get; set; }
    }
}
