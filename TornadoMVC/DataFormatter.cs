namespace TornadoMVC
{
    public class DataFormatter
    {
        public static string PrettyCost(decimal cost)
        {
            string result;

            // если стоимость имеет копейки
            if ((double)cost - (int)cost >= 0.01)
                result = Math.Round(cost, 2).ToString();
            else
                result = ((int)cost).ToString();

            return result;
        }
    }
}
