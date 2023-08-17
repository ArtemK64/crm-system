using System.Data;

namespace DAL.Interfaces
{
    public interface IHomeRepository
    {
        DataTable GetAllUsers();
        void UpdateUsers(int id, string firstName, string middleName, string lastName, DateTime birthday);
        DataTable GetAllOrders();
        void UpdateOrders(int id, int userId, int totalPrice, DateTime dateOfOrder);
    }
}