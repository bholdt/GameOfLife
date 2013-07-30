namespace GameOfLife.Tests
{
    public class WorldBuilder
    {
        public static World Build(params string[] rows)
        {
            var world = new World();
            int y = 0;
            foreach(var row in rows)
            {
                int x = 0;
                foreach(var column in row.ToCharArray())
                {
                    if(column == '#')
                        world.AddCell(new Position(x,y));
                    x--;
                }
                y--;
            }
            return world;
        }
    }
}