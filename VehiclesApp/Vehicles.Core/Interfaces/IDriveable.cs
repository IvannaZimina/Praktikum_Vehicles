namespace Vehicles.Core.Interfaces
{
    // public — accessible from other projects
    // interface — a contract that defines WHAT an object must do, but not HOW to do it
    public interface IDriveable
    {
        // Method signature: return type (string), name (Move), and parameter (double km)
        string Move(double km);
    }
}