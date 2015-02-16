
namespace Signa.Model
{
    public class SignSample
    {
        public double[] PalmNormal { get; set; }
        public double[] HandDirection { get; set; }
        public double[] AnglesBetweenFingers { get; set; }

        public double[] ToArray()
        {
            var array = new double[10];
            
            PalmNormal.CopyTo(array, 0);
            HandDirection.CopyTo(array, PalmNormal.Length);
            AnglesBetweenFingers.CopyTo(array, PalmNormal.Length + HandDirection.Length);

            return array;
        }
    }
}