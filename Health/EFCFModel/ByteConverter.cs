using System;
using System.Text;

namespace Model
{
    public class ByteConverter
    {
        public byte[] Get(object obj)
        {
            if (obj is int)
                return BitConverter.GetBytes(Convert.ToInt32(obj));

            if (obj is double || obj is decimal)
                return BitConverter.GetBytes(Convert.ToDouble(obj));

            if (obj is long)
                return BitConverter.GetBytes(Convert.ToInt64(obj));

            if (obj is string)
                return Encoding.UTF8.GetBytes(obj.ToString());

            if (obj is bool)
                return BitConverter.GetBytes(Convert.ToBoolean(obj));

            if (obj is DateTime)
                return BitConverter.GetBytes(Convert.ToDateTime(obj).ToBinary());

            throw new Exception(string.Format("Невозможно преобразовать тип {0} в массив байтов.", obj.GetType().FullName));
        }

        public T To<T>(byte[] bytes)
        {
            if (typeof(T) == typeof(int))
                return (T) (object)BitConverter.ToInt32(bytes, 0);

            if (typeof(T) == typeof(double) || typeof(T) == typeof(decimal))
                return (T) (object) BitConverter.ToDouble(bytes, 0);

            if (typeof(T) == typeof(long))
                return (T) (object) BitConverter.ToInt64(bytes, 0);

            if (typeof(T) == typeof(string))
                return (T) (object) Encoding.UTF8.GetString(bytes);

            if (typeof(T) == typeof(bool))
                return (T) (object) BitConverter.ToBoolean(bytes, 0);

            if (typeof(T) == typeof(DateTime))
                return (T) (object) DateTime.FromBinary(BitConverter.ToInt64(bytes, 0));

            throw new Exception(string.Format("Невозможно преобразовать массив байтов в {0}.", typeof (T).FullName));
        }
    }
}
