using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using BookingServiceApi.Data;

namespace BookingServiceApi.Repo
{
    public class ServiceBookingSQL : IDisposable
    {

        public void Dispose()
        {
            GC.SuppressFinalize((object)this);
        }
        private string db = "Server=tcp:abbitsofttest.database.windows.net,1433;Initial Catalog=ObjectDataTest;Persist Security Info=False;User ID=AllanFrost;Password=@ksanaF1978;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<ServiceBookingRDto> GetFutureServiceBooking(long CustomerID)
        {
            List<ServiceBookingRDto> serviceBookingRdtoList = new List<ServiceBookingRDto>();
            DataTable dataTable = new DataTable("Result");
            List<ServiceBookingRDto> futureServiceBooking;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.db))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("SELECT [ID],[Status],[AddressLine],[ExecuteDateTime],[Phone],[ServicesCsv],[Paid],[PaymentType],[TotalPrice],[Currency],[SystemValue],[LastChanged],[ChangedBy] FROM [dbo].[FutureServiceBooking] where CustomerID = " + CustomerID.ToString(), sqlConnection))
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                            ((DbDataAdapter)sqlDataAdapter).Fill(dataTable);
                    }
                  ((DbConnection)sqlConnection).Close();
                }
                futureServiceBooking = this.MapDataToFutureServiceBooking(dataTable);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Get Future Service Booking Error.", ex);
            }
            return futureServiceBooking;
        }

        public string CreateServiceBooking(ServiceBookingCDto serviceBooking)
        {
            int result = 0;
            Guid sbGuid;
            try
            {
                if (serviceBooking.Status == null || serviceBooking.Status.Trim() == "" || serviceBooking.CustomerID < 1 || serviceBooking.ISCountry == null || serviceBooking.ISCountry.Trim() == "" || serviceBooking.ISAddressLine == null || serviceBooking.ISAddressLine.Trim() == "" || serviceBooking.ExecuteDateTime == DateTimeOffset.MinValue || serviceBooking.Phone == null || serviceBooking.Phone.Trim() == "" || serviceBooking.ServicesCsv == null || serviceBooking.ServicesCsv.Trim() == "" || serviceBooking.PaymentType == null || serviceBooking.PaymentType.Trim() == "" || serviceBooking.TotalPrice == null || serviceBooking.TotalPrice.Trim() == "" || serviceBooking.Currency == null || serviceBooking.Currency.Trim() == "" || serviceBooking.SystemValue == null || serviceBooking.SystemValue.Trim() == "" || serviceBooking.ChangedBy == null || serviceBooking.ChangedBy.Trim() == "")
                    return "Invalid ServiceBooking data.";
                sbGuid = serviceBooking.SBGuid;
                if (sbGuid.ToString() == "00000000-0000-0000-0000-000000000000")
                    serviceBooking.SBGuid = Guid.NewGuid();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Validate ServiceBooking data Error", ex);
            }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.db))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("dbo.InsertServiceBooking", sqlConnection))
                    {
                        ((DbConnection)sqlConnection).Open();
                        ((DbCommand)sqlCommand).CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@SBGuid", SqlDbType.NVarChar, 128);
                        sqlCommand.Parameters.Add("@Status", SqlDbType.NVarChar, 20);
                        sqlCommand.Parameters.Add("@CompanyID", SqlDbType.BigInt);
                        sqlCommand.Parameters.Add("@CustomerID", SqlDbType.BigInt);
                        sqlCommand.Parameters.Add("@ISCountry", SqlDbType.NVarChar, 10);
                        sqlCommand.Parameters.Add("@ISAddressLine", SqlDbType.NVarChar, 300);
                        sqlCommand.Parameters.Add("@ExecuteDateTime", SqlDbType.DateTimeOffset, 7);
                        sqlCommand.Parameters.Add("@Phone", SqlDbType.NVarChar, 20);
                        sqlCommand.Parameters.Add("@ServicesCsv", SqlDbType.NVarChar, -1);
                        sqlCommand.Parameters.Add("@Paid", SqlDbType.Bit);
                        sqlCommand.Parameters.Add("@PaymentType", SqlDbType.NVarChar, 30);
                        sqlCommand.Parameters.Add("@TotalPrice", SqlDbType.VarChar, 20);
                        sqlCommand.Parameters.Add("@Currency", SqlDbType.VarChar, 10);
                        sqlCommand.Parameters.Add("@ISSystemValue", SqlDbType.VarChar, 50);
                        sqlCommand.Parameters.Add("@ISChangedBy", SqlDbType.NVarChar, 256);
                        sqlCommand.Parameters.Add("@DObj", SqlDbType.NVarChar, -1);
                        sqlCommand.Parameters.Add("@CallWebApi", SqlDbType.NVarChar, 200);
                        SqlParameter parameter = sqlCommand.Parameters["@SBGuid"];
                        sbGuid = serviceBooking.SBGuid;
                        string str = sbGuid.ToString();
                        ((DbParameter)parameter).Value = (object)str;
                        ((DbParameter)sqlCommand.Parameters["@Status"]).Value = (object)serviceBooking.Status;
                        ((DbParameter)sqlCommand.Parameters["@CompanyID"]).Value = (object)serviceBooking.CompanyID;
                        ((DbParameter)sqlCommand.Parameters["@CustomerID"]).Value = (object)serviceBooking.CustomerID;
                        ((DbParameter)sqlCommand.Parameters["@ISCountry"]).Value = (object)serviceBooking.ISCountry;
                        ((DbParameter)sqlCommand.Parameters["@ISAddressLine"]).Value = (object)serviceBooking.ISAddressLine;
                        ((DbParameter)sqlCommand.Parameters["@ExecuteDateTime"]).Value = (object)serviceBooking.ExecuteDateTime;
                        ((DbParameter)sqlCommand.Parameters["@Phone"]).Value = (object)serviceBooking.Phone;
                        ((DbParameter)sqlCommand.Parameters["@ServicesCsv"]).Value = (object)serviceBooking.ServicesCsv;
                        ((DbParameter)sqlCommand.Parameters["@Paid"]).Value = (object)serviceBooking.Paid;
                        ((DbParameter)sqlCommand.Parameters["@PaymentType"]).Value = (object)serviceBooking.PaymentType;
                        ((DbParameter)sqlCommand.Parameters["@TotalPrice"]).Value = (object)serviceBooking.TotalPrice;
                        ((DbParameter)sqlCommand.Parameters["@Currency"]).Value = (object)serviceBooking.Currency;
                        ((DbParameter)sqlCommand.Parameters["@ISSystemValue"]).Value = (object)serviceBooking.SystemValue;
                        ((DbParameter)sqlCommand.Parameters["@ISChangedBy"]).Value = (object)serviceBooking.ChangedBy;
                        ((DbParameter)sqlCommand.Parameters["@DObj"]).Value = (object)JsonConvert.SerializeObject((object)serviceBooking);
                        ((DbParameter)sqlCommand.Parameters["@CallWebApi"]).Value = (object)nameof(CreateServiceBooking);
                        if (!int.TryParse(((DbCommand)sqlCommand).ExecuteScalar().ToString(), out result))
                            result = -1;
                        ((DbConnection)sqlConnection).Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Create Service Booking Error", ex);
            }
            return result == -1 ? "Already Exist" : "OK";
        }

        public string ManagerUpdateServiceBooking(ServiceBookingUDto serviceBooking)
        {
            int result = 0;
            try
            {
                if (serviceBooking.Status == null || serviceBooking.Status.Trim() == "" || serviceBooking.ID < 1 || serviceBooking.ExecuteDateTime == DateTimeOffset.MinValue || serviceBooking.TotalPrice == null || serviceBooking.TotalPrice.Trim() == "" || serviceBooking.Currency == null || serviceBooking.Currency.Trim() == "" || serviceBooking.SystemValue == null || serviceBooking.SystemValue.Trim() == "" || serviceBooking.ChangedBy == null || serviceBooking.ChangedBy.Trim() == "")
                    return "Invalid ServiceBooking data.";
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Validate ServiceBooking data Error", ex);
            }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.db))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("dbo.UpdStatExecdateMoney", sqlConnection))
                    {
                        ((DbConnection)sqlConnection).Open();
                        ((DbCommand)sqlCommand).CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@SBID", SqlDbType.BigInt);
                        sqlCommand.Parameters.Add("@Status", SqlDbType.NVarChar, 20);
                        sqlCommand.Parameters.Add("@ExecuteDateTime", SqlDbType.DateTimeOffset, 7);
                        sqlCommand.Parameters.Add("@TotalPrice", SqlDbType.VarChar, 20);
                        sqlCommand.Parameters.Add("@Currency", SqlDbType.VarChar, 10);
                        sqlCommand.Parameters.Add("@SystemValue", SqlDbType.VarChar, 50);
                        sqlCommand.Parameters.Add("@ChangedBy", SqlDbType.NVarChar, 256);
                        sqlCommand.Parameters.Add("@DObj", SqlDbType.NVarChar, -1);
                        sqlCommand.Parameters.Add("@CallWebApi", SqlDbType.NVarChar, 200);
                        ((DbParameter)sqlCommand.Parameters["@SBID"]).Value = (object)serviceBooking.ID;
                        ((DbParameter)sqlCommand.Parameters["@Status"]).Value = (object)serviceBooking.Status;
                        ((DbParameter)sqlCommand.Parameters["@ExecuteDateTime"]).Value = (object)serviceBooking.ExecuteDateTime;
                        ((DbParameter)sqlCommand.Parameters["@TotalPrice"]).Value = (object)serviceBooking.TotalPrice;
                        ((DbParameter)sqlCommand.Parameters["@Currency"]).Value = (object)serviceBooking.Currency;
                        ((DbParameter)sqlCommand.Parameters["@SystemValue"]).Value = (object)serviceBooking.SystemValue;
                        ((DbParameter)sqlCommand.Parameters["@ChangedBy"]).Value = (object)serviceBooking.ChangedBy;
                        ((DbParameter)sqlCommand.Parameters["@DObj"]).Value = (object)JsonConvert.SerializeObject((object)serviceBooking);
                        ((DbParameter)sqlCommand.Parameters["@CallWebApi"]).Value = (object)nameof(ManagerUpdateServiceBooking);
                        if (!int.TryParse(((DbCommand)sqlCommand).ExecuteScalar().ToString(), out result))
                            result = -1;
                        ((DbConnection)sqlConnection).Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Create Service Booking Error", ex);
            }
            return result == -1 ? "No active booking" : "OK";
        }

        private List<ServiceBookingRDto> MapDataToFutureServiceBooking(DataTable resultSet)
        {
            List<ServiceBookingRDto> futureServiceBooking = new List<ServiceBookingRDto>();
            foreach (DataRow row in (InternalDataCollectionBase)resultSet.Rows)
                futureServiceBooking.Add(new ServiceBookingRDto()
                {
                    ID = row[0] != null ? long.Parse(row[0].ToString()) : 0L,
                    Status = row[1] != null ? row[1].ToString() : string.Empty,
                    AddressLine = row[2] != null ? row[2].ToString() : string.Empty,
                    ExecuteDateTime = row[3] != null ? DateTimeOffset.Parse(row[3].ToString()) : DateTimeOffset.MinValue,
                    Phone = row[4] != null ? row[4].ToString() : string.Empty,
                    ServicesCsv = row[5] != null ? row[5].ToString() : string.Empty,
                    Paid = row[6] != null && bool.Parse(row[6].ToString()),
                    PaymentType = row[7] != null ? row[7].ToString() : string.Empty,
                    TotalPrice = row[8] != null ? row[8].ToString() : string.Empty,
                    Currency = row[9] != null ? row[9].ToString() : string.Empty,
                    SystemValue = row[10] != null ? row[10].ToString() : string.Empty,
                    LastChanged = row[11] != null ? DateTimeOffset.Parse(row[11].ToString()) : DateTimeOffset.MinValue,
                    ChangedBy = row[12] != null ? row[12].ToString() : string.Empty
                });
            return futureServiceBooking;
        }
    }
}
