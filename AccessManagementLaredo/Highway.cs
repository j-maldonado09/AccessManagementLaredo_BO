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
	public interface IHighwayRepository : Interfaces.IRepository<Highway>
	{
		public string ReadByCounty(int? id = -1);
		public string ReadPrefixesByCounty();
		public string ReadHighways();

	}

	// *********************************************************************************************
	//                                 Basic Structure Class.
	// *********************************************************************************************

	public class Highway
	{
		public int Id { get; set; }

		[Required]
		public int CountyId { get; set; }
		public int PrefixId { get; set; }
		[Required]
		public string Number { get; set; }
	}

	// *********************************************************************************************
	//                                  Repository Class.
	// *********************************************************************************************
	public class HighwayRepository : IHighwayRepository
	{
		private Interfaces.IUnitOfWork _unitOfWork;
		private StringBuilder _strQuery = new StringBuilder();
		private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

		// ---------------------------------------------------------------------------------------------
		//                  Constructor.
		// ---------------------------------------------------------------------------------------------
		public HighwayRepository(Interfaces.IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Insert a new record.
		// ---------------------------------------------------------------------------------------------
		public int Create(Highway entity)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("INSERT INTO HWY (");
			_strQuery.Append("CNTY_ID, ");
			_strQuery.Append("HWY_PRFX_ID, ");
            _strQuery.Append("HWY_NBR ");
            _strQuery.Append(") OUTPUT inserted.HWY_ID ");
			_strQuery.Append("VALUES (");
			_strQuery.Append("@prm_county_id, ");
			_strQuery.Append("@prm_prefix_id, ");
            _strQuery.Append("@prm_number");

            _strQuery.Append(")");

			_queryParams.Clear();
			_queryParams.Add("prm_county_id", entity.CountyId);
			_queryParams.Add("prm_prefix_id", entity.PrefixId);
            _queryParams.Add("prm_number", entity.Number);


            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

			return sequenceValue;
		}

		// ---------------------------------------------------------------------------------------------
		//                  Delete record.
		// ---------------------------------------------------------------------------------------------
		public void Delete(int id)
		{
			_strQuery.Clear();
			_strQuery.Append("DELETE FROM HWY WHERE ");
			_strQuery.Append("HWY_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Update record.
		// ---------------------------------------------------------------------------------------------
		public void Update(Highway entity, int id)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("UPDATE HWY SET ");
			_strQuery.Append("CNTY_ID = @prm_county_id, ");
			_strQuery.Append("HWY_PRFX_ID = @prm_prefix_id, ");
            _strQuery.Append("HWY_NBR = @prm_number ");
            _strQuery.Append("WHERE ");
			_strQuery.Append("HWY_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);
            _queryParams.Add("prm_county_id", entity.CountyId);
            _queryParams.Add("prm_prefix_id", entity.PrefixId);
            _queryParams.Add("prm_number", entity.Number);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Get all records.
		// ---------------------------------------------------------------------------------------------
		public string Read(int? id = -1)
		{
			_strQuery.Clear();

			_strQuery.Append("SELECT ");
			_strQuery.Append("HWY.HWY_ID AS Id, ");
			_strQuery.Append("HWY.HWY_PRFX_ID AS PrefixId, ");
            _strQuery.Append("HWY_PRFX_CD AS PrefixCode, ");
            _strQuery.Append("HWY_NBR AS Number ");
            _strQuery.Append("FROM HWY ");
            _strQuery.Append("INNER JOIN HWY_PRFX ON HWY_PRFX.HWY_PRFX_ID = HWY.HWY_PRFX_ID ");

            // A record with specific "id" is searched.
            if (id != -1)
			{
				_strQuery.Append("WHERE ");
				_strQuery.Append("HWY_ID = @prm_id ");
			}

			_strQuery.Append("ORDER BY ");
			_strQuery.Append("HWY_NBR ");
			_strQuery.Append("FOR JSON PATH, INCLUDE_NULL_VALUES");

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
        //                  Get records from a county.
        // ---------------------------------------------------------------------------------------------
        public string ReadByCounty(int? id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("HWY.HWY_ID AS Id, ");
            _strQuery.Append("HWY.HWY_PRFX_ID AS PrefixId, ");
            _strQuery.Append("HWY_PRFX_CD AS PrefixCode, ");
            _strQuery.Append("HWY_NBR AS Number ");
            _strQuery.Append("FROM HWY ");
            _strQuery.Append("INNER JOIN HWY_PRFX ON HWY_PRFX.HWY_PRFX_ID = HWY.HWY_PRFX_ID ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("CNTY_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("HWY_NBR ");
            _strQuery.Append("FOR JSON PATH, INCLUDE_NULL_VALUES");

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
        //                  Get unique (distinct) highway prefixes related to a county.
        // ---------------------------------------------------------------------------------------------
        public string ReadPrefixesByCounty()
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT DISTINCT ");
            _strQuery.Append("HWY.CNTY_ID AS CountyId, ");
            _strQuery.Append("HWY.HWY_PRFX_ID AS PrefixId, ");
            _strQuery.Append("HWY_PRFX.HWY_PRFX_CD AS PrefixCode, ");
			_strQuery.Append("CONCAT(TRIM(STR(HWY.CNTY_ID)), TRIM(STR(HWY.HWY_PRFX_ID))) AS CountyPrefix ");
			_strQuery.Append("FROM HWY ");
            _strQuery.Append("INNER JOIN HWY_PRFX ON HWY_PRFX.HWY_PRFX_ID = HWY.HWY_PRFX_ID ");
            _strQuery.Append("ORDER BY ");
            _strQuery.Append("HWY.CNTY_ID, HWY_PRFX.HWY_PRFX_CD ");
            _strQuery.Append("FOR JSON PATH, INCLUDE_NULL_VALUES");

            _queryParams.Clear();

            string result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);

            return result;
        }

		// ---------------------------------------------------------------------------------------------
		//                  Get highway numbers related to a county and a prefix.
		// ---------------------------------------------------------------------------------------------
		public string ReadHighways()
		{
			_strQuery.Clear();

			_strQuery.Append("SELECT ");
			_strQuery.Append("HWY.HWY_ID AS HighwayId, ");
			_strQuery.Append("HWY.CNTY_ID AS CountyId, ");
			_strQuery.Append("HWY.HWY_NBR AS HighwayNumber, ");
			_strQuery.Append("HWY.HWY_PRFX_ID AS PrefixId, ");
			_strQuery.Append("HWY_PRFX.HWY_PRFX_CD AS PrefixCode, ");
			_strQuery.Append("CONCAT(TRIM(STR(HWY.CNTY_ID)), TRIM(STR(HWY.HWY_PRFX_ID))) AS CountyPrefix ");
			_strQuery.Append("FROM HWY ");
			_strQuery.Append("INNER JOIN HWY_PRFX ON HWY_PRFX.HWY_PRFX_ID = HWY.HWY_PRFX_ID ");
			_strQuery.Append("ORDER BY ");
			_strQuery.Append("HWY.CNTY_ID, HWY_PRFX.HWY_PRFX_CD ");
			_strQuery.Append("FOR JSON PATH, INCLUDE_NULL_VALUES");

			_queryParams.Clear();

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
		private static void ConvertCase(Highway entity)
		{
			//entity.Code = (entity.Code != null) ? entity.Code.ToUpper() : DBNull.Value.ToString();
            //entity.Description = (entity.Description != null) ? entity.Description.ToUpper() : DBNull.Value.ToString();
            entity.Number = (entity.Number != null) ? entity.Number.ToUpper() : DBNull.Value.ToString();
        }
    }
}

