﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProduto.Models
{
    [Table("PRODUTO_FAVORITO")]
    public class Favorito
    {
        [PrimaryKey(), Column("ID")]
        public int Id { get; set; }
    }
}
