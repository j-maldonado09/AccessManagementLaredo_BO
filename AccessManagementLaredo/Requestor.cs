using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataTier;


namespace AccessManagementLaredo
{
	// *********************************************************************************************
	//                                  Specific Interface.
	// *********************************************************************************************
	public interface IRequestorRepository : Interfaces.IRepository<Requestor>
	{
	}

	// *********************************************************************************************
	//                                 Basic Structure Class.
	// *********************************************************************************************

	public class Requestor
	{
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public int ZipCode { get; set; }
		public string StateCode { get; set; }
		public string PhoneNumber { get; set; }
	}

	// *********************************************************************************************
	//                                  Repository Class.
	// *********************************************************************************************
	public class RequestorRepository : IRequestorRepository
	{
		private Interfaces.IUnitOfWork _unitOfWork;
		private StringBuilder _strQuery = new StringBuilder();
		private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

		// ---------------------------------------------------------------------------------------------
		//                  Constructor.
		// ---------------------------------------------------------------------------------------------
		public RequestorRepository(Interfaces.IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Insert a new record.
		// ---------------------------------------------------------------------------------------------
		public int Create(Requestor entity)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("INSERT INTO RQSTR (");
			_strQuery.Append("RQSTR_FRST_NM, ");
			_strQuery.Append("RQSTR_LAST_NM, ");
			_strQuery.Append("RQSTR_ADDR, ");
			_strQuery.Append("RQSTR_CITY_NM, ");
			_strQuery.Append("RQSTR_ZIPCD, ");
			_strQuery.Append("RQSTR_STATE_CD, ");
			_strQuery.Append("RQSTR_PHN_NBR");
			_strQuery.Append(") OUTPUT inserted.RQSTR_ID ");
			_strQuery.Append("VALUES (");
			_strQuery.Append("@prm_first_name, ");
			_strQuery.Append("@prm_last_name, ");
			_strQuery.Append("@prm_address, ");
			_strQuery.Append("@prm_city_name, ");
			_strQuery.Append("@prm_zipcode, ");
			_strQuery.Append("@prm_state_code, ");
			_strQuery.Append("@prm_phone_number");
			_strQuery.Append(")");

			_queryParams.Clear();
			_queryParams.Add("prm_name", entity.FirstName);
			_queryParams.Add("prm_description", entity.LastName);
			_queryParams.Add("prm_description", entity.Address);
			_queryParams.Add("prm_description", entity.City);
			_queryParams.Add("prm_description", entity.ZipCode);
			_queryParams.Add("prm_description", entity.StateCode);
			_queryParams.Add("prm_description", entity.PhoneNumber);

			int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

			return sequenceValue;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Delete record.
		// ---------------------------------------------------------------------------------------------
		public void Delete(int id)
		{
			_strQuery.Clear();
			_strQuery.Append("DELETE FROM RQSTR WHERE ");
			_strQuery.Append("RQSTR_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Update record.
		// ---------------------------------------------------------------------------------------------
		public void Update(Requestor entity, int id)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("UPDATE RQSTR SET ");
			_strQuery.Append("RQSTR_FRST_NM = @prm_first_name, ");
			_strQuery.Append("RQSTR_LAST_NM = @prm_last_name, ");
			_strQuery.Append("RQSTR_ADDR = @prm_address, ");
			_strQuery.Append("RQSTR_CITY_NM = @prm_city_name, ");
			_strQuery.Append("RQSTR_ZIPCD = @prm_zipcode, ");
			_strQuery.Append("RQSTR_STATE_CD = @prm_state_code, ");
			_strQuery.Append("RQSTR_PHN_NBR = @prm_phone_number ");
			_strQuery.Append("WHERE ");
			_strQuery.Append("RQSTR_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);
			_queryParams.Add("prm_first_name", entity.FirstName);
			_queryParams.Add("prm_last_name", entity.LastName);
			_queryParams.Add("prm_address", entity.Address);
			_queryParams.Add("prm_city_name", entity.City);
			_queryParams.Add("prm_zipcode", entity.ZipCode);
			_queryParams.Add("prm_state_code", entity.StateCode);
			_queryParams.Add("prm_phone_number", entity.PhoneNumber);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Get all records.
		// ---------------------------------------------------------------------------------------------
		public string Read(int? id = -1)
		{
			_strQuery.Clear();

			_strQuery.Append("SELECT ");
			_strQuery.Append("RQSTR_ID AS Id, ");
			_strQuery.Append("RQSTR_FRST_NM AS FirstName, ");
			_strQuery.Append("RQSTR_LAST_NM AS LastName, ");
			_strQuery.Append("RQSTR_ADDR AS Address, ");
			_strQuery.Append("RQSTR_CITY_NM AS City, ");
			_strQuery.Append("RQSTR_ZIPCD AS ZipCode, ");
			_strQuery.Append("RQSTR_STATE_CD AS StateCode, ");
			_strQuery.Append("RQSTR_PHN_NBR AS PhoneNumber ");
			_strQuery.Append("FROM RQSTR ");

			// A record with specific "id" is searched.
			if (id != -1)
			{
				_strQuery.Append("WHERE ");
				_strQuery.Append("RQSTR_ID = @prm_id ");
			}

			_strQuery.Append("ORDER BY ");
			_strQuery.Append("RQSTR_LAST_NM ");
			_strQuery.Append("FOR JSON AUTO");

			// A record with specific "id" is searched.
			_queryParams.Clear();
			if (id != -1)
			{
				_queryParams.Add("prm_id", id);
			}

			string result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);

			return result;
		}

		// ---------------------------------------------------------------------------------------------
		//        Release database resources.       
		// ---------------------------------------------------------------------------------------------
		public void DisposeDBObjects()
		{
			_unitOfWork.ReleaseDBObjects();
		}

		// ---------------------------------------------------------------------------------------------
		//               Convert to upper case specific fields before CRUD operation.
		// ---------------------------------------------------------------------------------------------
		private static void ConvertCase(Requestor entity)
		{
			entity.FirstName = (entity.FirstName != null) ? entity.FirstName.ToUpper() : "";
			entity.LastName = (entity.LastName != null) ? entity.LastName.ToUpper() : "";
			entity.Address = (entity.Address != null) ? entity.Address.ToUpper() : "";
			entity.City = (entity.City != null) ? entity.City.ToUpper() : "";
			entity.StateCode = (entity.StateCode != null) ? entity.StateCode.ToUpper() : "";
		}
	}
}

