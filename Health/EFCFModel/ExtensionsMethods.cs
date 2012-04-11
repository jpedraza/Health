using System.Data.Entity;

namespace Model
{
    public static class ExDbSet
    {
        public static void AddRange(this DbSet set, params object[] objects)
        {
            foreach (object o in objects)
                set.Add(o);
        }

        public static void AddRange<T>(this DbSet<T> set, params T[] objects)
            where T : class
        {
            foreach (T o in objects)
                set.Add(o);
        }

        public static void DeleteRange(this DbSet set, params object[] objects)
        {
            foreach (object o in objects)
                set.Remove(o);
        }

        public static void DeleteRange<T>(this DbSet<T> set, params T[] objects)
            where T : class
        {
            foreach (T o in objects)
                set.Remove(o);
        }
    }
}