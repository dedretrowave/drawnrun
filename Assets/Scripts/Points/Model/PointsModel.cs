namespace Points.Model
{
    public class PointsModel
    {
        private int _count;

        public int Count => _count;

        public void Add(int amount = 1)
        {
            _count += amount;
        }
    }
}