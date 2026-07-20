using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects;
    public record Priority
    {
        public int Value { get; }
        public string Name { get; }

        private Priority(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public static readonly Priority Low = new(1, "Low");
        public static readonly Priority Medium = new(2, "Medium");
        public static readonly Priority High = new(3, "High");

        public static Priority FromValue(int value)
        {
            return value switch
            {
                1 => Low,
                2 => Medium,
                3 => High,
                _ => throw new ArgumentException("Invalid priority value")
            };
        }

        public static Priority FromName(string name)
        {
            return name.ToLower() switch
            {
                "low" => Low,
                "medium" => Medium,
                "high" => High,
                _ => throw new ArgumentException("Invalid priority name")
            };
        }
    }