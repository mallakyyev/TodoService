﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TodoBAL.Dtos
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}