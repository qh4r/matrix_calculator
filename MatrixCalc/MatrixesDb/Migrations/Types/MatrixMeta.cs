namespace MatrixesDb.Migrations.Types
{
    public class MatrixMeta
    {
        public MatrixMeta(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public long Id { get; }
        public string Name { get; }
    }
}
