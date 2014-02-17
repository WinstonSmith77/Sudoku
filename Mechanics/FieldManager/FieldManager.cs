using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Mechanics.Cell;
using Mechanics.Field;
using System.Collections.Generic;

namespace Mechanics.FieldManager
{
    [Serializable]
    public class FieldManager : IFieldManager
    {
        internal static FieldManager Load(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = File.OpenRead(fileName))
            {
                return (FieldManager)formatter.Deserialize(stream);
            }
        }

        public void Save(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = File.OpenWrite(fileName))
            {
                formatter.Serialize(stream, this);
            }
        }

        public IField SetCell(Point p, NumericValue value)
        {
            var tos = _fields.Peek();
            var newField = tos.SetCell(p, value);
            _fields.Push(newField);

            return newField;
        }

        public IField CurrentField
        {
            get
            {
                return (IField)_fields.Peek().Clone();
            }
        }


        private readonly Stack<IField> _fields = new Stack<IField>();

        public static IFieldManager CreateEmptyFieldManager()
        {
            return new FieldManager();
        }

        private FieldManager()
        {
            var field = Factory.Instance.CreateEmptyField();
            _fields.Push(field);
        }

        public override bool Equals(object obj)
        {
            var other = obj as FieldManager;
            return other != null && SameContent(this, other);
        }

        private static bool SameContent(FieldManager fieldManager, FieldManager other)
        {
            var fieldsA = fieldManager._fields.ToList();
            var fieldsB = other._fields.ToList();

            if (fieldsA.Count != fieldsB.Count)
            {
                return false;
            }

            for (int i = 0; i < fieldsA.Count; i++)
            {
                if (!fieldsA[i].Equals(fieldsB[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _fields.Count;
        }
    }
}
