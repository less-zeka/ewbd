namespace EwBD_WebApi.Models
{
    public class Vector3
    {
        public Vector3() : this(0, 0, 0){}

        public Vector3(double x, double y , double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }
}