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
	public interface ICountyRepository : Interfaces.IRepository<County>
	{
	}

	// *********************************************************************************************
	//                                 Basic Structure Class.
	// *********************************************************************************************

	public class County
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		[Required]
		public string Number { get; set; }
        [Required]
		public string Contact { get; set; }
	}

	// *********************************************************************************************
	//                                  Repository Class.
	// *********************************************************************************************
	public class CountyRepository : ICountyRepository
	{
		private Interfaces.IUnitOfWork _unitOfWork;
		private StringBuilder _strQuery = new StringBuilder();
		private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

		// ---------------------------------------------------------------------------------------------
		//                  Constructor.
		// ---------------------------------------------------------------------------------------------
		public CountyRepository(Interfaces.IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Insert a new record.
		// ---------------------------------------------------------------------------------------------
		public int Create(County entity)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("INSERT INTO CNTY (");
			_strQuery.Append("CNTY_NM, ");
			_strQuery.Append("CNTY_NBR, ");
			_strQuery.Append("CNTY_CNTCT");
			_strQuery.Append(") OUTPUT inserted.CNTY_ID ");
			_strQuery.Append("VALUES (");
			_strQuery.Append("@prm_name, ");
			_strQuery.Append("@prm_number, ");
			_strQuery.Append("@prm_contact");
			_strQuery.Append(")");

			_queryParams.Clear();
			_queryParams.Add("prm_name", entity.Name);
			_queryParams.Add("prm_number", entity.Number);
			_queryParams.Add("prm_contact", entity.Contact);

			int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

			return sequenceValue;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Delete record.
		// ---------------------------------------------------------------------------------------------
		public void Delete(int id)
		{
			_strQuery.Clear();
			_strQuery.Append("DELETE FROM CNTY WHERE ");
			_strQuery.Append("CNTY_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Update record.
		// ---------------------------------------------------------------------------------------------
		public void Update(County entity, int id)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("UPDATE CNTY SET ");
			_strQuery.Append("CNTY_NM = @prm_name, ");
			_strQuery.Append("CNTY_NBR = @prm_number, ");
			_strQuery.Append("CNTY_CNTCT = @prm_contact ");
			_strQuery.Append("WHERE ");
			_strQuery.Append("CNTY_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);
			_queryParams.Add("prm_name", entity.Name);
			_queryParams.Add("prm_number", entity.Number);
			_queryParams.Add("prm_contact", entity.Contact);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Get all records.
		// ---------------------------------------------------------------------------------------------
		public string Read(int? id = -1)
		{
			_strQuery.Clear();

			_strQuery.Append("SELECT ");
			_strQuery.Append("CNTY_ID AS Id, ");
			_strQuery.Append("CNTY_NM AS Name, ");
			_strQuery.Append("CNTY_NBR AS Number, ");
			_strQuery.Append("CNTY_CNTCT AS Contact ");
			_strQuery.Append("FROM CNTY ");

			// A record with specific "id" is searched.
			if (id != -1)
			{
				_strQuery.Append("WHERE ");
				_strQuery.Append("CNTY_ID = @prm_id ");
			}

			_strQuery.Append("ORDER BY ");
			_strQuery.Append("CNTY_NM ");
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
		private static void ConvertCase(County entity)
		{
			entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : DBNull.Value.ToString();
			entity.Number = (entity.Number!= null) ? entity.Number.ToUpper() : DBNull.Value.ToString();
			entity.Contact = (entity.Contact!= null) ? entity.Contact.ToUpper() : DBNull.Value.ToString();
		}
	}
}

