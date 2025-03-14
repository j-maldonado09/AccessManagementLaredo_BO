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
	public interface IHighwayPrefixRepository : Interfaces.IRepository<HighwayPrefix>
	{
	}

	// *********************************************************************************************
	//                                 Basic Structure Class.
	// *********************************************************************************************

	public class HighwayPrefix
	{
		public int Id { get; set; }

		[Required]
		public string Code { get; set; }
		public string? Description { get; set; }
	}

	// *********************************************************************************************
	//                                  Repository Class.
	// *********************************************************************************************
	public class HighwayPrefixRepository : IHighwayPrefixRepository
	{
		private Interfaces.IUnitOfWork _unitOfWork;
		private StringBuilder _strQuery = new StringBuilder();
		private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

		// ---------------------------------------------------------------------------------------------
		//                  Constructor.
		// ---------------------------------------------------------------------------------------------
		public HighwayPrefixRepository(Interfaces.IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Insert a new record.
		// ---------------------------------------------------------------------------------------------
		public int Create(HighwayPrefix entity)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("INSERT INTO HWY_PRFX (");
			_strQuery.Append("HWY_PRFX_CD, ");
			_strQuery.Append("HWY_PRFX_DSCR");
			_strQuery.Append(") OUTPUT inserted.HWY_PRFX_ID ");
			_strQuery.Append("VALUES (");
			_strQuery.Append("@prm_code, ");
			_strQuery.Append("@prm_description");
			_strQuery.Append(")");

			_queryParams.Clear();
			_queryParams.Add("prm_code", entity.Code);
			_queryParams.Add("prm_description", entity.Description);

			int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

			return sequenceValue;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Delete record.
		// ---------------------------------------------------------------------------------------------
		public void Delete(int id)
		{
			_strQuery.Clear();
			_strQuery.Append("DELETE FROM HWY_PRFX WHERE ");
			_strQuery.Append("HWY_PRFX_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Update record.
		// ---------------------------------------------------------------------------------------------
		public void Update(HighwayPrefix entity, int id)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("UPDATE HWY_PRFX SET ");
			_strQuery.Append("HWY_PRFX_CD = @prm_code, ");
			_strQuery.Append("HWY_PRFX_DSCR = @prm_description ");
			_strQuery.Append("WHERE ");
			_strQuery.Append("HWY_PRFX_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);
			_queryParams.Add("prm_code", entity.Code);
			_queryParams.Add("prm_description", entity.Description);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Get all records.
		// ---------------------------------------------------------------------------------------------
		public string Read(int? id = -1)
		{
			_strQuery.Clear();

			_strQuery.Append("SELECT ");
			_strQuery.Append("HWY_PRFX_ID AS Id, ");
			_strQuery.Append("HWY_PRFX_CD AS Code, ");
			_strQuery.Append("HWY_PRFX_DSCR AS Description ");
			_strQuery.Append("FROM HWY_PRFX ");

			// A record with specific "id" is searched.
			if (id != -1)
			{
				_strQuery.Append("WHERE ");
				_strQuery.Append("HWY_PRFX_ID = @prm_id ");
			}

			_strQuery.Append("ORDER BY ");
			_strQuery.Append("HWY_PRFX_CD ");
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
		private static void ConvertCase(HighwayPrefix entity)
		{
			entity.Code = (entity.Code != null) ? entity.Code.ToUpper() : DBNull.Value.ToString();
			entity.Description = (entity.Description != null) ? entity.Description.ToUpper() : DBNull.Value.ToString();
		}
	}
}

