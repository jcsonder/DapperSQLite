namespace Persistence.Entities
{
    public class Highscore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, Name={Name}, Score={Score}";
        }
    }
}
