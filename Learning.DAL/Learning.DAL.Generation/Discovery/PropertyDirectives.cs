﻿using System;

namespace Learning.DAL.Generation.Discovery {

    public class PropertyDirectives {
        public string Name { get; set; }
        public bool NullifyOnCreate { get; set; }
        public Type Type { get; set; }
    }

}
