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
	public interface IPermitEventTypeRepository : Interfaces.IRepository<PermitEventType>
	{
	}

	// *********************************************************************************************
	//                                 Basic Structure Class.
	// *********************************************************************************************

	public class PermitEventType
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
        [Required]
        public string Code { get; set; }
    }

	// *********************************************************************************************
	//                                  Repository Class.
	// *********************************************************************************************
	public class PermitEventTypeRepository : IPermitEventTypeRepository
	{
		private Interfaces.IUnitOfWork _unitOfWork;
		private StringBuilder _strQuery = new StringBuilder();
		private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

		// ---------------------------------------------------------------------------------------------
		//                  Constructor.
		// ---------------------------------------------------------------------------------------------
		public PermitEventTypeRepository(Interfaces.IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Insert a new record.
		// ---------------------------------------------------------------------------------------------
		public int Create(PermitEventType entity)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("INSERT INTO PRMT_EVNT_TYPE (");
			_strQuery.Append("PRMT_EVNT_TYPE_NM, ");
			_strQuery.Append("PRMT_EVNT_TYPE_DSCR, ");
			_strQuery.Append("PRMT_EVNT_TYPE_CD");
			_strQuery.Append(") OUTPUT inserted.PRMT_EVNT_TYPE_ID ");
			_strQuery.Append("VALUES (");
			_strQuery.Append("@prm_name, ");
			_strQuery.Append("@prm_description, ");
			_strQuery.Append("@prm_code");
			_strQuery.Append(")");

			_queryParams.Clear();
			_queryParams.Add("prm_name", entity.Name);
			_queryParams.Add("prm_description", entity.Description);
			_queryParams.Add("prm_code", entity.Code);

			int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

			return sequenceValue;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Delete record.
		// ---------------------------------------------------------------------------------------------
		public void Delete(int id)
		{
			_strQuery.Clear();
			_strQuery.Append("DELETE FROM PRMT_EVNT_TYPE WHERE ");
			_strQuery.Append("PRMT_EVNT_TYPE_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Update record.
		// ---------------------------------------------------------------------------------------------
		public void Update(PermitEventType entity, int id)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("UPDATE PRMT_EVNT_TYPE SET ");
			_strQuery.Append("PRMT_EVNT_TYPE_NM = @prm_name, ");
			_strQuery.Append("PRMT_EVNT_TYPE_DSCR = @prm_description, ");
			_strQuery.Append("PRMT_EVNT_TYPE_CD = @prm_code ");
			_strQuery.Append("WHERE ");
			_strQuery.Append("PRMT_EVNT_TYPE_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);
			_queryParams.Add("prm_name", entity.Name);
			_queryParams.Add("prm_description", entity.Description);
			_queryParams.Add("prm_code", entity.Code);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Get all records.
		// ---------------------------------------------------------------------------------------------
		public string Read(int? id = -1)
		{
			_strQuery.Clear();

			_strQuery.Append("SELECT ");
			_strQuery.Append("PRMT_EVNT_TYPE_ID AS Id, ");
			_strQuery.Append("PRMT_EVNT_TYPE_NM AS Name, ");
			_strQuery.Append("PRMT_EVNT_TYPE_DSCR AS Description, ");
			_strQuery.Append("PRMT_EVNT_TYPE_CD AS Code ");
			_strQuery.Append("FROM PRMT_EVNT_TYPE ");

			// A record with specific "id" is searched.
			if (id != -1)
			{
				_strQuery.Append("WHERE ");
				_strQuery.Append("PRMT_EVNT_TYPE_ID = @prm_id ");
			}

			_strQuery.Append("ORDER BY ");
			_strQuery.Append("PRMT_EVNT_TYPE_NM ");
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
		private static void ConvertCase(PermitEventType entity)
		{
			entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : DBNull.Value.ToString();
			entity.Description = (entity.Description != null) ? entity.Description.ToUpper() : DBNull.Value.ToString();
			entity.Code = (entity.Code != null) ? entity.Code.ToUpper() : DBNull.Value.ToString();

		}
	}
}

