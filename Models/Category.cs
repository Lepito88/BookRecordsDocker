﻿using System;
using System.Collections.Generic;

namespace BookRecords.Models
{
    public partial class Category
    {
        public int Idcategory { get; set; }
        public string Name { get; set; } = null!;

        public virtual BookCategory? BookCategory { get; set; }
    }
}