using AccessManagementLaredo.HelperModels;
using DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo
{
    // *********************************************************************************************
    //                                  Specific Interface.
    // *********************************************************************************************
    public interface IPermitEventRepository : Interfaces.IRepository<PermitEventHelperModel>
    {
        public string ReadPermitEvents(string role, int? id = -1);
	}

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************
    public class PermitEvent
    {
    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class PermitEventRepository : IPermitEventRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public PermitEventRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(PermitEventHelperModel entity)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO PRMT_EVNT (");
            _strQuery.Append("PRMT_EVNT_TS, ");
            _strQuery.Append("PRMT_RQST_ID, ");
            _strQuery.Append("PRMT_EVNT_TYPE_CD, ");
            _strQuery.Append("PRMT_EVNT_USER_NM, ");
            _strQuery.Append("PRMT_EVNT_USER_ROLE_NM, ");
            _strQuery.Append("PRMT_EVNT_CMNT ");
            _strQuery.Append(") OUTPUT inserted.PRMT_EVNT_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_date_created, ");
            _strQuery.Append("@prm_request_id, ");
            _strQuery.Append("@prm_type_code, ");
            _strQuery.Append("@prm_user_name, ");
            _strQuery.Append("@prm_user_role, ");
            _strQuery.Append("@prm_comment");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_date_created", entity.DateCreated);
            _queryParams.Add("prm_request_id", entity.PermitRequestId);
            _queryParams.Add("prm_type_code", entity.PermitEventTypeCode);
            _queryParams.Add("prm_user_name", entity.UserName);
            _queryParams.Add("prm_user_role", entity.UserRoleName);
            _queryParams.Add("prm_comment", entity.PermitEventComment is null ? DBNull.Value : entity.PermitEventComment);


            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            return sequenceValue;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Delete record.
        // ---------------------------------------------------------------------------------------------
        public void Delete(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM DIST WHERE ");
            _strQuery.Append("DIST_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(PermitEventHelperModel entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE DIST SET ");
            _strQuery.Append("DIST_NM = @prm_name ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("DIST_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            //_queryParams.Add("prm_name", entity.Name);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Get all records.
        // ---------------------------------------------------------------------------------------------
        public string Read(int? id = -1)
        {
            

            return "";
        }

		// ---------------------------------------------------------------------------------------------
		//                  Get all records.
		// ---------------------------------------------------------------------------------------------
		public string ReadPermitEvents(string role, int? id = -1)
		{
			_strQuery.Clear();

			_strQuery.Append("SELECT ");
			_strQuery.Append("PRMT_EVNT_ID AS Id, ");
			_strQuery.Append("PRMT_EVNT_TS AS DateCreated, ");
			_strQuery.Append("PRMT_RQST_ID AS PermitRequestId, ");
			_strQuery.Append("PRMT_EVNT_TYPE.PRMT_EVNT_TYPE_CD AS EventTypeCode, ");
			_strQuery.Append("PRMT_EVNT_TYPE_NM AS EventTypeName, ");
			_strQuery.Append("PRMT_EVNT_USER_NM AS UserName, ");
			_strQuery.Append("PRMT_EVNT_USER_ROLE_NM AS UserRoleName, ");
			_strQuery.Append("PRMT_EVNT_CMNT AS EventComment ");
			_strQuery.Append("FROM PRMT_EVNT ");
			_strQuery.Append("INNER JOIN PRMT_EVNT_TYPE ON PRMT_EVNT.PRMT_EVNT_TYPE_CD = PRMT_EVNT_TYPE.PRMT_EVNT_TYPE_CD ");

			// A record with specific "id" is searched.
			if (id != -1)
			{
				_strQuery.Append("WHERE ");
				_strQuery.Append("PRMT_RQST_ID = @prm_id ");
			}
            if (role == "OWNER" || role == "CONSULTANT")
            {
                _strQuery.Append("AND PRMT_EVNT.PRMT_EVNT_TYPE_CD != 'REVIEWED' ");
            }

			_strQuery.Append("ORDER BY ");
			_strQuery.Append("PRMT_EVNT_TS DESC ");
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
		//        Release database resources.       
		// ---------------------------------------------------------------------------------------------
		public void DisposeDBObjects()
        {
            _unitOfWork.ReleaseDBObjects();
        }

        // ---------------------------------------------------------------------------------------------
        //               Convert to upper case specific fields before CRUD operation.
        // ---------------------------------------------------------------------------------------------
        private static void ConvertCase(PermitEventHelperModel entity)
        {
            //entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : "";
        }
    }
}
