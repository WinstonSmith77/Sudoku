using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Mechanics.Cell;
using Mechanics.Field;
using System.Collections.Generic;
using Mechanics.Geometry;

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
                using (var compress = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    return (FieldManager)formatter.Deserialize(compress);
                }
            }
        }

        public void Save(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = File.OpenWrite(fileName))
            {
                using (var compress = new DeflateStream(stream, CompressionLevel.Optimal))
                {
                    formatter.Serialize(compress, this);
                }
            }
        }

        public IField SetCell(Point p, NumericValue value)
        {
            var tos = _fields.Peek();
            var newField = tos.SetCell(p, value);

            newField = ClearRange(p, value, newField);

            _fields.Push(newField);
            _redo.Clear();

            return newField;
        }

        private static IField ClearRange(Point p, NumericValue value, IField newField)
        {
            foreach (var rangeP in new Range(p))
            {
                if (newField[rangeP].IsDefined)
                {
                    continue;
                }
                newField = newField.ExcludeValueFromCell(rangeP, value);
                if (newField[rangeP].IsDefined)
                {
                    newField = ClearRange(rangeP, newField[rangeP].Value, newField);
                }
            }
            return newField;
        }

        public IField CurrentField
        {
            get
            {
                return _fields.Peek();
            }
        }

        public bool CanUndo()
        {
            return _fields.Count > 1;
        }

        public IField Undo()
        {
            _redo.Push(_fields.Pop());
            return _fields.Peek();
        }

        public bool CanRedo()
        {
            return _redo.Any();
        }

        public IField Redo()
        {
            var toRedo = _redo.Pop();
            _fields.Push(toRedo);

            return toRedo;
        }

        public bool CanReset()
        {
            return _fields.Count != 1 || _redo.Count != 0;
        }


        private readonly Stack<IField> _fields = new Stack<IField>();
        private readonly Stack<IField> _redo = new Stack<IField>();

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
